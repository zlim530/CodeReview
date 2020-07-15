using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 事件 {
    public partial class UserControl2 : UserControl {
        public UserControl2() {
            InitializeComponent();
        }
        int count = 0;

        //声明一个事件
        //声明事件与声明委托变量特别像,就是在声明委托变量的前面加上一个 event 关键字
        //当加上 event 关键字后,委托变量就变成了一个事件
        public event Action TripleClick;
        private void button1_Click(object sender, EventArgs e) {
            count++;
            if (count >= 3) {
                //事件在"触发"或"调用"的时候与委托变量的使用方式一模一样
                if (TripleClick != null) {
                    TripleClick();
                }
                count = 0;
            }
        }
    }
}
