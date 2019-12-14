namespace OrderSystem
{
    partial class MenuManage
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
            System.Windows.Forms.Label 编号Label;
            System.Windows.Forms.Label 菜名Label;
            System.Windows.Forms.Label 菜系Label;
            System.Windows.Forms.Label 口味Label;
            System.Windows.Forms.Label 原材料Label;
            System.Windows.Forms.Label 荤素Label;
            System.Windows.Forms.Label 价格Label;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuManage));
            this.menuBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.menuBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.orderSystemDataSet = new OrderSystem.OrderSystemDataSet();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.menuBindingNavigatorSaveItem = new System.Windows.Forms.ToolStripButton();
            this.menuDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.图片PictureBox = new System.Windows.Forms.PictureBox();
            this.价格TextBox = new System.Windows.Forms.TextBox();
            this.荤素ComboBox = new System.Windows.Forms.ComboBox();
            this.原材料TextBox = new System.Windows.Forms.TextBox();
            this.口味ComboBox = new System.Windows.Forms.ComboBox();
            this.菜系ComboBox = new System.Windows.Forms.ComboBox();
            this.菜名TextBox = new System.Windows.Forms.TextBox();
            this.编号TextBox = new System.Windows.Forms.TextBox();
            this.menuTableAdapter = new OrderSystem.OrderSystemDataSetTableAdapters.MenuTableAdapter();
            this.tableAdapterManager = new OrderSystem.OrderSystemDataSetTableAdapters.TableAdapterManager();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            编号Label = new System.Windows.Forms.Label();
            菜名Label = new System.Windows.Forms.Label();
            菜系Label = new System.Windows.Forms.Label();
            口味Label = new System.Windows.Forms.Label();
            原材料Label = new System.Windows.Forms.Label();
            荤素Label = new System.Windows.Forms.Label();
            价格Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.menuBindingNavigator)).BeginInit();
            this.menuBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderSystemDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.图片PictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // 编号Label
            // 
            编号Label.AutoSize = true;
            编号Label.Location = new System.Drawing.Point(16, 52);
            编号Label.Name = "编号Label";
            编号Label.Size = new System.Drawing.Size(35, 12);
            编号Label.TabIndex = 0;
            编号Label.Text = "编号:";
            // 
            // 菜名Label
            // 
            菜名Label.AutoSize = true;
            菜名Label.Location = new System.Drawing.Point(184, 55);
            菜名Label.Name = "菜名Label";
            菜名Label.Size = new System.Drawing.Size(35, 12);
            菜名Label.TabIndex = 2;
            菜名Label.Text = "菜名:";
            // 
            // 菜系Label
            // 
            菜系Label.AutoSize = true;
            菜系Label.Location = new System.Drawing.Point(363, 55);
            菜系Label.Name = "菜系Label";
            菜系Label.Size = new System.Drawing.Size(35, 12);
            菜系Label.TabIndex = 4;
            菜系Label.Text = "菜系:";
            // 
            // 口味Label
            // 
            口味Label.AutoSize = true;
            口味Label.Location = new System.Drawing.Point(184, 115);
            口味Label.Name = "口味Label";
            口味Label.Size = new System.Drawing.Size(35, 12);
            口味Label.TabIndex = 6;
            口味Label.Text = "口味:";
            // 
            // 原材料Label
            // 
            原材料Label.AutoSize = true;
            原材料Label.Location = new System.Drawing.Point(6, 164);
            原材料Label.Name = "原材料Label";
            原材料Label.Size = new System.Drawing.Size(47, 12);
            原材料Label.TabIndex = 8;
            原材料Label.Text = "原材料:";
            // 
            // 荤素Label
            // 
            荤素Label.AutoSize = true;
            荤素Label.Location = new System.Drawing.Point(363, 115);
            荤素Label.Name = "荤素Label";
            荤素Label.Size = new System.Drawing.Size(35, 12);
            荤素Label.TabIndex = 10;
            荤素Label.Text = "荤素:";
            // 
            // 价格Label
            // 
            价格Label.AutoSize = true;
            价格Label.Location = new System.Drawing.Point(16, 116);
            价格Label.Name = "价格Label";
            价格Label.Size = new System.Drawing.Size(35, 12);
            价格Label.TabIndex = 12;
            价格Label.Text = "价格:";
            // 
            // menuBindingNavigator
            // 
            this.menuBindingNavigator.AddNewItem = this.bindingNavigatorAddNewItem;
            this.menuBindingNavigator.BindingSource = this.menuBindingSource;
            this.menuBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.menuBindingNavigator.DeleteItem = null;
            this.menuBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.menuBindingNavigatorSaveItem});
            this.menuBindingNavigator.Location = new System.Drawing.Point(0, 0);
            this.menuBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.menuBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.menuBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.menuBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.menuBindingNavigator.Name = "menuBindingNavigator";
            this.menuBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.menuBindingNavigator.Size = new System.Drawing.Size(830, 25);
            this.menuBindingNavigator.TabIndex = 0;
            this.menuBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            // 
            // menuBindingSource
            // 
            this.menuBindingSource.DataMember = "Menu";
            this.menuBindingSource.DataSource = this.orderSystemDataSet;
            // 
            // orderSystemDataSet
            // 
            this.orderSystemDataSet.DataSetName = "OrderSystemDataSet";
            this.orderSystemDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // menuBindingNavigatorSaveItem
            // 
            this.menuBindingNavigatorSaveItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.menuBindingNavigatorSaveItem.Image = ((System.Drawing.Image)(resources.GetObject("menuBindingNavigatorSaveItem.Image")));
            this.menuBindingNavigatorSaveItem.Name = "menuBindingNavigatorSaveItem";
            this.menuBindingNavigatorSaveItem.Size = new System.Drawing.Size(23, 22);
            this.menuBindingNavigatorSaveItem.Text = "保存数据";
            this.menuBindingNavigatorSaveItem.Click += new System.EventHandler(this.menuBindingNavigatorSaveItem_Click);
            // 
            // menuDataGridView
            // 
            this.menuDataGridView.AutoGenerateColumns = false;
            this.menuDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.menuDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewImageColumn1});
            this.menuDataGridView.DataSource = this.menuBindingSource;
            this.menuDataGridView.Location = new System.Drawing.Point(12, 355);
            this.menuDataGridView.Name = "menuDataGridView";
            this.menuDataGridView.RowTemplate.Height = 23;
            this.menuDataGridView.Size = new System.Drawing.Size(806, 234);
            this.menuDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "编号";
            this.dataGridViewTextBoxColumn1.HeaderText = "编号";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "菜名";
            this.dataGridViewTextBoxColumn2.HeaderText = "菜名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "菜系";
            this.dataGridViewTextBoxColumn3.HeaderText = "菜系";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "原材料";
            this.dataGridViewTextBoxColumn4.HeaderText = "原材料";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "口味";
            this.dataGridViewTextBoxColumn5.HeaderText = "口味";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "荤素";
            this.dataGridViewTextBoxColumn6.HeaderText = "荤素";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "价格";
            this.dataGridViewTextBoxColumn7.HeaderText = "价格";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.DataPropertyName = "图片";
            this.dataGridViewImageColumn1.HeaderText = "图片";
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBox2);
            this.groupBox1.Controls.Add(this.comboBox1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(806, 61);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查找";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(340, 24);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 10;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "",
            "菜系",
            "荤素",
            "酒水"});
            this.comboBox1.Location = new System.Drawing.Point(79, 23);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 9;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(663, 21);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "查找";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.图片PictureBox);
            this.groupBox2.Controls.Add(价格Label);
            this.groupBox2.Controls.Add(this.价格TextBox);
            this.groupBox2.Controls.Add(荤素Label);
            this.groupBox2.Controls.Add(this.荤素ComboBox);
            this.groupBox2.Controls.Add(原材料Label);
            this.groupBox2.Controls.Add(this.原材料TextBox);
            this.groupBox2.Controls.Add(口味Label);
            this.groupBox2.Controls.Add(this.口味ComboBox);
            this.groupBox2.Controls.Add(菜系Label);
            this.groupBox2.Controls.Add(this.菜系ComboBox);
            this.groupBox2.Controls.Add(菜名Label);
            this.groupBox2.Controls.Add(this.菜名TextBox);
            this.groupBox2.Controls.Add(编号Label);
            this.groupBox2.Controls.Add(this.编号TextBox);
            this.groupBox2.Location = new System.Drawing.Point(12, 110);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(806, 239);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "菜单信息";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(680, 210);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 17;
            this.button3.Text = "删除相片";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(565, 210);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 16;
            this.button2.Text = "选择相片";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // 图片PictureBox
            // 
            this.图片PictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.图片PictureBox.DataBindings.Add(new System.Windows.Forms.Binding("Image", this.menuBindingSource, "图片", true));
            this.图片PictureBox.Location = new System.Drawing.Point(565, 20);
            this.图片PictureBox.Name = "图片PictureBox";
            this.图片PictureBox.Size = new System.Drawing.Size(198, 182);
            this.图片PictureBox.TabIndex = 15;
            this.图片PictureBox.TabStop = false;
            // 
            // 价格TextBox
            // 
            this.价格TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "价格", true));
            this.价格TextBox.Location = new System.Drawing.Point(57, 111);
            this.价格TextBox.Name = "价格TextBox";
            this.价格TextBox.Size = new System.Drawing.Size(100, 21);
            this.价格TextBox.TabIndex = 13;
            // 
            // 荤素ComboBox
            // 
            this.荤素ComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "荤素", true));
            this.荤素ComboBox.FormattingEnabled = true;
            this.荤素ComboBox.Items.AddRange(new object[] {
            "荤菜",
            "素菜",
            "无"});
            this.荤素ComboBox.Location = new System.Drawing.Point(404, 112);
            this.荤素ComboBox.Name = "荤素ComboBox";
            this.荤素ComboBox.Size = new System.Drawing.Size(100, 20);
            this.荤素ComboBox.TabIndex = 11;
            // 
            // 原材料TextBox
            // 
            this.原材料TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "原材料", true));
            this.原材料TextBox.Location = new System.Drawing.Point(57, 161);
            this.原材料TextBox.Name = "原材料TextBox";
            this.原材料TextBox.Size = new System.Drawing.Size(447, 21);
            this.原材料TextBox.TabIndex = 9;
            // 
            // 口味ComboBox
            // 
            this.口味ComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "口味", true));
            this.口味ComboBox.FormattingEnabled = true;
            this.口味ComboBox.Items.AddRange(new object[] {
            "清淡味",
            "麻辣味",
            "香辣味",
            "鲜香味",
            "咸鲜味",
            "微甜味",
            "微苦味",
            "微酸味"});
            this.口味ComboBox.Location = new System.Drawing.Point(225, 113);
            this.口味ComboBox.Name = "口味ComboBox";
            this.口味ComboBox.Size = new System.Drawing.Size(100, 20);
            this.口味ComboBox.TabIndex = 7;
            // 
            // 菜系ComboBox
            // 
            this.菜系ComboBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "菜系", true));
            this.菜系ComboBox.FormattingEnabled = true;
            this.菜系ComboBox.Items.AddRange(new object[] {
            "粤菜",
            "川菜",
            "湘菜",
            "东北菜",
            "饮料",
            "酒类",
            "茶水"});
            this.菜系ComboBox.Location = new System.Drawing.Point(404, 52);
            this.菜系ComboBox.Name = "菜系ComboBox";
            this.菜系ComboBox.Size = new System.Drawing.Size(100, 20);
            this.菜系ComboBox.TabIndex = 5;
            // 
            // 菜名TextBox
            // 
            this.菜名TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "菜名", true));
            this.菜名TextBox.Location = new System.Drawing.Point(225, 52);
            this.菜名TextBox.Name = "菜名TextBox";
            this.菜名TextBox.Size = new System.Drawing.Size(100, 21);
            this.菜名TextBox.TabIndex = 3;
            // 
            // 编号TextBox
            // 
            this.编号TextBox.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.menuBindingSource, "编号", true));
            this.编号TextBox.Location = new System.Drawing.Point(57, 49);
            this.编号TextBox.Name = "编号TextBox";
            this.编号TextBox.Size = new System.Drawing.Size(100, 21);
            this.编号TextBox.TabIndex = 1;
            // 
            // menuTableAdapter
            // 
            this.menuTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.AdminTableAdapter = null;
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.DetailMenuTableAdapter = null;
            this.tableAdapterManager.LiuyanTableAdapter = null;
            this.tableAdapterManager.MenuTableAdapter = this.menuTableAdapter;
            this.tableAdapterManager.Order1TableAdapter = null;
            this.tableAdapterManager.salesTableAdapter = null;
            this.tableAdapterManager.UpdateOrder = OrderSystem.OrderSystemDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            this.tableAdapterManager.UsersTableAdapter = null;
            this.tableAdapterManager.VipTableAdapter = null;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3});
            this.statusStrip1.Location = new System.Drawing.Point(0, 604);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(830, 22);
            this.statusStrip1.TabIndex = 4;
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
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(367, 17);
            this.toolStripStatusLabel2.Spring = true;
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(367, 17);
            this.toolStripStatusLabel3.Spring = true;
            // 
            // MenuManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(830, 626);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuDataGridView);
            this.Controls.Add(this.menuBindingNavigator);
            this.Name = "MenuManage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "菜单管理";
            this.Load += new System.EventHandler(this.MenuManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.menuBindingNavigator)).EndInit();
            this.menuBindingNavigator.ResumeLayout(false);
            this.menuBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.menuBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.orderSystemDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.menuDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.图片PictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OrderSystemDataSet orderSystemDataSet;
        private System.Windows.Forms.BindingSource menuBindingSource;
        private OrderSystem.OrderSystemDataSetTableAdapters.MenuTableAdapter menuTableAdapter;
        private OrderSystem.OrderSystemDataSetTableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.BindingNavigator menuBindingNavigator;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton menuBindingNavigatorSaveItem;
        private System.Windows.Forms.DataGridView menuDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox 图片PictureBox;
        private System.Windows.Forms.TextBox 价格TextBox;
        private System.Windows.Forms.ComboBox 荤素ComboBox;
        private System.Windows.Forms.TextBox 原材料TextBox;
        private System.Windows.Forms.ComboBox 口味ComboBox;
        private System.Windows.Forms.ComboBox 菜系ComboBox;
        private System.Windows.Forms.TextBox 菜名TextBox;
        private System.Windows.Forms.TextBox 编号TextBox;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
    }
}