using System;

namespace InterfaceExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle v = new Truck();
            v.Fill();// Pay and fill!
            v.Run();// Truck is running!
        }
    }

    interface IVehicle
    {
        void Stop();
        void Fill();

        void Run();
    }

    abstract class Vehicle : IVehicle
    {
        public void Stop()
        {
            Console.WriteLine("Stoppped!");
        }

        public void Fill()
        {
            Console.WriteLine("Pay and fill!");
        }

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
}
