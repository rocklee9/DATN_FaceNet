using DATN_FaceNet.Controller;
using System;
using System.Windows.Forms;

namespace DATN_FaceNet
{
    public partial class AccountManagementForm : Form
    {
        
        public AccountManagementForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                var _listdata = AccountController.Instance.getAccounts();
                int i = 1;
                dgvAccounts.Rows.Clear();
                foreach (var data in _listdata)
                {
                    dgvAccounts.Rows.Add(data.Id, i++, data.UserName, data.Name, data.FullName, data.Email, data.Gender == true ? "Nam" : "Nữ", data.Birthday.ToString("dd/MM/yyyy"));
                }
            }
            catch
            {
                MessageBox.Show("Vui lòng kiểm tra lại dữ liệu !");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            var dialogAddAccountForm = new AddAccountForm();
            dialogAddAccountForm.ShowDialog();
            LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var dialogAddAccountForm = new AddAccountForm(new Model.Schema.NewAccount
            {
                UserName = dgvAccounts.SelectedRows[0].Cells[2].Value.ToString(),
                Password = "********",
                Email= dgvAccounts.SelectedRows[0].Cells[5].Value.ToString(),
                Name= dgvAccounts.SelectedRows[0].Cells[3].Value.ToString(),
                FullName= dgvAccounts.SelectedRows[0].Cells[4].Value.ToString(),
                Birthday= Convert.ToDateTime(dgvAccounts.SelectedRows[0].Cells[7].Value.ToString()),
                Gender= dgvAccounts.SelectedRows[0].Cells[6].Value.ToString()=="Nam"?true:false
            }); 
            dialogAddAccountForm.ShowDialog();
            LoadData();
        }

        private void dgvAccounts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnDel.Enabled = true;
            btnEdit.Enabled = true;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show(@"Bạn có chắc chắn xóa dữ liệu này ?", @"Thông báo", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) != DialogResult.Yes) return;
                var id = Convert.ToInt32(this.dgvAccounts.SelectedRows[0].Cells[0].Value);
                AccountController.Instance.Delete(id);
                LoadData();
            }
            catch
            {
                MessageBox.Show("Vui lòng kiểm tra lại dữ liệu !");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            var _listdata = AccountController.Instance.Search(txtSearch.Text);
            int i = 1;
            dgvAccounts.Rows.Clear();
            foreach (var data in _listdata)
            {
                dgvAccounts.Rows.Add(data.Id, i++, data.UserName, data.Name, data.FullName, data.Email, data.Gender == true ? "Nam" : "Nữ", data.Birthday.ToString("dd/MM/yyyy"));
            }
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            var dialogChangePassForm = new ChangePassForm();
            dialogChangePassForm.ShowDialog();
        }
    }
}
