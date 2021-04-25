using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Override
{
    class Program
    {
        static void Main(string[] args)
        {
            Vehicle v = new Car();
            v.Run();//Car is running!
            Console.WriteLine(v.Speed);//50
        }
    }

    class Vehicle
    {
        private int _speed;

        public virtual int Speed 
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = value;
            }
        }
        public virtual void Run()
        {
            Console.WriteLine("I'm running!");
            _speed = 100;
        }
    }

    class Car:Vehicle
    {
        private int _rpm;

        public override int Speed 
        {
            get
            {
                return _rpm / 100;
            }
            set
            {
                _rpm = value * 100;
            }
        }
        public override void Run()
        {
            Console.WriteLine("Car is running!");
            _rpm = 5000;
        }
    }
}
