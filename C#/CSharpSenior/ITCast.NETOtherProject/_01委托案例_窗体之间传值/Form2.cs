using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using static _01委托案例_窗体之间传值.Form1;

namespace _01委托案例_窗体之间传值 {
    public partial class Form2 : Form {
        public Form2() {
            InitializeComponent();
        }

        public Form2(string n,UpdateTextDelegate updateText) : this() {
            this.textBox1.Text = n;
            this._update = updateText;
        }

        private UpdateTextDelegate _update;

        private void button1_Click(object sender, EventArgs e) {
            // 1.将当前窗体中的文本框中的值，赋值给“窗体1”中的文本框
            this._update(textBox1.Text.Trim());
            // 2.关闭窗体2
            this.Close();
        }
    }
}
