using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaultOperator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 值类型未赋值时内存块都刷为0 值就是0
            int x = default(int);
            Console.WriteLine(x);
               
            // 引用类型中的类类型 声明了引用变量但没有实例化 内存块刷为0 值就是null
            Form myForm = default(Form);
            Console.WriteLine(myForm==null);
               
            // 而枚举类型映射到整型上，枚举类型的默认值是整型值为0所对应的那个值 可以是人为手动指定的 也可以是系统默认赋值的
            // 故我们在创造枚举类型时  要考虑到可能有人会使用default访问我们的枚举类型 故应该在我们的枚举类型找那个手动添加一个值为0的枚举值
            Level l = default(Level);
            Console.WriteLine(l);
            

        }
    }

    enum Level
    { 
        low = 3,
        mid = 2,
        high = 1,
        defaultValue = 0
    }
}
