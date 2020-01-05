using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;// 引用依赖注入的名称空间

namespace IspExample
{
    class Program
    {
        static void Main()
        {
            // 新建一个容器
            var sc = new ServiceCollection();
            // 注册容器时需要把对应的接口与实现了此接口的类型分别传给容器
            // 在容器中添加（注册）ITank，并设置其对应的实现类为MediumTank
            sc.AddScoped(typeof(ITank),typeof(MediumTank));
            // 在容器中添加（注册）IVehicle，并设置其对应的实现类为LightTank
            sc.AddScoped(typeof(IVehicle),typeof(LightTank));
            // adds a scoped service of the type specified in TService to the specified IServiceCollection
            sc.AddScoped<Driver>();
            var sp = sc.BuildServiceProvider();
            // ============华丽的分割线========
            // 分割线上面是整个程序的一次性注册，下面是具体的使用
            ITank tank = sp.GetService<ITank>();
            tank.Fire();
            tank.Run();
            // Driver类实例化时需要一个IVehicle类型的实例 因为我们在容器中注册IVehicle接口时与LightTank绑定在一起
            // 故这里的实例化出来的driver调用的Driver方法中的Run方法是LightTank中实现的的Run方法
            var driver = sp.GetService<Driver>();
            driver.Drive();

        }

        static void Main2()
        {
            // ITank,HeavyTank,LightTank,MediumTank等这些都属于静态类型
            ITank tank = new HeavyTank();
            // ===============华丽的分割线===============
            // 分割线以下表示不再使用静态类型 而是使用静态类型在程序运行时在内存中的实例类型信息
            var t = tank.GetType();
            object o = Activator.CreateInstance(t);
            MethodInfo fireMi = t.GetMethod("Fire");
            MethodInfo runMi = t.GetMethod("Run");
            fireMi.Invoke(o,null);
            runMi.Invoke(o,null);
        }

        static void Main1(string[] args)
        {
            var driver = new Driver(new Car());
            driver.Drive();
            // 如果我们现在想让driver开tank就不行 因为tank类中没有实现ivehicle接口
            // 想要让diver可以开tank：方法一：将Driver中的字段与实例构造器改为ITank类型
            // 但是这样就不能开正常的小汽车 并且也违背了开闭原则
            // 之所以会出现driver不能同时开汽车与坦克的困扰 就是因为坦克实现了一个“胖接口”ITank
            // 其中Fire与Run方法应该分别放在两个不同的接口中
            var tankDriver = new Driver(new LightTank());
            tankDriver.Drive();
            
        }
    }

    class Driver
    {
        private IVehicle _vehicle;
        public Driver(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        public void Drive()
        {
            _vehicle.Run();
        }
    }

    interface IVehicle
    {
        void Run();
    }

    class Car:IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Car is running ...");
        }
    }

    class Truck:IVehicle
    {
        public void Run()
        {
            Console.WriteLine("Truck is running ...");
        }
    }

    interface ITank
    {
        void Fire();
        void Run();
    }
    interface IWeapon
    {
        void Fire();
    }

    // C#中是单根单继承 但是允许一个类实现多个接口
    class LightTank:IVehicle,IWeapon, ITank
    {
        public void Fire()
        {
            Console.WriteLine("Boom!");
        }

        public void Run()
        {
            Console.WriteLine("ka ka ka ...");
        }
    }

    class MediumTank: IVehicle, IWeapon, ITank
    {
        public void Fire()
        {
            Console.WriteLine("Boom!!");
        }

        public void Run()
        {
            Console.WriteLine("ka! ka! ka! ...");
        }
    }

    class HeavyTank: IVehicle, IWeapon, ITank
    {
        public void Fire()
        {
            Console.WriteLine("Boom!!!");
        }

        public void Run()
        {
            Console.WriteLine("ka!! ka!! ka!! ...");
        }
    }
}
