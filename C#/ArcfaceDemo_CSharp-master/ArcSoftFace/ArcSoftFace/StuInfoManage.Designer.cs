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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StuInfoManage));
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.dataGridView1Stu = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.create_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.update_time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.is_checked = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Stu)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(740, 37);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(101, 33);
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
            this.dataGridView1Stu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1Stu.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.create_time,
            this.update_time,
            this.sex,
            this.name,
            this.is_checked});
            this.dataGridView1Stu.Location = new System.Drawing.Point(12, 313);
            this.dataGridView1Stu.Name = "dataGridView1Stu";
            this.dataGridView1Stu.RowHeadersWidth = 51;
            this.dataGridView1Stu.RowTemplate.Height = 27;
            this.dataGridView1Stu.Size = new System.Drawing.Size(1046, 335);
            this.dataGridView1Stu.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNumber);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtSearch);
            this.groupBox1.Location = new System.Drawing.Point(50, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 98);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找";
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
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(361, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "学号：";
            // 
            // txtNumber
            // 
            this.txtNumber.Location = new System.Drawing.Point(419, 43);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(179, 25);
            this.txtNumber.TabIndex = 5;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.HeaderText = "学号";
            this.id.MinimumWidth = 6;
            this.id.Name = "id";
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
            this.update_time.ToolTipText = "签到成功则更新";
            this.update_time.Width = 125;
            // 
            // sex
            // 
            this.sex.DataPropertyName = "sex";
            this.sex.HeaderText = "性别";
            this.sex.MinimumWidth = 6;
            this.sex.Name = "sex";
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
            this.is_checked.Width = 125;
            // 
            // StuInfoManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 660);
            this.Controls.Add(this.dataGridView1Stu);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StuInfoManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "学生信息管理";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1Stu)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn create_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn update_time;
        private System.Windows.Forms.DataGridViewTextBoxColumn sex;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn is_checked;
    }
}