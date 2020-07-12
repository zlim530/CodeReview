using System;
using System.Windows.Forms;

namespace _01委托案例_窗体之间传值 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            Form2 f2 = new Form2(textBox1.Text.Trim(), UpdateTextBox);
            f2.Show();
        }

        private void UpdateTextBox(string val) {
            this.textBox1.Text = val;
        }

        public delegate void UpdateTextDelegate(string val);


    }
}
