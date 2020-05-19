namespace OrderSystem
{
    partial class liuyan
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label 星级Label;
            this.orderSystemDataSet = new OrderSystem.OrderSystemDataSet();
            this.liuyanBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.liuyanTableAdapter = new OrderSystem.OrderSystemDataSetTableAdapters.LiuyanTableAdapter();
            this.tableAdapterManager = new OrderSystem.OrderSystemDataSetTableAdapters.TableAdapterManager();
            this.liuyanDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.星级ComboBox = new System.Windows.Forms.ComboBox();
            this.留言RichTextBox = new System.Windows.Forms.RichTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            星级Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.orderSystemDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liuyanBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.liuyanDataGridView)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 星级Label
            // 
            星级Label.AutoSize = true;
            星级Label.Location = new System.Drawing.Point(121, 21);
            星级Label.Name = "星级Label";
            星级Label.Size = new System.Drawing.Size(35, 12);
            星级Label.TabIndex = 24;
            星级Label.Text = "星级:";
            // 
            // orderSystemDataSet
            // 
            this.orderSystemDataSet.DataSetName = "OrderSystemDataSet";
            this.orderSystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // liuyanBindingSource
            // 
            this.liuyanBindingSource.DataMember = "Liuyan";
            this.liuyanBindingSource.DataSource = this.orderSystemDataSet;
            // 
            // liuyanTableAdapter
            // 
            this.liuyanTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AdminTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.DetailMenuTableAdapter = null;
            this.tableAdapterManager.LiuyanTableAdapter = this.liuyanTableAdapter;
            this.tableAdapterManager.MenuTableAdapter = null;
            this.tableAdapterManager.Order1TableAdapter = null;
            this.tableAdapterManager.salesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = OrderSystem.OrderSystemDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.VipTableAdapter = null;
            // 
            // liuyanDataGridView
            // 
            this.liuyanDataGridView.AutoGenerateColumns = false;
            this.liuyanDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.liuyanDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4});
            this.liuyanDataGridView.DataSource = this.liuyanBindingSource;
            this.liuyanDataGridView.Location = new System.Drawing.Point(15, 215);
            this.liuyanDataGridView.Name = "liuyanDataGridView";
            this.liuyanDataGridView.RowTemplate.Height = 23;
            this.liuyanDataGridView.Size = new System.Drawing.Size(494, 220);
            this.liuyanDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "序号";
            this.dataGridViewTextBoxColumn1.HeaderText = "序号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "用户名";
            this.dataGridViewTextBoxColumn2.HeaderText = "用户名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "留言";
            this.dataGridViewTextBoxColumn3.HeaderText = "留言";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 150;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "星级";
            this.dataGridViewTextBoxColumn4.HeaderText = "星级";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 457);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(531, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(80, 17);
            this.toolStripStatusLabel1.Text = "餐馆订餐系统";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(202, 17);
            this.toolStripStatusLabel2.Spring = true;
            this.toolStripStatusLabel2.Text = "当前用户：";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(202, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // 星级ComboBox
            // 
            this.星级ComboBox.FormattingEnabled = true;
            this.星级ComboBox.Items.AddRange(new object[] {
            "5",
            "4",
            "3",
            "2",
            "1"});
            this.星级ComboBox.Location = new System.Drawing.Point(163, 18);
            this.星级ComboBox.Name = "星级ComboBox";
            this.星级ComboBox.Size = new System.Drawing.Size(109, 20);
            this.星级ComboBox.TabIndex = 26;
            // 
            // 留言RichTextBox
            // 
            this.留言RichTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.留言RichTextBox.Location = new System.Drawing.Point(15, 58);
            this.留言RichTextBox.Name = "留言RichTextBox";
            this.留言RichTextBox.Size = new System.Drawing.Size(396, 127);
            this.留言RichTextBox.TabIndex = 25;
            this.留言RichTextBox.Text = "";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(431, 150);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 35);
            this.button4.TabIndex = 23;
            this.button4.Text = "提交评价";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(430, 77);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(88, 35);
            this.button3.TabIndex = 22;
            this.button3.Text = "重新评价";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("幼圆", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label4.Location = new System.Drawing.Point(12, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 16);
            this.label4.TabIndex = 21;
            this.label4.Text = "我也来评价：";
            // 
            // liuyan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 479);
            this.Controls.Add(星级Label);
            this.Controls.Add(this.星级ComboBox);
            this.Controls.Add(this.留言RichTextBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.liuyanDataGridView);
            this.Name = "liuyan";
            this.Text = "liuyan";
            this.Load += new System.EventHandler(this.liuyan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.orderSystemDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liuyanBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.liuyanDataGridView)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OrderSystemDataSet orderSystemDataSet;
        private System.Windows.Forms.BindingSource liuyanBindingSource;
        private OrderSystem.OrderSystemDataSetTableAdapters.LiuyanTableAdapter liuyanTableAdapter;
        private OrderSystem.OrderSystemDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView liuyanDataGridView;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ComboBox 星级ComboBox;
        private System.Windows.Forms.RichTextBox 留言RichTextBox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}