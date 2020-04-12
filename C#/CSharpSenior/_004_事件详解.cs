using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpSenior {
    class _004_事件详解 {

        static void Main(string[] args) {
            var customer = new Customer();
            var waiter = new Waiter();
            customer.Order += waiter.OrderAction;

            customer.Action();
            customer.PayTheBill();
        }

    }

    public delegate void OrderEventHanlder(Customer customer,OrderEventArgs e);

    public class OrderEventArgs:EventArgs {
        public string DishName { get;set; }
        public string Size { get; set; }
    }

    public class Customer {
        private OrderEventHanlder orderEventHanlder;

        // 声明事件成员
        // 事件(成员)对外界隐藏了委托实例的大部分功能
        // 仅暴露了添加(add)/移除(remove)事件处理器的功能
        public event OrderEventHanlder Order {
            add { this.orderEventHanlder += value; }
            remove { this.orderEventHanlder -= value; }
        }

        public double Bill{ get; set; }

        public void PayTheBill() {
            Console.WriteLine("I will pay ${0}.",this.Bill);
        }

        public void WalkIn() {
            Console.WriteLine("Walk into the restaurant");
        }

        public void SitDown() {
            Console.WriteLine("Sit down.");
        }

        public void Think() {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine("Let me think...");
                Thread.Sleep(500);
            }

            if ( this.orderEventHanlder != null) {
                var e = new OrderEventArgs();
                e.DishName = "Kongpao Chicken";
                e.Size = "large";

                this.orderEventHanlder.Invoke(this,e);
            }
        }

        public void Action() {
            Console.ReadLine();
            this.WalkIn();
            this.SitDown();
            this.Think();
        }

    }

    public class Waiter {
        public void OrderAction(Customer customer,OrderEventArgs e) {
            Console.WriteLine("I will serve you the dish - {0}.",e.DishName);
            double price = 10;
            switch (e.Size) {
                case "small":
                    price *= 0.5;
                    break;
                case "large":
                    price *= 1.5;
                    break;
                default:
                    break;
            }
            customer.Bill += price;
        }
    }

}
