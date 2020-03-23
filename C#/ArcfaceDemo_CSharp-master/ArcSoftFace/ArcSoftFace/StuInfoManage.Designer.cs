namespace ArcSoftFace
{
    partial class StuInfoManage
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StuInfoManage));
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView1Stu = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtUtime = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtCtime = new System.Windows.Forms.TextBox();
            this.ComboBoxSex = new System.Windows.Forms.ComboBox();
            this.ComboBoxCheck = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtNumberS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeleted = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_checked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Stu)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(669, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 40);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "查找";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(101, 43);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(147, 25);
            this.txtSearch.TabIndex = 2;
            // 
            // dataGridView1Stu
            // 
            this.dataGridView1Stu.AllowUserToAddRows = false;
            this.dataGridView1Stu.AllowUserToDeleteRows = false;
            this.dataGridView1Stu.AllowUserToResizeColumns = false;
            this.dataGridView1Stu.AllowUserToResizeRows = false;
            this.dataGridView1Stu.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1Stu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1Stu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.create_time,
            this.update_time,
            this.sex,
            this.name,
            this.is_checked});
            this.dataGridView1Stu.Location = new System.Drawing.Point(12, 285);
            this.dataGridView1Stu.Name = "dataGridView1Stu";
            this.dataGridView1Stu.RowHeadersWidth = 51;
            this.dataGridView1Stu.RowTemplate.Height = 27;
            this.dataGridView1Stu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1Stu.Size = new System.Drawing.Size(1107, 401);
            this.dataGridView1Stu.TabIndex = 0;
            this.dataGridView1Stu.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1Stu_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAdd);
            this.groupBox1.Controls.Add(this.btnDeleted);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Controls.Add(this.txtNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Location = new System.Drawing.Point(50, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(981, 121);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找";
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(807, 24);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(129, 40);
            this.buttonOpen.TabIndex = 6;
            this.buttonOpen.Text = "查看人脸库信息";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(419, 43);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(179, 25);
            this.txtNumber.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "学号：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 3;
            this.label1.Text = "姓名：";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(835, 42);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(101, 36);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtUtime);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.btnSave);
            this.groupBox2.Controls.Add(this.txtCtime);
            this.groupBox2.Controls.Add(this.ComboBoxSex);
            this.groupBox2.Controls.Add(this.ComboBoxCheck);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtNumberS);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtName);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(50, 153);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(981, 111);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "学生信息";
            // 
            // txtUtime
            // 
            this.txtUtime.Location = new System.Drawing.Point(625, 73);
            this.txtUtime.Name = "txtUtime";
            this.txtUtime.Size = new System.Drawing.Size(185, 25);
            this.txtUtime.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(537, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 15);
            this.label8.TabIndex = 15;
            this.label8.Text = "更新时间：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(537, 37);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 14;
            this.label7.Text = "创建时间：";
            // 
            // txtCtime
            // 
            this.txtCtime.Location = new System.Drawing.Point(625, 32);
            this.txtCtime.Name = "txtCtime";
            this.txtCtime.Size = new System.Drawing.Size(185, 25);
            this.txtCtime.TabIndex = 13;
            // 
            // ComboBoxSex
            // 
            this.ComboBoxSex.FormattingEnabled = true;
            this.ComboBoxSex.Items.AddRange(new object[] {
            "",
            "男",
            "女"});
            this.ComboBoxSex.Location = new System.Drawing.Point(101, 75);
            this.ComboBoxSex.Name = "ComboBoxSex";
            this.ComboBoxSex.Size = new System.Drawing.Size(147, 23);
            this.ComboBoxSex.TabIndex = 12;
            // 
            // ComboBoxCheck
            // 
            this.ComboBoxCheck.FormattingEnabled = true;
            this.ComboBoxCheck.Items.AddRange(new object[] {
            "",
            "否",
            "是"});
            this.ComboBoxCheck.Location = new System.Drawing.Point(342, 75);
            this.ComboBoxCheck.Name = "ComboBoxCheck";
            this.ComboBoxCheck.Size = new System.Drawing.Size(179, 23);
            this.ComboBoxCheck.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(254, 78);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(82, 15);
            this.label6.TabIndex = 9;
            this.label6.Text = "是否签到：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 78);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 7;
            this.label5.Text = "性别：";
            // 
            // txtNumberS
            // 
            this.txtNumberS.Location = new System.Drawing.Point(342, 34);
            this.txtNumberS.Name = "txtNumberS";
            this.txtNumberS.Size = new System.Drawing.Size(179, 25);
            this.txtNumberS.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(284, 37);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 15);
            this.label4.TabIndex = 6;
            this.label4.Text = "学号：";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 34);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(147, 25);
            this.txtName.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "姓名：";
            // 
            // btnDeleted
            // 
            this.btnDeleted.Location = new System.Drawing.Point(807, 75);
            this.btnDeleted.Name = "btnDeleted";
            this.btnDeleted.Size = new System.Drawing.Size(101, 40);
            this.btnDeleted.TabIndex = 7;
            this.btnDeleted.Text = "删除行";
            this.btnDeleted.UseVisualStyleBackColor = true;
            this.btnDeleted.Click += new System.EventHandler(this.btnDeleted_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(669, 75);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(101, 40);
            this.btnAdd.TabIndex = 8;
            this.btnAdd.Text = "添加行";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.id.DefaultCellStyle = dataGridViewCellStyle1;
            this.id.HeaderText = "学号";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
            this.id.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.id.Width = 125;
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
            // sex
            // 
            this.sex.DataPropertyName = "sex";
            this.sex.HeaderText = "性别";
            this.sex.MinimumWidth = 6;
            this.sex.Name = "sex";
            this.sex.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.sex.Width = 125;
            // 
            // name
            // 
            this.name.DataPropertyName = "name";
            this.name.HeaderText = "姓名";
            this.name.MinimumWidth = 6;
            this.name.Name = "name";
            this.name.Width = 125;
            // 
            // is_checked
            // 
            this.is_checked.DataPropertyName = "is_checked";
            this.is_checked.HeaderText = "是否签到";
            this.is_checked.MinimumWidth = 6;
            this.is_checked.Name = "is_checked";
            this.is_checked.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.is_checked.Width = 125;
            // 
            // StuInfoManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1131, 698);
            this.Controls.Add(this.dataGridView1Stu);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StuInfoManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "学生信息管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Stu)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.DataGridView dataGridView1Stu;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtNumberS;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox ComboBoxCheck;
        private System.Windows.Forms.ComboBox ComboBoxSex;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtCtime;
        private System.Windows.Forms.TextBox txtUtime;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.Button btnDeleted;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_checked;
    }
}