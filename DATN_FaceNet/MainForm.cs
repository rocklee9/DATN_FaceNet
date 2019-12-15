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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            var frmLogin = new LoginForm();
            frmLogin.ShowDialog();
            if (Common.User.Role != 1)
            {
                trainningToolStripMenuItem.Visible = false;
                nhậnDiệnToolStripMenuItem3.Visible = false;
                quảnLýTàiKhoảnToolStripMenuItem1.Visible = false;
            }
        }

        private void OpenForm(Form form)
        {
            foreach (var frm in MdiChildren)
                if (frm.Name == form.Name)
                {
                    form.Dispose();
                    frm.Activate();
                    return;
                }
            form.Show();
        }

        

        

        

        
        

        private void quảnLýHìnhẢnhToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new ImagerManagementForm());
        }

        private void quảnLýTàiKhoảnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenForm(new AccountManagementForm());
        }

        private void thêmHìnhẢnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new TrainningForm());
        }

        private void trainningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                //pictureInput.Image=
                // Goi len server va tra ve ket qua
                String server_ip = "http://localhost";
                String server_path = server_ip + ":5000/train";
                var postData = "image=";
                var data = Encoding.ASCII.GetBytes(postData);
                var request = (HttpWebRequest)WebRequest.Create(server_path);
                request.Timeout = 20000;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
                var response = (HttpWebResponse)request.GetResponse();

                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();


                MessageBox.Show(responseString);
            }
            catch
            {
                MessageBox.Show("Kiểm tra lại kết nối");
            }
        }

        private void nhậnDiệnToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenForm(new RecognitionForm());
        }
    }
}
