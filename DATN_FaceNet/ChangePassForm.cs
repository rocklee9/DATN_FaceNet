using DATN_FaceNet.Controller;
using DATN_FaceNet.Model.Schema;
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
    public partial class ChangePassForm : Form
    {
        public ChangePassForm()
        {
            InitializeComponent();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            if (txtPass.Text != txtPass2.Text)
            {
                MessageBox.Show("mật khẩu và nhập lại mật khẩu không giống nhau, vui lòng kiểm tra lại !");
                return;
            }
            var kq = AccountController.Instance.ChangePass(new ChangePass
            {
                OldPass=txtOldPass.Text,
                Pass=txtPass.Text,
                Pass_2=txtPass2.Text
            });
            if (kq == 0)
            {
                MessageBox.Show("Đã xảy ra trong quá trình thay đổi, vui lòng kiểm tra lại !");
            }else if (kq==2)
            {
                MessageBox.Show("Mật khẩu cũ không đúng, vui lòng kiểm tra lại");
            }
            else
            {
                MessageBox.Show("Thay đổi mật khẩu thành công");
                Close();
            }
        }
    }
}
