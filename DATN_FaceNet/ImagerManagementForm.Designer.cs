namespace DATN_FaceNet
{
    partial class ImagerManagementForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.group = new System.Windows.Forms.GroupBox();
            this.dgvAccount = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgvImager = new System.Windows.Forms.DataGridView();
            this.ID_Imager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Base64Img = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STT_Imager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Imager = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.picImager = new System.Windows.Forms.PictureBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btn_next = new System.Windows.Forms.Button();
            this.btn_prev = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvImager)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImager)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // group
            // 
            this.group.Controls.Add(this.dgvAccount);
            this.group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.group.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.group.Location = new System.Drawing.Point(0, 0);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(459, 561);
            this.group.TabIndex = 0;
            this.group.TabStop = false;
            this.group.Text = "Tài khoản";
            // 
            // dgvAccount
            // 
            this.dgvAccount.AllowUserToAddRows = false;
            this.dgvAccount.AllowUserToDeleteRows = false;
            this.dgvAccount.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAccount.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvAccount.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAccount.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.STT,
            this.UserName,
            this.Name});
            this.dgvAccount.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAccount.Location = new System.Drawing.Point(3, 23);
            this.dgvAccount.Name = "dgvAccount";
            this.dgvAccount.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAccount.Size = new System.Drawing.Size(453, 535);
            this.dgvAccount.TabIndex = 0;
            this.dgvAccount.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvAccount_CellClick);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // STT
            // 
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            // 
            // UserName
            // 
            this.UserName.HeaderText = "UserName";
            this.UserName.MinimumWidth = 170;
            this.UserName.Name = "UserName";
            // 
            // Name
            // 
            this.Name.HeaderText = "Name";
            this.Name.MinimumWidth = 170;
            this.Name.Name = "Name";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgvImager);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(474, 561);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách hình ảnh";
            // 
            // dgvImager
            // 
            this.dgvImager.AllowUserToAddRows = false;
            this.dgvImager.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvImager.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvImager.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvImager.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID_Imager,
            this.Base64Img,
            this.STT_Imager,
            this.Imager});
            this.dgvImager.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvImager.Location = new System.Drawing.Point(3, 23);
            this.dgvImager.Name = "dgvImager";
            this.dgvImager.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvImager.Size = new System.Drawing.Size(468, 535);
            this.dgvImager.TabIndex = 1;
            this.dgvImager.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvImager_CellClick);
            // 
            // ID_Imager
            // 
            this.ID_Imager.HeaderText = "Id";
            this.ID_Imager.Name = "ID_Imager";
            this.ID_Imager.Visible = false;
            // 
            // Base64Img
            // 
            this.Base64Img.HeaderText = "Base64Img";
            this.Base64Img.Name = "Base64Img";
            this.Base64Img.Visible = false;
            // 
            // STT_Imager
            // 
            this.STT_Imager.HeaderText = "STT";
            this.STT_Imager.MinimumWidth = 50;
            this.STT_Imager.Name = "STT_Imager";
            // 
            // Imager
            // 
            this.Imager.HeaderText = "Hình";
            this.Imager.MinimumWidth = 290;
            this.Imager.Name = "Imager";
            // 
            // picImager
            // 
            this.picImager.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.picImager.Location = new System.Drawing.Point(79, 34);
            this.picImager.Name = "picImager";
            this.picImager.Size = new System.Drawing.Size(160, 160);
            this.picImager.TabIndex = 2;
            this.picImager.TabStop = false;
            // 
            // btnAdd
            // 
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.Location = new System.Drawing.Point(36, 342);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(118, 36);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Thêm hình";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(171, 342);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(111, 36);
            this.btnDel.TabIndex = 4;
            this.btnDel.Text = "Xóa hình";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.group);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(459, 561);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btn_next);
            this.panel2.Controls.Add(this.btn_prev);
            this.panel2.Controls.Add(this.picImager);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.btnDel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(933, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(309, 561);
            this.panel2.TabIndex = 6;
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(164, 224);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(75, 23);
            this.btn_next.TabIndex = 6;
            this.btn_next.Text = ">>";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Visible = false;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // btn_prev
            // 
            this.btn_prev.Location = new System.Drawing.Point(79, 224);
            this.btn_prev.Name = "btn_prev";
            this.btn_prev.Size = new System.Drawing.Size(75, 23);
            this.btn_prev.TabIndex = 5;
            this.btn_prev.Text = "<<";
            this.btn_prev.UseVisualStyleBackColor = true;
            this.btn_prev.Visible = false;
            this.btn_prev.Click += new System.EventHandler(this.btn_prev_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(459, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(474, 561);
            this.panel3.TabIndex = 7;
            // 
            // ImagerManagementForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1242, 561);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Text = "ImagerManagementForm";
            this.group.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAccount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvImager)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picImager)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.PictureBox picImager;
        private System.Windows.Forms.DataGridView dgvAccount;
        private System.Windows.Forms.DataGridView dgvImager;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_Imager;
        private System.Windows.Forms.DataGridViewTextBoxColumn Base64Img;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT_Imager;
        private System.Windows.Forms.DataGridViewTextBoxColumn Imager;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_next;
        private System.Windows.Forms.Button btn_prev;
    }
}