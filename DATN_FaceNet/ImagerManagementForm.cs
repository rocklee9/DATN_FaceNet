using DATN_FaceNet.Controller;
using DATN_FaceNet.Model.Schema;
using FaceRecognition.Common;
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
    public partial class ImagerManagementForm : Form
    {
        private int index = 0;
        public ImagerManagementForm()
        {
            this.WindowState = FormWindowState.Maximized;
            InitializeComponent();
            Init();
            LoadImager(Common.User.Id);
        }

        public void Init()
        {
            if (Common.User.Role == 1)
            {
                var _listdata = AccountController.Instance.getAccounts();
                int i = 1;
                foreach (var data in _listdata)
                {
                    dgvAccount.Rows.Add(data.Id, i++, data.UserName, data.Name);
                }
            }
            else
            {
                dgvAccount.Rows.Add(Common.User.Id, 1, Common.User.UserName, Common.User.Name);
            }
        }

        public void LoadImager(int Id)
        {
            var ImageList = ImagerManagementController.Instance.getImagers(Id);
            int i = 1;
            dgvImager.Rows.Clear();
            foreach (var data in ImageList)
            {
                dgvImager.Rows.Add(data.Id,data.Base64Img, i, "Hình "+i++);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            var Id = Convert.ToInt32(this.dgvImager.SelectedRows[0].Cells[0].Value);
            ImagerManagementController.Instance.DelImager(Id);
            var id = Convert.ToInt32(this.dgvAccount.SelectedRows[0].Cells[0].Value); 
            LoadImager(id);

            //Nếu còn ảnh thì load ảnh đẩu tiên
            Load_Anh(0);
            

        }

        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var id = Convert.ToInt32(this.dgvAccount.SelectedRows[0].Cells[0].Value); 
            LoadImager(id);
            btn_next.Visible = false;
            btn_prev.Visible = false;
            picImager.Image = null;
        }

        private void dgvImager_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var base64Img = this.dgvImager.SelectedRows[0].Cells["Base64Img"].Value.ToString();
            Image image = Common.Base64ToImage(base64Img);
            picImager.Image = image;
            btnDel.Enabled = true;
            index= this.dgvImager.CurrentCell.RowIndex;

            btn_next.Visible = true;
            btn_prev.Visible = true;
        }

        private void btn_prev_Click(object sender, EventArgs e)
        {
            int leght=this.dgvImager.ColumnCount;
            if (index == 0)
            {
                
                Load_Anh(leght-1);
                dgvImager.Rows[index].Selected = false;
                dgvImager.Rows[leght-1].Selected = true;
                index = leght-1;

            }
            else
            {
                
                Load_Anh(index-1);
                dgvImager.Rows[index].Selected = false;
                dgvImager.Rows[index-1].Selected = true;
                index--;
            }

        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            int leght = this.dgvImager.RowCount;
            if (index == leght-1)
            {
                Load_Anh(0);
                dgvImager.Rows[leght-1].Selected = false;
                dgvImager.Rows[0].Selected = true;
                index = 0;
            }
            else
            {
                Load_Anh(index + 1);
                dgvImager.Rows[index].Selected = false;
                dgvImager.Rows[index + 1].Selected = true;
                index++;
            }
        }

        void Load_Anh(int index)
        {
            var base64Img = this.dgvImager.Rows[index].Cells["Base64Img"].Value.ToString();
            if (base64Img != null)
            {
                Image image = Common.Base64ToImage(base64Img);
                picImager.Image = image;
                btnDel.Enabled = true;
            }
        }
    }
}
