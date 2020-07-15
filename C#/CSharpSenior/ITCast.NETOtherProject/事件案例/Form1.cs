using System;
using System.Windows.Forms;

namespace 事件案例 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            ucLogin1.UserLoginValidating += UcLogin1_UserLoginValidating;
        }

        private void UcLogin1_UserLoginValidating(object sender, UserLoginEventArgs e) {
            if (e.LoginId == "admin" && e.LoginPassword == "123456") {
                e.IsOk = true;
            }
        }
    }
}
