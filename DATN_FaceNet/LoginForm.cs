using DATN_FaceNet.Controller;
using FaceRecognition.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DATN_FaceNet
{
    public partial class LoginForm : Form
    {
        
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var t = LoginController.Instance.IsCheckLogin(txtUsername.Text,txtPassword.Text);
            if (t == 1)
            {
                MessageBox.Show("Không tìm thấy Username, vui lòng kiếm tra lại");
            }else if (t == 2)
            {
                MessageBox.Show("Mật khẩu không đúng, vui lòng kiểm tra lại");
            }
            else
            {
                this.Close();
            }
        }

        private void lbforget_Click(object sender, EventArgs e)
        {
            new SendMailForm().Show();
        }
    }
}
