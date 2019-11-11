from __future__ import absolute_import
from __future__ import division
from __future__ import print_function

from flask import Flask
from flask import request, jsonify
from flask_cors import CORS, cross_origin
import numpy as np
import cv2
import base64
import pickle
from io import BytesIO
from PIL import Image
from scipy import misc
import sys
import os
import argparse
import tensorflow as tf
import numpy as np
import facenet
import align.detect_face
import random
from time import sleep
import math
from sklearn.svm import SVC
import collections
import datetime
import pyodbc

#tạo để lấy dữ liệu training
conn=pyodbc.connect(
    'Driver={SQL Server};'
    'Server=DESKTOP-OVLNOKF;'
    'Database=DATN_FaceRecognition;'
    'Trusted_Connection=yes;'
)

# Khai bao cong cua server
my_port = '8000'
scale = 0.00392
conf_threshold = 0.5
nms_threshold = 0.4

# Doan ma khoi tao server
app = Flask(__name__)
CORS(app)

# Cai dat cac tham so can thiet để nhận diện
MINSIZE = 20
THRESHOLD = [0.6, 0.7, 0.7]
FACTOR = 0.709
IMAGE_SIZE = 182
INPUT_IMAGE_SIZE = 160
CLASSIFIER_PATH = '../Models/facemodel.pkl'
FACENET_MODEL_PATH = '../Models/20180402-114759.pb'




with tf.Graph().as_default():
    # Cai dat GPU neu co
    gpu_options = tf.GPUOptions(per_process_gpu_memory_fraction=0.6)
    sess = tf.Session(config=tf.ConfigProto(gpu_options=gpu_options, log_device_placement=False))
    with sess.as_default():
        # Load model MTCNN phat hien khuon mat
        print('Loading feature extraction model')
        facenet.load_model(FACENET_MODEL_PATH)
        
        # Lay tensor input va output
        images_placeholder = tf.get_default_graph().get_tensor_by_name("input:0")
        embeddings = tf.get_default_graph().get_tensor_by_name("embeddings:0")
        phase_train_placeholder = tf.get_default_graph().get_tensor_by_name("phase_train:0")
        embedding_size = embeddings.get_shape()[1]

        # Cai dat cac mang con
        pnet, rnet, onet = align.detect_face.create_mtcnn(sess, "align")

        people_detected = set()
        person_detected = collections.Counter()

# Cac ham ho tro chay YOLO

@app.route("/")
def hello():
    return "Hello PyMi.vn!"

def get_output_layers(net):
    layer_names = net.getLayerNames()
    output_layers = [layer_names[i[0] - 1] for i in net.getUnconnectedOutLayers()]
    return output_layers


def build_return(class_id, x, y, x_plus_w, y_plus_h):
    return str(class_id) + "," + str(x) + "," + str(y) + "," + str(x_plus_w) + "," + str(y_plus_h)


# Khai bao ham xu ly request trainning
@app.route('/trainning', methods=['POST'])
@cross_origin()
def trainning():

    retString = "không phát hiện thấy khuôn mặt lúc training_"
    str_ba64=""
    parser = argparse.ArgumentParser()

    parser.add_argument('--detect_multiple_faces', type=bool,
                        help='Detect and align multiple faces per image.', default=False)
    #facenet.store_revision_info(src_path, output_dir, ' '.join(sys.argv))
                        
    with tf.Graph().as_default(): #with dùng để xử lý tương tương như try catch
        gpu_options = tf.GPUOptions(per_process_gpu_memory_fraction=0.25)
        sess = tf.Session(config=tf.ConfigProto(gpu_options=gpu_options, log_device_placement=False))
        with sess.as_default():
            pnet, rnet, onet = align.detect_face.create_mtcnn(sess, None)
    minsize = 20 # minimum size of face
    threshold = [ 0.6, 0.7, 0.7 ]  # three steps's threshold
    factor = 0.709 # scale factor
    
    # Lay du lieu image B64 gui len va chuyen thanh image
    image_b64 = request.form.get('image')
    image=Image.open(BytesIO(base64.b64decode(image_b64)))
    
    #image=cv2.imdecode(image, cv2.IMREAD_ANYCOLOR)
    image=np.asarray(image)
    if image.ndim<2:
        print('Unable to align ')
        return retString
        
    if image.ndim == 2:
        image = facenet.to_rgb(image)
    image = image[:,:,0:3]
    bounding_boxes, _ = align.detect_face.detect_face(image, minsize, pnet, rnet, onet, threshold, factor)
    nrof_faces = bounding_boxes.shape[0]
    if nrof_faces>0:
        det = bounding_boxes[:,0:4]
        det_arr = []
        img_size = np.asarray(image.shape)[0:2]
        if nrof_faces>1:
            if parser.parse_args().detect_multiple_faces:
                for i in range(nrof_faces):
                    det_arr.append(np.squeeze(det[i]))
            else:
                bounding_box_size = (det[:,2]-det[:,0])*(det[:,3]-det[:,1])
                img_center = img_size / 2
                offsets = np.vstack([ (det[:,0]+det[:,2])/2-img_center[1], (det[:,1]+det[:,3])/2-img_center[0] ])
                offset_dist_squared = np.sum(np.power(offsets,2.0),0)
                index = np.argmax(bounding_box_size-offset_dist_squared*2.0) # some extra weight on the centering
                det_arr.append(det[index,:])
        else:
            det_arr.append(np.squeeze(det))
        for i, det in enumerate(det_arr):
            det = np.squeeze(det)
            bb = np.zeros(4, dtype=np.int32)
            bb[0] = np.maximum(det[0]-32/2, 0)
            bb[1] = np.maximum(det[1]-32/2, 0)
            bb[2] = np.minimum(det[2]+32/2, img_size[1])
            bb[3] = np.minimum(det[3]+32/2, img_size[0])
            cropped = image[bb[1]:bb[3],bb[0]:bb[2],:]
            scaled = misc.imresize(cropped, (160, 160), interp='bilinear')
            
            pil_img = Image.fromarray(scaled)
            buff = BytesIO()
            pil_img.save(buff, format="PNG")
            str_ba64 = base64.b64encode(buff.getvalue()).decode("utf-8")

            retString = "training thành công_"
    
    retString=retString+str_ba64

       

    return retString;


# Khai bao ham xu ly request trainning
@app.route('/train', methods=['POST'])
@cross_origin()
def train():
    result="train thất bại"
    list_img=[]
    list_lable_number=[]
    lables=[]
    list_lables=[]
    
    cursor=conn.cursor()
    cursor.execute("select Users.Name , Imagers.Img, Imagers.Id_Account from Imagers inner join Accounts on Imagers.Id_Account=Accounts.Id inner join Users on Accounts.Id_User=Users.Id where Imagers.DelFlag='False' ORDER BY Imagers.Id_Account")
    for row in cursor:
        list_img.append(row[1])
        list_lable_number.append(row[2])
        lables.append(row[0])
    list_lables=list( dict.fromkeys(lables) )
    
    
    with tf.Graph().as_default():
      
        with tf.Session() as sess:
            
            np.random.seed(seed=666)
            

            #paths là đường dẫn tới anh, labels là 
            #labels chứa các số thứ tự để biết thằng ảnh là của ai
            #paths, labels = facenet.get_image_paths_and_labels(dataset)
            
            print('Number of classes: %d' % len(list_lables))
            print('Number of images: %d' % len(list_img))
            
            # Load the model
            print('Loading feature extraction model')
            facenet.load_model('../Models/20180402-114759.pb')
            
            
            #========================================================================
            
            # Get input and output tensors
            images_placeholder = tf.get_default_graph().get_tensor_by_name("input:0")
            embeddings = tf.get_default_graph().get_tensor_by_name("embeddings:0")
            phase_train_placeholder = tf.get_default_graph().get_tensor_by_name("phase_train:0")
            embedding_size = embeddings.get_shape()[1]
            
            
            # lấy ra số lượng ảnh
            #nrof_images = len(paths)
            nrof_images = len(list_img)
            
            nrof_batches_per_epoch = int(math.ceil(1.0*nrof_images / 1000))
            emb_array = np.zeros((nrof_images, embedding_size))
            for i in range(nrof_batches_per_epoch):
                start_index = i*1000
                end_index = min((i+1)*1000, nrof_images)
                paths_batch = list_img[start_index:end_index]
                images = facenet.load_data(paths_batch, False, False, 160)
                feed_dict = { images_placeholder:images, phase_train_placeholder:False }
                emb_array[start_index:end_index,:] = sess.run(embeddings, feed_dict=feed_dict)
            
            classifier_filename_exp = os.path.expanduser('../Models/facemodel.pkl')

            
            # Train classifier
            print('Training classifier')
            model = SVC(kernel='linear', probability=True)
            model.fit(emb_array, list_lable_number)
        
            # Create a list of class names
            # class_names = [ cls.name.replace('_', ' ') for cls in dataset]

            # Saving classifier model
            with open(classifier_filename_exp, 'wb') as outfile:
                pickle.dump((model, list_lables), outfile)
            print('Saved classifier model to file "%s"' % classifier_filename_exp)
            result="train thành công"
    return result         
  
#chuỗi trả về
def build_return(name, x, y, x_plus_w, y_plus_h,probabilities):
    return str(name) + "," + str(x) + "," + str(y) + "," + str(x_plus_w) + "," + str(y_plus_h)  +","+str(probabilities)  
    
# Khai bao ham xu ly request trainning
@app.route('/run_video', methods=['POST'])
@cross_origin()
def run_video():
    retString=""
    # Load model da train de nhan dien khuon mat - thuc chat la classifier
    with open(CLASSIFIER_PATH, 'rb') as file:
        model, class_names = pickle.load(file)
    print("Custom Classifier, Successfully loaded")

    
    with tf.Graph().as_default():
        with sess.as_default():
            # Doc tung frame
            #fram là thanh niên ảnh được đưa lên
            
            image_b64_run = request.form.get('image')
            frame = np.fromstring(base64.b64decode(image_b64_run), dtype=np.uint8)
            frame=cv2.imdecode(frame, cv2.IMREAD_ANYCOLOR)
            
            
            # Phat hien khuon mat, tra ve vi tri trong bounding_boxes
            bounding_boxes, _ = align.detect_face.detect_face(frame, MINSIZE, pnet, rnet, onet, THRESHOLD, FACTOR)

            faces_found = bounding_boxes.shape[0]
            try:
                # Neu co it nhat 1 khuon mat trong frame
                if faces_found > 0:
                    det = bounding_boxes[:, 0:4]
                    bb = np.zeros((faces_found, 4), dtype=np.int32)
                    for i in range(faces_found):
                        bb[i][0] = det[i][0]
                        bb[i][1] = det[i][1]
                        bb[i][2] = det[i][2]
                        bb[i][3] = det[i][3]

                        # Cat phan khuon mat tim duoc
                        cropped = frame[bb[i][1]:bb[i][3], bb[i][0]:bb[i][2], :]
                        scaled = cv2.resize(cropped, (INPUT_IMAGE_SIZE, INPUT_IMAGE_SIZE),
                                            interpolation=cv2.INTER_CUBIC)
                        scaled = facenet.prewhiten(scaled)
                        scaled_reshape = scaled.reshape(-1, INPUT_IMAGE_SIZE, INPUT_IMAGE_SIZE, 3)
                        feed_dict = {images_placeholder: scaled_reshape, phase_train_placeholder: False}
                        emb_array = sess.run(embeddings, feed_dict=feed_dict)
                        
                        # Dua vao model de classifier
                        predictions = model.predict_proba(emb_array)
                        best_class_indices = np.argmax(predictions, axis=1)
                        best_class_probabilities = predictions[
                            np.arange(len(best_class_indices)), best_class_indices]
                        
                        # Lay ra ten va ty le % cua class co ty le cao nhat
                        best_name = class_names[best_class_indices[0]]
                        #print("Name: {}, Probability: {}".format(best_name, best_class_probabilities))

                        

                        # Neu ty le nhan dang > 0.5 thi hien thi ten
                        if best_class_probabilities > 0.6:
                            name = class_names[best_class_indices[0]]
                        else:
                            # Con neu <=0.5 thi hien thi Unknow
                            name = "Unknown"
                            
                        
                        retString += build_return(name, bb[i][0], bb[i][1], bb[i][2], bb[i][3],best_class_probabilities) + "|"
            except:
                retString=""
                
    return retString
        

def parse_arguments():
    parser = argparse.ArgumentParser()
    parser.add_argument('--detect_multiple_faces', type=bool,
                        help='Detect and align multiple faces per image.', default=False)
    return parser.parse_args()

# Thuc thi server
if __name__ == '__main__':
    app.run(debug=True, host='localhost',port=my_port)
    