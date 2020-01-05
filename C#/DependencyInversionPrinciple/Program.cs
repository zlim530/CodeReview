using System;

namespace DependencyInversionPrinciple
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneUser user = new PhoneUser(new NokiaPhone());
            user.UsePhone();
        }
    }

    class PhoneUser
    {
        private IPhone _phone;

        public PhoneUser(IPhone phone)
        {
            _phone = phone;
        }

        public void UsePhone()
        {
            _phone.Dail();
            _phone.PickUp();
            _phone.Send();
            _phone.Receive();
        }
    }

    interface IPhone
    {
        void Dail();
        void PickUp();
        void Send();
        void Receive();
    }

    class NokiaPhone:IPhone
    {
        public void Dail()
        {
            Console.WriteLine("Nokia calling ... ");
        }

        public void PickUp()
        {
            Console.WriteLine("Hello! This is Tim!");
        }

        public void Send()
        {
            Console.WriteLine("Nokia message ring ..");
        }

        public void Receive()
        {
            Console.WriteLine("Hello!");
        }
    }

    class EricssonPhone:IPhone
    {
        public void Dail()
        {
            Console.WriteLine("Ericsson calling ... ");
        }

        public void PickUp()
        {
            Console.WriteLine("Hello! This is Tim!");
        }

        public void Send()
        {
            Console.WriteLine("Ericsson message ring ..");
        }

        public void Receive()
        {
            Console.WriteLine("Good evening!");
        }
    }



    //class Engine
    //{
    //    public int RPM { get; set; }

    //    public void Work(int gas)
    //    {
    //        RPM = 1000 * gas;
    //    }
    //}

    //class Car
    //{
    //    // Car中有一个Engine的私有字段 故Car与Engine形成了紧耦合关系：
    //    // 所谓紧耦合就是一旦Engine出现问题 那么Car也一定无法正常运行
    //    private Engine _engine;

    //    public int Speed 
    //    { 
    //        get;
    //        // 表示不允许外界修改
    //        private set; 
    //    }

    //    public Car(Engine engine)
    //    {
    //        _engine = engine;
    //    }

    //    public void Run(int gas)
    //    {
    //        _engine.Work(gas);
    //        Speed = _engine.RPM / 100;
    //    }

    //}
}
