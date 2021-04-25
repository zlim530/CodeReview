using System;


namespace Inherit
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    var car = new Car();
        //    // is a 概念：从语义上来说，一个派生类的实例也是一个基类的实例
        //    Console.WriteLine(car is Vehicle);//True
        //    Console.WriteLine(car is object);//True

        //    var vehicle = new Vehicle();
        //    Console.WriteLine(vehicle is Car);//False

        //    Vehicle ve = new Vehicle();
        //    // 可以用基类类型的变量去引用派生类实例：由于C#为强类型语言 即变量与对象都有对应的类型 故变量与对象之间会存在“类型差”
        //    object o1 = new Vehicle();
        //    object o2 = new Car();


        //    var t = typeof(Car);
        //    var tb = t.BaseType;
        //    var top = tb.BaseType;
        //    Console.WriteLine(tb.FullName);//Inherit.Vehicle
        //    Console.WriteLine(top.FullName);//System.Object 在C#中所有类型都显式或隐式的继承至Object

        //    Console.WriteLine(top.BaseType == null);//True 
        //}

        static void Main()
        {
            var v = new Vehicle("N/A");
            var car = new Car("VOV");
            car.ShowOwner();
        }
    }

    class Vehicle
    {
        public Vehicle(string owner)
        {
            Owner = owner;
        }

        public string Owner { get; set; }
    }

    class Car : Vehicle
    {
        public Car(string owner) : base(owner)
        {

        }

        public void ShowOwner()
        {
            Console.WriteLine(Owner);
        }
    }
}
