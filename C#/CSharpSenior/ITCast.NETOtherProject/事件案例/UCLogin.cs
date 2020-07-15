using System;
using System.Drawing;
using System.Windows.Forms;

namespace 事件案例 {
    public partial class UCLogin : UserControl {
        public UCLogin() {
            InitializeComponent();
        }
        //定了一个用户校验事件
        public event Action<object,UserLoginEventArgs> UserLoginValidating;

        private void button1_Click(object sender, EventArgs e) {
            //在这里执行用户的检验
            if (UserLoginValidating != null) {
                UserLoginEventArgs eventArgs = new UserLoginEventArgs();
                eventArgs.IsOk = false;
                eventArgs.LoginId = txtUid.Text.Trim();
                eventArgs.LoginPassword = txtPwd.Text.Trim();
                UserLoginValidating(this,eventArgs);
                if (eventArgs.IsOk) {
                    this.BackColor = Color.FromArgb(253, 232, 234);
                } else {
                    this.BackColor = Color.FromArgb(40, 44, 52);
                }
            }
        }
    }

    public class UserLoginEventArgs:EventArgs {
        public string LoginId { get; set; }
        public string LoginPassword { get; set; }

        public bool IsOk { get; set; }
    }
}
