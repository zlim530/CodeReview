using System;
using System.Threading;

namespace EventExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();

            Customer customer = new Customer();// 事件拥有者
            Watier watier = new Watier(); // 事件响应者
            customer.Order += watier.Action; // 事件本身以及事件的订阅关系以及事件处理器
            //customer.Action();// customer内部触发事件

            OrderEventArgs e = new OrderEventArgs();
            e.DishName = "Manhanquanxi";
            e.Size = "large";

            OrderEventArgs e2 = new OrderEventArgs();
            e2.DishName = "Beer";
            e2.Size = "large";

            // badGuy借刀杀人 只要将Order声明为事件（即添加event关键字）即可避免
            //Customer badGuy = new Customer();
            //badGuy.Order += watier.Action;
            //badGuy.Order.Invoke(customer,e);
            //badGuy.Order.Invoke(customer,e2);

            customer.PayTheBill();
        }
    }

    public class OrderEventArgs : EventArgs
    {
        public string DishName { get; set; }

        public string Size { get; set; }
    }

    // 声明一个委托类型：该委托专门用于事件处理故以EventHandler结尾
    // 注意这个委托的声明是与类平级的
    public delegate void OrderEventHandler(Customer customer, OrderEventArgs e);

    public class Customer
    {
        // 委托类型字段的声明
        //private OrderEventHandler orderEventHandler;

        // 事件的简略声明 OrderEventHandler表示使用什么样的委托来约束事件 Order表示事件名
        //public event OrderEventHandler Order;
        public event OrderEventHandler Order;

        public double Bill { get; set; }

        public void PayTheBill()
        {
            Console.WriteLine("I will pay ${0}.", Bill);
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

            if (this.Order != null)
            {
                OrderEventArgs e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";
                this.Order.Invoke(this, e);
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
            Console.WriteLine("I will server you the dish - {0}.", e.DishName);
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
