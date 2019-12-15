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
    public partial class AddAccountForm : Form
    {
        private bool IsAdd = true;
        public AddAccountForm(NewAccount newAccount = null)
        {
            InitializeComponent();
            if (newAccount != null)
            {
                Init(newAccount);
                IsAdd = false;
            }
        }

        public void Init(NewAccount newAccount)
        {
            txtName.Text = newAccount.Name;
            txtUsername.Text = newAccount.UserName;
            txtPassword.Text = newAccount.Password;
            txtFullName.Text = newAccount.FullName;
            txtEmail.Text = newAccount.Email;
            dtBirthday.Value = newAccount.Birthday;
            if (newAccount.Gender)
            {
                rdNam.Checked = true;
            }
            else
            {
                rdNu.Checked = true;
            }
            txtUsername.Enabled = false;
            txtPassword.Enabled = false;
            txtEmail.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int kq = 0;
            if (IsAdd)
            {
                kq = AccountController.Instance.AddAccount(new Model.Schema.NewAccount
                {
                    UserName = txtUsername.Text,
                    Password = txtPassword.Text,
                    Email = txtEmail.Text,
                    Name = txtName.Text,
                    FullName = txtFullName.Text,
                    Gender = rdNam.Checked ? true : false,
                    Birthday = dtBirthday.Value
                });
            }
            else
            {
                kq = AccountController.Instance.EditAccount(new Model.Schema.NewAccount
                {
                    UserName = txtUsername.Text,
                    Password = txtPassword.Text,
                    Email = txtEmail.Text,
                    Name = txtName.Text,
                    FullName = txtFullName.Text,
                    Gender = rdNam.Checked ? true : false,
                    Birthday = dtBirthday.Value
                });
            }

            if (kq == 0)
            {
                MessageBox.Show("Đã xảy ra lỗi trong quá trình lưu dứ liệu, vui lòng kiểm tra lại !");
            }
            else if (kq == 2)
            {
                MessageBox.Show("Username đã tồn tại, vui lòng chọn Username khác !");
            }
            else if (kq == 3)
            {
                MessageBox.Show("Email đã tồn tại, vui lòng chọn Email khác !");
            }
            else
            {
                MessageBox.Show("Thêm tài khoản thành công !");
                Close();
            }
        }
    }
}
