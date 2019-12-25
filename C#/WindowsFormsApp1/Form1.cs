using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.button3.Click += new EventHandler(this.ButtonClick);
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            //this.textBox1.Text = "Hello,world!";
            if ( sender == this.button1)
            {
                this.textBox1.Text = "Hello!";
            }
            if ( sender == this.button2)
            {
                this.textBox1.Text = "World!";
            }
            if ( sender == this.button3)
            {
                this.textBox1.Text = "Mr.Okay!";
            }
        }
    }
}
