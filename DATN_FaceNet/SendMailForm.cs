using DATN_FaceNet.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DATN_FaceNet
{
    public partial class SendMailForm : Form
    {
        public SendMailForm()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var send = LoginController.Instance.IsSendMail(txtEmail.Text,txtUsername.Text);
            if (send)
            {
                MessageBox.Show("Đã gửi mật khẩu mới vào mail của bạn, vui lòng kiểm tra mail !");
            }
            else
            {
                MessageBox.Show("Email hoặc Username không tồn tại !");
            }
        }
    }
}
