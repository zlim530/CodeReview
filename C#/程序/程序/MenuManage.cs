using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem
{
    public partial class MenuManage : Form
    {
        public MenuManage()
        {
            InitializeComponent();
        }

        private void menuBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            {
                this.Validate();
                this.menuBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
                MessageBox.Show("保存成功", "提示");
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message );
            }

        }

        private void MenuManage_Load(object sender, EventArgs e)
        {
            // TODO: 这行代码将数据加载到表“orderSystemDataSet.Menu”中。您可以根据需要移动或移除它。
            this.menuTableAdapter.Fill(this.orderSystemDataSet.Menu);
            toolStripStatusLabel2.Text = "当前用户：" + Common.DataBase.userName;
            toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToLocalTime();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string conditionStr = "";
            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    conditionStr = "菜系 ='" + comboBox2.Text.Trim() + "'";
                    break;
                case 2:
                    conditionStr = "荤素 ='" + comboBox2.Text.Trim() + "'";
                    break;
                case 3:
                    conditionStr = "菜系 ='" + comboBox2.Text.Trim() + "'";
                    break;
            }
            menuBindingSource.Filter = conditionStr;
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (menuDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("请先单击最左边的空白处，选择要删除的行，" + "按住CTRL或Shift键可同时选择多行");
            }
            else
            {
                if (MessageBox.Show("确定要删除选定的行吗？", "小心", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    for (int i = 0; i < menuDataGridView.SelectedRows.Count; i++)
                    {
                        menuBindingSource.RemoveAt(menuDataGridView.SelectedRows[i].Index);
                    }
                    this.Validate();
                    this.menuBindingSource.EndEdit();
                    this.tableAdapterManager.UpdateAll(this.orderSystemDataSet);
                }

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Bitmap image = new Bitmap(openFileDialog1.FileName);
                图片PictureBox.Image = image;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            图片PictureBox.Image = null;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox1.SelectedIndex)
            {
                case 0:
                    comboBox2.Items.Clear();
                    break;
                case 1:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("湘菜");
                    comboBox2.Items.Add("粤菜");
                    comboBox2.Items.Add("川菜");
                    comboBox2.Items.Add("东北菜");
                    comboBox2.SelectedIndex = 0;
                    break;
                case 2:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("荤菜");
                    comboBox2.Items.Add("素菜");
                    comboBox2.SelectedIndex = 0;
                    break;
                case 3:
                    comboBox2.Items.Clear();
                    comboBox2.Items.Add("酒类");
                    comboBox2.Items.Add("饮料");
                    comboBox2.Items.Add("茶水");
                    comboBox2.SelectedIndex = 0;
                    break;
            }
        }

    
    }
}