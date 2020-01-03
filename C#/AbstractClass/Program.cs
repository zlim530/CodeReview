using System;

namespace AbstractClass
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle v = new Truck();
            v.Run();
        }
    }

    abstract class Vehicle
    {
        // 将相同的方法提取出来放在父类中  但这样Vehicle类型的变量将没有Run方法
        public void Stop()
        {
            Console.WriteLine("Stoppped!");
        }

        public void Fill()
        {
            Console.WriteLine("Pay and fill...");
        }

        // 解决方法一：在父类中添加Run方法 但是这样后面没添加一个子类都要在父类中修改代码：这违反了开闭原则
        //public void Run(string type)
        //{
        //    if ( type == "Car")
        //    {
        //        Console.WriteLine("Car is running!");
        //    }
        //    else if ( type == "Truck")
        //    {
        //        Console.WriteLine("Truck is running!");
        //    }
        //    else if ( type == "...")
        //    {
        //        Console.WriteLine("...");
        //    }
        //}

        // 解决方法2：添加虚方法 并在子类中进行重写
        //  虚方法解决了 Vehicle 类型变量调用子类 Run 方法的问题，
        //  也遗留下来一个问题：Vehicle 的 Run 方法的行为本身就很模糊，且在实际应用中也根本不会被调到。
        //  而且从测试的角度来看，测试一段你永远用不到的代码，也是不合理的。
        //public virtual void Run()
        //{
        //    Console.WriteLine("Vehicle is running!");
        //}

        // 最后干脆将Run方法变成一个抽象方法 则此时Vehicle就变成了一个抽象类
        public abstract void Run();

    }

    class Car:Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Car is running!");
        }

    }

    class Truck:Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Truck is running!");
        }

    }

    class RaceCar:Vehicle
    {

        public override void Run()
        {
            Console.WriteLine("Race car is running!");
        }
    }
}
