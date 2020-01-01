using System;
using MyLib;

namespace Polymorphism
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    Car car = new Car();
        //    car.Refuel();
        //    car.TurboAccelerate();
        //    Console.WriteLine(car.Speed);

        //    Bus bus = new Bus();
        //    bus.Refuel();
        //    bus.SlowAccelerate();
        //    Console.WriteLine(bus.Speed);

        //}

        static void Main()
        {
            Vehicle v = new RaceCar();
            v.Run();//Race car is running!

            Car c = new RaceCar();
            c.Run();//Race car is running!


        }
    }

    //class Bus:Vehicle
    //{
    //    public void SlowAccelerate()
    //    {
    //        Burn(1);
    //        _rpm += 500;
    //    }
    //}
    class Vehicle
    {
        public virtual void Run()
        {
            Console.WriteLine("I'm running!");
        }
    }

    class Car:Vehicle
    {
        public override void Run()
        {
            Console.WriteLine("Car is running!");
        }
    }

    class RaceCar:Car
    {
        public override void Run()
        {
            Console.WriteLine("Race car is running!");
        }
    }

}
