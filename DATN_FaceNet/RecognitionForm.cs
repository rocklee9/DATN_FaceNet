using DATN_FaceNet.Controller;
using Emgu.CV;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace DATN_FaceNet
{
    public partial class RecognitionForm : Form
    {
        private OpenFileDialog _openFileDialog;
        VideoCapture grabber;
        Image<Bgr, Byte> currentFrame;
        string server_path =  "http://localhost:5000/run_video";
        public RecognitionForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();

            cbbTypeRecognition.DataSource = TrainingController.Instance.GetOptionRecogtion();
            cbbTypeRecognition.DisplayMember = "Name";
            cbbTypeRecognition.ValueMember = "Id";
            cbbTypeRecognition.SelectedValue = 1;

            this.cbbTypeRecognition.SelectedValueChanged += new System.EventHandler(this.cbbTypeRecognition_SelectedValueChanged);
        }

        

        void FrameGrabber(object sender, EventArgs e)
        {
            //Get the current frame form capture device
            using (var imageFrame = grabber.QueryFrame().ToImage<Bgr, byte>())
            {
                if (imageFrame != null)
                {
                    Recognition(imageFrame);
                }
               
            }
        }

        private void Recognition(Image<Bgr,byte> image)
        {
            int numberFace = 0;
            string _names = "";
            //currentFrame = grabber.QueryFrame().ToImage<Bgr, byte>();

            // Convert image to B64
            string B64 = ConvertImageToBase64String(image.ToBitmap());

            // Goi len server va tra ve ket qua
            
            string retStr = sendPOST(server_path, B64);
            string[] arrRet = retStr.Split('|');
            for (int i = 0; i < arrRet.Length - 1; i++)
            {
                string[] face = arrRet[i].Split(',');

                Rectangle rect = new Rectangle(Convert.ToInt32(face[1]), Convert.ToInt32(face[2]), Convert.ToInt32(face[3]) - Convert.ToInt32(face[1]), Convert.ToInt32(face[4]) - Convert.ToInt32(face[2]));
                image.Draw(rect
                    , new Bgr(Color.Red), 2);
                //Point pont = new Point(rect.Location.X, rect.Location.Y - 40);
                //CvInvoke.PutText(imageFrame, face[0], rect.Location, Emgu.CV.CvEnum.FontFace.HersheyPlain, 3, new MCvScalar(255), 2);
                
                
                if(string.Compare(face[0], "Unknown") != 0)
                {
                    image.Draw(face[0], new Point(rect.Location.X, rect.Location.Y - 40), Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1, new Bgr(Color.Red), 2);
                    image.Draw(face[5], new Point(rect.Location.X, rect.Location.Y - 10), Emgu.CV.CvEnum.FontFace.HersheyComplexSmall, 1, new Bgr(Color.Red), 2);
                    numberFace++;
                    _names += numberFace+" : "+ face[0]+"\n";
                }
                else
                {
                    image.Draw(face[0], new Point(rect.Location.X, rect.Location.Y - 15), Emgu.CV.CvEnum.FontFace.HersheyTriplex, 1, new Bgr(Color.Red), 2);
                }
            }
            pictureInput.Image = image.ToBitmap();
            rtb_result.Text = "Phát hiện được số khuôn mặt là : " + (arrRet.Length-1) + "\n" + "Số người phát hiện được là : " + numberFace + "\n"+_names;
        }



        // Ham goi HTTP Get len server
        public string sendGet(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        // Ham chuyen Image thanh Base 64
        public static string ConvertImageToBase64String(Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {

                image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        // Ham convert B64 de gui len server
        private String EscapeData(string B64)
        {
            int B64_length = B64.Length;
            if (B64_length <= 32000)
            {
                return Uri.EscapeDataString(B64);
            }


            int idx = 0;
            StringBuilder builder = new StringBuilder();
            String substr = B64.Substring(idx, 32000);
            while (idx < B64_length)
            {
                builder.Append(Uri.EscapeDataString(substr));
                idx += 32000;

                if (idx < B64_length)
                {

                    substr = B64.Substring(idx, Math.Min(32000, B64_length - idx));
                }

            }
            return builder.ToString();

        }

        // Ham goi HTTP POST len server de detect
        private String sendPOST(string url, string B64)
        {
            try
            {

                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 1000000;
                var postData = "image=" + EscapeData(B64);

                var data = Encoding.ASCII.GetBytes(postData);

                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;


                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }


                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (Exception ex)
            {
                return "Exception" + ex.ToString();
            }
        }

        private void cbbTypeRecognition_SelectedValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbbTypeRecognition.SelectedValue) == 1)
            {
                btnOpen.Visible = false;
                btnRecog.Visible = false;
                btnStart.Visible = true;
                btnClose.Visible = true;
            }
            else
            {
                btnOpen.Visible = true;
                btnRecog.Visible = true;
                btnStart.Visible = false;
                btnClose.Visible = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            grabber.Stop();
            grabber.Pause();

        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            grabber = new VideoCapture();


            grabber.QueryFrame();

            Application.Idle += new EventHandler(FrameGrabber);
            btnStart.Enabled = false;
        }

        private void btnRecog_Click(object sender, EventArgs e)
        {
            Recognition(new Image<Bgr, Byte>(new Bitmap(pictureInput.Image)));
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbbTypeRecognition.SelectedValue) == 3)
            {
                _openFileDialog = new OpenFileDialog();
                // image filters  
                _openFileDialog.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp;*.png)|*.jpg; *.jpeg; *.gif; *.bmp;*png";
                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // display image in picture box  
                    pictureInput.Image = new Bitmap(_openFileDialog.FileName);
                    // image file path  
                }
            }
            else
            {
                btnClose.Visible = true;
                btnRecog.Visible = false;
                _openFileDialog = new OpenFileDialog();
                // image filters  
                _openFileDialog.Filter = "Image Files(*.mp4 )|*.mp4";
                if (_openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    
                    grabber = new VideoCapture(_openFileDialog.FileName);


                    grabber.QueryFrame();

                    Application.Idle += new EventHandler(FrameGrabber);
                    btnStart.Enabled = false;
                }
            }
                
        }


    }
}
