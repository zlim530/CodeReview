namespace ArcSoftFace
{
    partial class CheckInfoManage
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCMSearch = new System.Windows.Forms.Button();
            this.txtCMNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox图像 = new System.Windows.Forms.PictureBox();
            this.txtCMUtime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCMCtime = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCMId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1CI = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCIName = new System.Windows.Forms.TextBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.feature = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.stu_info_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox图像)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1CI)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCIName);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnCMSearch);
            this.groupBox1.Controls.Add(this.txtCMNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(188, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(757, 92);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找";
            // 
            // btnCMSearch
            // 
            this.btnCMSearch.Location = new System.Drawing.Point(631, 27);
            this.btnCMSearch.Name = "btnCMSearch";
            this.btnCMSearch.Size = new System.Drawing.Size(101, 40);
            this.btnCMSearch.TabIndex = 7;
            this.btnCMSearch.Text = "查找";
            this.btnCMSearch.UseVisualStyleBackColor = true;
            this.btnCMSearch.Click += new System.EventHandler(this.btnCMSearch_Click);
            // 
            // txtCMNumber
            // 
            this.txtCMNumber.Location = new System.Drawing.Point(111, 33);
            this.txtCMNumber.Name = "txtCMNumber";
            this.txtCMNumber.Size = new System.Drawing.Size(179, 25);
            this.txtCMNumber.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "学号：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.btnSelect);
            this.groupBox2.Controls.Add(this.pictureBox图像);
            this.groupBox2.Controls.Add(this.txtCMUtime);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.txtCMCtime);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtCMId);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(188, 151);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(757, 255);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "人脸信息";
            // 
            // pictureBox图像
            // 
            this.pictureBox图像.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox图像.Location = new System.Drawing.Point(480, 24);
            this.pictureBox图像.Name = "pictureBox图像";
            this.pictureBox图像.Size = new System.Drawing.Size(218, 174);
            this.pictureBox图像.TabIndex = 19;
            this.pictureBox图像.TabStop = false;
            // 
            // txtCMUtime
            // 
            this.txtCMUtime.Location = new System.Drawing.Point(165, 137);
            this.txtCMUtime.Name = "txtCMUtime";
            this.txtCMUtime.Size = new System.Drawing.Size(185, 25);
            this.txtCMUtime.TabIndex = 18;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(77, 147);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 17;
            this.label8.Text = "更新时间：";
            // 
            // txtCMCtime
            // 
            this.txtCMCtime.Location = new System.Drawing.Point(165, 98);
            this.txtCMCtime.Name = "txtCMCtime";
            this.txtCMCtime.Size = new System.Drawing.Size(185, 25);
            this.txtCMCtime.TabIndex = 16;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(77, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 15;
            this.label7.Text = "创建时间：";
            // 
            // txtCMId
            // 
            this.txtCMId.Location = new System.Drawing.Point(165, 61);
            this.txtCMId.Name = "txtCMId";
            this.txtCMId.Size = new System.Drawing.Size(185, 25);
            this.txtCMId.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(77, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "编号：";
            // 
            // dataGridView1CI
            // 
            this.dataGridView1CI.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1CI.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1CI.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.create_time,
            this.update_time,
            this.feature,
            this.dataGridViewImageColumn1,
            this.stu_info_id,
            this.name});
            this.dataGridView1CI.Location = new System.Drawing.Point(25, 426);
            this.dataGridView1CI.Name = "dataGridView1CI";
            this.dataGridView1CI.RowHeadersWidth = 51;
            this.dataGridView1CI.RowTemplate.Height = 27;
            this.dataGridView1CI.Size = new System.Drawing.Size(1072, 346);
            this.dataGridView1CI.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(325, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "姓名：";
            // 
            // txtCIName
            // 
            this.txtCIName.Location = new System.Drawing.Point(383, 33);
            this.txtCIName.Name = "txtCIName";
            this.txtCIName.Size = new System.Drawing.Size(179, 25);
            this.txtCIName.TabIndex = 9;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(480, 205);
            this.btnSelect.Margin = new System.Windows.Forms.Padding(4);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(100, 29);
            this.btnSelect.TabIndex = 20;
            this.btnSelect.Text = "选择相片";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(598, 205);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 29);
            this.btnSave.TabIndex = 21;
            this.btnSave.Text = "保存相片";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "编号";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.Width = 75;
            // 
            // create_time
            // 
            this.create_time.DataPropertyName = "create_time";
            this.create_time.HeaderText = "创建时间";
            this.create_time.MinimumWidth = 6;
            this.create_time.Name = "create_time";
            this.create_time.Width = 125;
            // 
            // update_time
            // 
            this.update_time.DataPropertyName = "update_time";
            this.update_time.HeaderText = "更新时间";
            this.update_time.MinimumWidth = 6;
            this.update_time.Name = "update_time";
            this.update_time.Width = 125;
            // 
            // feature
            // 
            this.feature.DataPropertyName = "feature";
            this.feature.HeaderText = "人脸特征";
            this.feature.MinimumWidth = 6;
            this.feature.Name = "feature";
            this.feature.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.feature.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.feature.Visible = false;
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "image";
            this.dataGridViewImageColumn1.HeaderText = "图像";
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // stu_info_id
            // 
            this.stu_info_id.DataPropertyName = "stu_info_id";
            this.stu_info_id.HeaderText = "学号";
            this.stu_info_id.MinimumWidth = 6;
            this.stu_info_id.Name = "stu_info_id";
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "姓名";
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            // 
            // CheckInfoManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1137, 816);
            this.Controls.Add(this.dataGridView1CI);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CheckInfoManage";
            this.Text = "人脸库信息";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox图像)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1CI)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCMNumber;
        private System.Windows.Forms.Button btnCMSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtCMId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCMCtime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCMUtime;
        private System.Windows.Forms.PictureBox pictureBox图像;
        private System.Windows.Forms.DataGridView dataGridView1CI;
        private System.Windows.Forms.TextBox txtCIName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_time;
        private System.Windows.Forms.DataGridViewImageColumn feature;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn stu_info_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;

    }
}