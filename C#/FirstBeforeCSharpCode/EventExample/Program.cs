using System;
using System.Threading;

namespace EventExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Watier watier = new Watier();
            customer.Order += watier.Action;
            customer.Action();// customer内部触发事件
            customer.PayTheBill();
        }
    }

    public class OrderEventArgs:EventArgs
    {
        public string DishName { get; set; }

        public string Size { get; set; }
    }

    // 声明一个委托类型：该委托专门用于事件处理故以EventHandler结尾
    // 注意这个委托的声明是与类平级的
    public delegate void OrderEventHandler(Customer customer,OrderEventArgs e);

    public class Customer
    {
        // 委托类型字段的声明
        private OrderEventHandler orderEventHandler;

        // 自定义事件的完整声明格式
        public event OrderEventHandler Order
        {
            add
            {
                // 这里的value是一个上下文关键字：表示我们触发事件时传进来的那个OrderEventHandler
                this.orderEventHandler += value;
            }
            remove
            {
                this.orderEventHandler -= value;
            }
        }

        public double Bill { get; set; }

        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.",Bill);
        }

        public void WalkIn()
        {
            Console.WriteLine("Walk into the restaurant.");
        }

        public void SitDown()
        {
            Console.WriteLine("Sit down.");
        }

        public void Think()
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Let me think ...");
                Thread.Sleep(1000);
            }

            if (this.orderEventHandler != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                this.orderEventHandler.Invoke(this, e);
            }
        }

        public void Action()
        {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }

    }

    public class Watier
    {
        public void Action(Customer customer, OrderEventArgs e)
        {
            Console.WriteLine("I will server you the dish - {0}.",e.DishName);
            double price = 10;
            switch (e.Size)
            {
                case "small":
                    price = price * 0.5;
                    break;
                case "large":
                    price = price * 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }

}
