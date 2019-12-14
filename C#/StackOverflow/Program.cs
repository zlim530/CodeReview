using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Stack Overflow：函数调用过多（算法错误）--->在栈上分配了过多的内存 
// 由于栈空间很小 但函数调用分配的内存超过栈的内存大小时就会发生Stack Overflow
// 运行程序(Ctrl+F5)---> Process is terminated due to StackOverflowException.
namespace StackOverflow
{
    class Program
    {
        static void Main(string[] args)
        {
            //BadGuy bg = new BadGuy();
            //bg.BadMethod();

            unsafe
            {
                int* p = stackalloc int[9999999];
            }
        }

        class BadGuy
        {
            public void BadMethod()
            {
                int x = 100;
                this.BadMethod();
            }
        }
    }
}
