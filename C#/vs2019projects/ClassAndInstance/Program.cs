using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClassAndInstance
{
    class Program
    {
        static void Main(string[] args)
        {
            //. 表示成员操作符 由Console.WriteLine可知WriteLine是Console这个类的成员 即它是隶属于Console这个类的
            //故WriteLine是一个静态方法
            Console.WriteLine("hello world!");
            new Form();//创建了一个实例：使用new关键字 与 ()小括号方法（实际上是生成了构造器）
            Form myform;//创建了一个引用变量 但没有用它引用任何实例
            Form myform1 = new Form();
            //创建了一个新的引用变量 并引用了new Form的实例
            Form myform2;
            myform2 = myform1;
            //又创建了一个引用变量 并让此引用变量引用了与myform1一样的实例
            //即多个引用变量引用同一个实例 使用 = 赋值符号
            myform1.Text = "this is my form1.";
            //由myform1.Text可知 Text是实例myform1的成员 即Text是隶属于myform1实例的 
            //故Text是一个实例方法
            myform2.ShowDialog();
            //因为两个引用变量均引用了同一个实例 故使用引用变量对实例进行操作时  它们两操作的是同一个实例
            

        }
    }
}
