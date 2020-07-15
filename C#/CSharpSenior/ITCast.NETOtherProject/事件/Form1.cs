using System;
using System.Windows.Forms;

namespace 事件 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) {
            MessageBox.Show("Hello,World!");
        }

        private void Form1_Load(object sender, EventArgs e) {
            userControl11.TripleClick = () => {
                MessageBox.Show("在窗体1中被点击了三次！");
            };

            //用事件实现的3连击
            //事件不能用 = 赋值，只能使用 += 或 -= 来赋值
            userControl21.TripleClick += () => {
                MessageBox.Show("使用事件来实现三次点击触发事件！");
            };
        }
    }
}
