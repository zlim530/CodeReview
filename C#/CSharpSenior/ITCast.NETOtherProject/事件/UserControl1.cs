using System;
using System.Windows.Forms;

namespace 事件 {
    public partial class UserControl1 : UserControl {
        public UserControl1() {
            InitializeComponent();
        }

        int count = 0;
        
        public Action TripleClick;

        private void button1_Click(object sender, EventArgs e) {
            count++;
            if (count >= 3) {
                //MessageBox.Show("Hello,World!");
                if (TripleClick != null) {
                    TripleClick();
                }
                count = 0;
            }
        }
    }
}
