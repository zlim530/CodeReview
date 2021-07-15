using System;

namespace DesignMode
{
    #region 单例模式的实现
    /// <summary>
    /// 单列模式的实现
    /// </summary>
    public sealed class Singleton
    {
        // 定义一个静态变量来保存类的实例
        private static volatile Singleton uniqueInstance;

        // 定义一个标识确保线程同步
        private static readonly object locker = new object();

        // 定义私有构造函数，使外界不能创建该类实例
        private Singleton()
        {
        }

        /// <summary>
        /// 定义公有方法提供一个全局访问点，同时你也可以定义公有属性来提供全局访问点
        /// </summary>
        /// <returns></returns>
        public static Singleton GetInstance()
        {
            // 当第一个线程运行到这里时，此时会对locker对象“加锁”
            // 当第二个线程运行该方法时，首先检测locker对象为“加锁”状态，该线程就会挂起等到第一个线程解锁
            // lock语句运行完成之后（即第一个线程运行完之后）会对该对象“解锁”
            // 双重锁定只需要一句判断就可以了
            if (uniqueInstance == null)
            {
                lock (locker)
                {
                    // 如果类的实例不存在则创建，否则直接返回
                    if (uniqueInstance == null)
                    {
                        uniqueInstance = new Singleton();
                    }
                }
            }
            return uniqueInstance;
        }

    }
    #endregion


    #region 简单工厂模式
    /// <summary>
    /// 简单工厂模式的实现
    /// </summary>
    public static class FoodSimpleFactory
    {
        public static Food CreateFood(string type)
        {
            Food food = null;
            if ("shredded pork with potatoes".Equals(type))
            {
                food = new ShreddedPorkWithPotatoes();
            }
            else if ("tomato Scrambled eggs".Equals(type))
            {
                food = new TomatoScrambledEggs();
            }
            return food;
        }
    }

    public static class SimpleFactoryClass
    {
        static void Main0(string[] args)
        {
            var tomatoFood = FoodSimpleFactory.CreateFood("tomato Scrambled eggs");
            tomatoFood.Print();

            var potatoesFood = FoodSimpleFactory.CreateFood("shredded pork with potatoes");
            potatoesFood.Print();
        }

    }

    public abstract class Food
    {
        public abstract void Print();
    }

    public class TomatoScrambledEggs : Food
    {
        public override void Print()
        {
            Console.WriteLine("One tomato Scrambled eggs.");
        }
    }

    public class ShreddedPorkWithPotatoes : Food
    {
        public override void Print()
        {
            Console.WriteLine("One shredded pork with potatoes.");
        }
    }
    #endregion

    
}