using System;

namespace IspExample3
{
    class Program
    {
        static void Main(string[] args)
        {
            var warmkiller = new WarmKiller();
            // 我们不希望killer的Kill方法被随随便便的看到
            warmkiller.Love();
            
            IKiller killer = warmkiller;
            // 此时只有IKiller类型的变量才可以看到Kill方法
            killer.Kill();

            IKiller killer2 = new WarmKiller();
            killer2.Kill();
            // 如果想调用Love方法 可以使用强制类型转换 或者使用 as关键字
            //var wk = (IGentleman)killer2;
            var wk = (WarmKiller)killer2;
            //var wk = killer2 as WarmKiller;
            wk.Love();

        }
    }

    interface IGentleman
    {
        void Love();
    }

    interface IKiller
    {
        void Kill();
    }

    // 一个类可以实现多个接口
    class WarmKiller : IGentleman, IKiller
    {

        public void Love()
        {
            Console.WriteLine("I will love you for ever...");
        }

        // 接口的显式实现：是C#所独有的功能 
        void IKiller.Kill()
        {
            Console.WriteLine("Let me kill the enemy...");
        }
    }
}
