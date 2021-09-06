using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQAndXML
{
    /*
    泛型的好处：增加类型安全，带来编码的方便
    常见的泛型：泛型类和泛型方法
    
    泛型类的规范：public class 类名<T>{类的成员...}
    T：仅仅是一个占位符，只要符合C#的命名规则即可使用，但一般都是用T
    T：表示一个通用的数据类型，在使用的时候用实际类型代替
    T：泛型类可以在定义中包含多个任意数据类型的参数，参数之间用多个逗号分隔开
        如class MyGeneriClass<T1,T2,T3>{...}
        各种类型参数可以用作成员变量的类型，属性或方法等成员的返回类型或方法的参数类型等
    */
    public class MyStack<T>
    {
        private T[] stack;
        private int stackPoint;
        private int size;

        public MyStack(int size)
        {
            this.size = size;
            stack = new T[size];
            this.stackPoint = -1;
        }

        public void Push(T item)
        {
            if (stackPoint >= size)
            {
                Console.WriteLine("栈空间已满！");
            }
            else
            {
                stackPoint++;
                this.stack[stackPoint] = item;
            }
        }

        public T Pop()
        {
            T data = stack[stackPoint];
            stackPoint--;
            return data;
        }

    }

    #region default关键字的使用


    #endregion
}
