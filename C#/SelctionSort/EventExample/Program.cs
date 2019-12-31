using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace EventExample
{
    class Program
    {
        static void Main()
        {
            var form = new MyForm();
            form.Click += form.FormClicked;
            form.ShowDialog();
        }

        class MyForm : Form
        {
            internal void FormClicked(object sender, EventArgs e)
            {
                this.Text = DateTime.Now.ToString();
            }
        }

        static void Main2()
        {
            Form form = new Form();
            var controller = new Controller(form);
            form.ShowDialog();
        }

        class Controller
        {
            private Form form;
            public Controller(Form form)
            {
                if ( form != null)
                {
                    this.form = form;
                    this.form.Click += this.FormClicked;
                }
            }

            private void FormClicked(object sender, EventArgs e)
            {
                this.form.Text = DateTime.Now.ToString();
            }
        }

        static void Main1(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1000;

            Boy boy = new Boy();
            Girl girl = new Girl();
            
            timer.Elapsed += boy.Action;
            timer.Elapsed += girl.Action;

            timer.Start();
            Console.ReadLine();

        }

    }

    class Boy
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Jump!");
        }
    }

    class Girl
    {
        internal void Action(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Sing!");
        }
    }
}
