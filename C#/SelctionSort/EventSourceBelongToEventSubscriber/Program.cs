using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EventSourceBelongToEventSubscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            // 事件响应者
            var form = new MyForm();
            form.ShowDialog();
        }
    }

    class MyForm:Form
    {
        private TextBox textBox;
        // 事件拥有者：button是Form类的一个字段成员，又MyForm继承至Form，故button也是MyForm的一个字段成员：
        // 即事件拥有者是事件响应者的一个字段成员:这是微软默认的事件模型
        private Button button;

        public MyForm()
        {
            this.textBox = new TextBox();
            this.button = new Button();

            this.Controls.Add(this.button);
            this.Controls.Add(this.textBox);

            this.button.Click += this.ButtonClicked;
        }

        private void ButtonClicked(object sender, EventArgs e)
        {
            this.textBox.Text = "Hello,World!!!!!!!!!!!!!!!!!!!!!!!1";
        }
    }
}
