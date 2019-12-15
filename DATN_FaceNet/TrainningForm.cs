using DATN_FaceNet.Controller;
using Emgu.CV;
using FaceRecognition.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATN_FaceNet
{
    public partial class TrainningForm : Form
    {
        private OpenFileDialog _openFileDialog;
        //Capure  grabber;
        public TrainningForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();

            cbbTypeRecognition.DataSource = TrainingController.Instance.GetOptionTraining();
            cbbTypeRecognition.DisplayMember = "Name";
            cbbTypeRecognition.ValueMember = "Id";
            cbbTypeRecognition.SelectedValue = 1;
            this.cbbTypeRecognition.SelectedIndexChanged += new System.EventHandler(this.cbbTypeRecognition_SelectedIndexChanged);
        }

        private void nhậnDiệnToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnOpen_Click(object sender, EventArgs e)
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

        private void btnTraining_Click(object sender, EventArgs e)
        {
            
            String B64 = ConvertImageToBase64String(pictureInput.Image);
            // Goi len server va tra ve ket qua
            String server_ip = "http://localhost";
            String server_path = server_ip + ":5000/trainning";
            String retStr = sendPOST(server_path, B64);
            string[] arrListStr = retStr.Split('_');
            if (string.Compare(arrListStr[0].ToString(), "training thành công") == 0)
            {
                int kq = TrainingController.Instance.AddImager(arrListStr[1].ToString());
                if (kq == 1)
                {
                    MessageBox.Show("Thêm hình ảnh thành công !");
                }
                else
                {
                    MessageBox.Show("Xảy ra lỗi trong quá trình lưu");
                }
            }
            else
            {
                MessageBox.Show(arrListStr[0].ToString());
            }
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
        private String EscapeData(String B64)
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
        private String sendPOST(String url, String B64)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Timeout = 100000;
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

        private void btnStart_Click(object sender, EventArgs e)
        {
            //grabber = new VideoCapture();
            //pictureInput.Image=grabber.QueryFrame();
        }

        private void btnChupAnh_Click(object sender, EventArgs e)
        {
            //pictureInput.Image = grabber.ImageGrabbed();
        }

        private void cbbTypeRecognition_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(cbbTypeRecognition.SelectedValue) == 1)
            {
                btnOpen.Visible = false;
                btnTraining.Visible = false;
                btnChupAnh.Visible = true;
                btnStart.Visible = true;

            }
            else
            {
                btnOpen.Visible = true;
                btnTraining.Visible = true;
                btnChupAnh.Visible = false;
                btnStart.Visible = false;

            }
        }
    }
}
