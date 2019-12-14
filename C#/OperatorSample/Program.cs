using System;
using System.Windows.Forms;

namespace OperatorSample
{
    class Program
    {
        static void Main3()
        {
            uint x = uint.MaxValue;
            Console.WriteLine(x);
            uint z = x + 1;
            Console.WriteLine(z);   // z = 0 发生了溢出 
            string binZstr = Convert.ToString(z, 2);
            Console.WriteLine(binZstr);
            string binStr = Convert.ToString(x, 2);
            Console.WriteLine(binStr);
            try
            {
                // 使用check操作符可以检查溢出 C#默认使用unchecked就不检查机制
                uint y = checked(x + 1);
                Console.WriteLine(y);
            }
            catch (OverflowException)
            {
                Console.WriteLine("There's overflow!");
            }
            // checked还可以当做关键字使用 这样默然checked{}语句块中的所有内容都会进行溢出检查
            checked
            {
                try
                {
                    // 使用check操作符可以检查溢出 C#默认使用unchecked就不检查机制
                    uint yy = x + 1;
                    Console.WriteLine(yy);
                }
                catch (OverflowException)
                {
                    Console.WriteLine("There's yy overflow!");
                }
            }
        }

        static void Main()
        {
            unsafe
            {
                StudentTwo stutwo;
                stutwo.ID = 1;
                stutwo.Score = 99;
                   
                // & 取地址
                StudentTwo* pstu = &stutwo;
                pstu->Score = 100;
                Console.WriteLine(stutwo.Score);

                // * 取引用 即取内容
                (*pstu).Score = 101;
                Console.WriteLine(stutwo.Score);
            
            }
        
        }

        struct StudentTwo
        {
            public int ID;
            public long Score;
        }


        static void Main4(string[] args)
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
            //new还可以作为关键字使用 这里的new是一个修饰符 而不是一个操作符
            // 使用new来隐藏子类继承父类中的方法 ---> 不常用 了解即可
            new public void Report()
            {
                Console.WriteLine("I'm a CS student.");
            }
        
        }

        static void Main2(string[] args)
        {

            // var关键字 配合 new操作符使用 可以为匿名类型创建实例 此时就体现了var关键字的强大
            // 因为此时你想显式声明这个类型的实例都无法声明 因为你不知道这个类型的名称 
            var person = new { Name = "Mr.C# ",Age = 34 };
            Console.WriteLine(person.Name);
            Console.WriteLine(person.Age);
            Console.WriteLine(person.GetType().Name);
            // <>f__AnonymousType0`2    <>f__AnonymousType为固定字段表明这是一个匿名类型
            // 0表明是程序中的第一个 `2 表示这一个泛型类 由两个字类型构成(即前面的Name 和 Age)

        }
        static void Main1(string[] args)
        {
            var x = 100D;
            // var 关键字：让编译器自动推导类型
            Console.WriteLine(x.GetType().Name);
            Console.WriteLine(x.GetType().FullName);

            // 将new关键字当做操作符使用：不仅可以调用实例的构造器() 也可以调用实例的初始化器{}
            Form myForm = new Form() { Text = "Hello", FormBorderStyle = FormBorderStyle.SizableToolWindow };
            myForm.ShowDialog();

            // 使用new操作符创建实例也可以不用在前面一定加上一个引用类型引用此实例 如果此实例只需要用一次 就没有必要再新建一个引用变量来引用这个实例
            new Form() { Text = "Hello" }.ShowDialog();
        }
    }
}
