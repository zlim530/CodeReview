using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TypeInCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            //int = Int32 -->struct C#将常用的int整合为Int32结构体 但由于int太常用了所以C#吸收使其成为了C#中的关键字
            //long = Long64
            Form f = new Form();
            f.WindowState = FormWindowState.Normal;
            f.ShowDialog();
            //class
            //struct
            //enum

            short s;
            s = -1000;
            string str = Convert.ToString(s, 2);
            // 将s转化为string 并以2进制的形式转化
            Console.WriteLine(str);
        }
    }
}
