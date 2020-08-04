using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoongEgg.MvvmCore.Demo.NetFramework {
    public class Program {
        // Title: [蛋兄]-58 观察者模式，在.Net Core可用的MVVM框架及演示, [.NetStandard]、[委托]、[Event]
        // 什么是.NetStandard 2.0? 一个同时兼容.Net framework和.Net Core的跨框架框架
        // TODO: 1.LoongEgg.MvvmCore下载, from NuGet
        // TODO: 2.控制台输入输出
        // TODO: 3.被观察者，饲养员的创建，来自框架提供的委托，CodeSnippet
        // TODO: 4.观察者，动物类的创建
        // TODO: 5.开始暗中观察
        // TODO: 6.Virtual虚方法override，只吃鱼的猫
        // TODO: 7.PowerShell启动C#程序并传入Main的args
        public static void Main(string[] args) {
            string name = string.Empty;
            if (args == null || !args.Any()) {
                while (string.IsNullOrEmpty(name)) {
                    Console.WriteLine($"Hello what's your name?");
                    name = Console.ReadLine();
                }
            } else {
                name = args[0];
            }
            Console.WriteLine($"Welcome <{name}> to loongegg's magic kindom!");

            // 创建对象
            var keeper = new Keeper(name);
            var monkey = new Monkey();
            var dog = new Dog();
            var cat = new Cat();

            // 让动物们“暗中观察”饲养员
            keeper.PropertyChanged += monkey.Observer;
            keeper.PropertyChanged += dog.Observer;
            keeper.PropertyChanged += cat.Observer;

            while (true) {
                Console.WriteLine("What food do you want to feed the animal?");
                keeper.Food = Console.ReadLine();
            }
        }


    }

    /// <summary>
    /// 饲养员
    /// </summary>
    public class Keeper:ObservableObject {
        public string Name { get; set; }

        /// <summary>
        /// 要分发的食物
        /// </summary>
        public string Food {
            get => _Food;
            set { _Food = value; }
        }
        private string _Food = "Peach";

        public Keeper(string name) {
            if (name == null) {
                this.Name = "No name";
            }
        }
    }

    /// <summary>
    /// 动物抽象类
    /// </summary>
    public abstract class Animal {
        /// <summary>
        /// “暗中观察”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Observer(object sender, PropertyChangedEventArgs e) {
            if (sender is Keeper keeper) {
                Eat(keeper.Food);
            }
        }

        /// <summary>
        /// 吃食物的虚方法
        /// </summary>
        /// <param name="food"></param>
        public virtual void Eat(string food) {
            Console.WriteLine($"   {this.GetType().Name} eat {food}");
        }
    }


    public class Monkey:Animal {
    }
    public class Dog:Animal {
    }
    public class Cat:Animal {
        public override void Eat(string food) {
            if (food.ToLower() == "fish") {
                base.Eat(food);
            } else {
                var oldColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("   Cat:Stuip human");
                Console.ForegroundColor = oldColor;
            }
        }
    }


}
