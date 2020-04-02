using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OrderSystem
{
    public partial class adminlogin : Form
    {
        public adminlogin()
        {

            InitializeComponent();
        }

        static void Main(string[] args)
        {
            var stu = new Student();
            stu.Report();
            var csStu = new CsStudent();
            csStu.Report();
        }


        class Student
        {
            public void Report()
            {
                Console.WriteLine("I'm a student.");
            }
        }

        class CsStudent:Student
        {
            // new 修饰符
            // 显示隐藏继承于基类的继承成员
            new public void Report()
            {
                Console.WriteLine("I'm CS student.");
            }
        }


    }
}
