using System;

/**
 * @author zlim
 * @create 2020/5/12 18:16:41
 */

namespace 显式实现接口 { 
    public class Program {
        static void Main0(string[] args) {

            Teather teather = new Teather();
            // 会调用正常实现接口的 Fly 方法，显式实现的调不到，因为默认是 private
            teather.Fly();// Implement IInteraface1's Fly.

            IInterface1 i1 = new Teather();
            i1.Fly();// Implement IInteraface1's Fly.


            IInterface2 i2 = new Teather();
            i2.Fly();// Implement IInteraface2's Fly.
            // 虽然 Teather 类中显式实现了 IInterface2 中的 Fly 方法，但是对于接口 IInterface2 本身来说，Fly 方法是 public
            // 因此通过 IInterface2 变量可以调用到显式实现的 Fly 方法 

            // 如果两个接口都使用显式实现，那么使用 MyClass 对象在外界都无法访问到这个两个方法 
            // 但是可以通过 IInterface1 和 IInterface2 变量进行访问
            IInterface1 interface1 = new MyClass();
            interface1.Fly();// Implement IInteraface1's Fly.
            IInterface2 interface2 = new MyClass();
            interface2.Fly();// Implement IInteraface2's Fly.

        }
    }

    public interface ISwim {
        void Swim();
    }

    public class Perosn : ISwim {

        public int Age {
            get { return _Age; }
            set { _Age = value; }
        }
        private int _Age;


        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name;


        public void Swim() {
            Console.WriteLine("Person is swimming ... ");
        }
    }

    /*
    此时 Women 类中就已经有了 Swim() 方法，也即相当于 Women 类也实现了 ISwim 接口
    因此我们可以显式的在 Person 类后面再加上一个 ISwim 接口，这样在利用接口实现多态进行向上转型/换时效率更高
        ISwim swim = new Women();    
    即就不需要先将 ISwim 转换成 Person，再将 Person 转换为 Women，而是可以直接将 ISwim 转换为 Women，因为 Women
    实现了接口
    */
    public class Women : Perosn,ISwim {
        // 此时如果 Women 类想再次实现接口，则只能显式实现，因为它继承了 Person 类，就相当于已经有一个 Swim() 方法了
        // 因此只能使用显式实现接口的方法进行 ISwim 接口的重新实现
        void ISwim.Swim() { 
            Console.WriteLine("Women is swimming ... ");
        }
    }

    public interface IInterface1 {
        void Fly();
    }

    public interface IInterface2 {
        void Fly();
    }

    // 这样不会报错，相当于两个接口中的同名方法使用了同一个实现
    public class Student : IInterface1,IInterface2 {
        public void Fly() {
            Console.WriteLine("Implement IInteraface1's Fly.");
        }
    }

    public class Teather : IInterface1, IInterface2 {
        public void Fly() {
            Console.WriteLine("Implement IInteraface1's Fly.");
        }

        // 显式实现接口
        // 显式实现接口没有访问修饰符，默认是私有的，并且方法名称前面加了“接口名”，例如：接口名.方法名
        void IInterface2.Fly() {
            Console.WriteLine("Implement IInteraface2's Fly.");
        }
    }

    // 如果两个接口都使用显式实现，那么使用 MyClass 对象在外界都无法访问到这个两个方法 
    public class MyClass : IInterface1, IInterface2 {
        void IInterface1.Fly() {
            Console.WriteLine("Implement IInteraface1's Fly.");
        }

        void IInterface2.Fly() {
            Console.WriteLine("Implement IInteraface2's Fly.");
        }
    }
}

/*
什么是接口？
    接口就是一种规范，协议，约定好遵守某种规范就可以写通用的代码。
    定义了一组具有各种功能的方法。（只是一种能力，没有具体实现，像抽象方法一样，“光说不做”）
接口存在的意义：多态。多态的意义：程序可扩展性。最终让同一套使用程序代码变的更灵活，从而节省成本，提高效率。
接口解决了类的多继承的问题
接口解决了类继承以后体积庞大的问题
接口之间可以实现多继承（也即不管是接口还是类还是结构体都可以实现多个接口）
从语法角度来看，与抽象类类似
在 U盘插在电脑的 USB 接口上 这个例子中，USB3.0 协议就是接口，U盘就是接口的具体实现类
而 USB3.0 接口的多态就是体现在电脑上，就不管是哪个 U盘厂商生产的 U盘，只要你实现了 
USB3.0 接口那就可以插在有 USB3.0 接口的电脑上使用这个 U盘

接口不能实例化
接口就是让子类来实现的
接口里面只能包含方法：而方法、属性、事件、索引器都是方法；不能有委托，委托是一个字段
接口中的所有的成员都不能显式的写任何的访问修饰符，默认是 public 的访问修饰符
接口中的成员不能有任何实现

1.接口可以实现“多继承”（多实现），一个类智能继承自一个父类，但是可以实现多个接口；
2.接口解决了不同类型之间进行多态的问题，比如鱼和船不是同一个类型，但是都能在水里“游泳”，
  只是实现的方式不一样，要对“游泳”实现多态，只能考虑接口。
在实现多态的方法中：优先原则：接口 优于 抽象类 优于 父类 优于 子类
    接口比抽象类更抽象
*/
namespace 接口 {
    public class Program {
        static void Main0(string[] args) {
            IFlyable0 fly = new MyClass();
            fly.SayHi();
        }

        static void Main1(string[] args) {
            IFlyable bird = new Sparrow();
            bird.Fly();
        }

        static void Main2(string[] args) {
            ICollectHomework collectHomework = new /*Student*/Teather();
            collectHomework.Collect();
        }

        static void Main3(string[] args) {
            Chinese cn = new Chinese();
            American am = new American();
            Germen gm = new Germen();

            Register(cn);
            Register(am);
            Register(gm);

            Car car = new Car();
            Register(car);
        }

        public static void Register(IShowInfo obj) {
            obj.Show();
        }
    }

    public interface IShowInfo {
        void Show();
    }

    public abstract class Person1:IShowInfo {
        public string Name { get; set; }

        public int Age { get; set; }

        public abstract void Show();
    }

    public class Chinese : Person1 {
        public override void Show() {
            Console.WriteLine("Chinese 18.");
        }
    }

    public class American : Person1 {
        public override void Show() {
            Console.WriteLine("American 20.");
        }
    }

    public class Germen : Person1 {
        public override void Show() {
            Console.WriteLine("Germen 22.");
        }
    }

    public class Car : IShowInfo {
        public void Show() {
            Console.WriteLine("BMW 25.");
        }
    }

    public interface ICollectHomework {
        void Collect();
    }

    public class Person {

    }

    public class Student : Person, ICollectHomework {
        public void Collect() {
            Console.WriteLine("Student is collecting homework.");
        }
    }

    public class Teather : Person, ICollectHomework {
        public void Collect() {
            Console.WriteLine("Teather is collecting homework.");
        }
    }

    public class SchoolMaster : Person {

    }

    public class Bird {
        public void Bark() {
            Console.WriteLine("Bird is Barking .... ");
        }
    }

    public interface IFlyable {
        void Fly();
    }

    /// <summary>
    /// 麻雀
    /// 当一个类同时继承父类，并且实现了多个接口的时候，必须将继承类写在第一个
    /// </summary>
    public class Sparrow : Bird, IFlyable {
        public void Fly() {
            Console.WriteLine("Sparrow is flying ... ");
        }
    }

    /// <summary>
    /// 鸵鸟
    /// </summary>
    public class Ostich : Bird {

    }

    /// <summary>
    /// 企鹅
    /// </summary>
    public class Penguin : Bird {

    }

    /// <summary>
    /// 鹦鹉
    /// </summary>
    public class Parrot : Bird, IFlyable {
        public void Fly() {
            Console.WriteLine("Parrot is flying ... ");
        }
    }

    // 接口不能实例化
    // 接口就是让子类来实现的
    // 接口里面只能包含方法：而方法、属性、事件、索引器都是方法
    // 接口中的所有的成员都不能显式的写任何的访问修饰符，默认是 public 的访问修饰符
    // 接口中的成员不能有任何实现
    public interface IFlyable0 {
        void SayHi();

        // 在接口中这样写表示是一个未实现的属性，而不表示自动属性（只有在类中才表示）
        string Name { get; set; }

        // 索引器
        string this[int index] { get; set; }
    }

    public class MyClass : IFlyable0 {
        public string this[int index] { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public string Name { 
            get => throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        public void SayHi() {
            Console.WriteLine("Hi!");
        }
    }
}

/*
定义好一个类之后，不写构造函数会有一个默认的无参构造函数
当为类手动编写一个构造函数之后，会覆盖默认的无参构造函数，不管手动编写的构造函数是有参的还是无参的
只要手动添加了构造函数就会把默认的构造函数覆盖
*/


/*
引用传递：ref 传递的是栈内存中变量本身的地址：也即栈内存中的地址
    此时形参与实参其实就是同一个的内存地址（变量）的两个不同的变量名/别名（变量名本身其实就是内存地址的别名）
    注意：ref 关键字在方法声明时要加在形参前面，在方法调用时也要加在实参前面
    此时就是想显式的告诉程序员使用 ref 就是对同一变量的的两个不同的别名
引用传递就好比 C 语言中的指针变量，只要我获得你的内存地址，那我就可以通过地址/我去操作你。
而值传递则是传递的栈内存变量中的内容，只不过是值类型变量直接在栈内存中存储数据内容
    而引用类型变量在栈内存中则是存储实例对象在堆内存中的地址
*/
namespace 值类型与引用类型_值类型与引用类型作为参数传递时的问题_引用传递 {
    public class Program {
        static void Main0(string[] args) {
            int m = 100;
            M1(ref m);
            Console.WriteLine(m);// 101

            // =============================================
            Person p1 = new Person();
            p1.Name = "Tom";
            M2(ref p1);
            Console.WriteLine(p1.Name);// Tim
            // 因为此时是引用传递，因此此时实参 p1 和 形参 p2 其实就是同一块栈内存地址空间的两个不同的别名
            // 故我们在 M1 中改变了引用变量 p2 的指向，也即将 p2 所指向的那个栈内存地址空间中存储了一个新的
            // Person 对象的堆内存地址，又由于引用变量 p1 也是指向同一块栈内存空间，因此此时再通过 p1 去获得
            // Person 对象的 Name，就会获得新创建的那个 Person 对象的 Name 属性
            // 引用传递就好比 C 语言中的指针变量，只要我获得你的内存地址，那我就可以通过我本身去操作你。
            // =============================================

            Person p2 = new Person();
            p2.Name = "Tim";
            M3(ref p2);
            Console.WriteLine(p2.Name);// Tam
        }

        static void M3(ref Person p3) {
            p3.Name = "Tam";
        }

        static void M2(ref Person p2) {
            p2 = new Person();
            p2.Name = "Tim";
        }

        static void M1(ref int n) {
            n = n + 1;
        }
    }

    public class Person {
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name;

    }
}

/*
值类型：
int、char、double、float、long、short、byte、enum、struct、decimal
值类型都是派生自 ValueType
值类型不能继承，只能实现接口（结构体可以实现接口）

引用类型：
string、数组、类（自定义数据类型）、接口、委托
int[] n = {1,2,3};引用类型
引用类型都派生自 Object
引用类型可以继承（类之间可以继承）

普通参数的传递都是值传递。
*/
namespace 值类型与引用类型_值类型与引用类型作为参数传递时的问题_值传递 {
    public class Program {
        static void Main0(string[] args) {
            Person p = new Person();
            p.Name = "Tom";
            //M1(p); Console.WriteLine(p.Name); Tom
            //M2(p); Console.WriteLine(p.Name); Tim
            Console.WriteLine(p.Name);// Tom
        }

        static void M3(Person p) {
            p = new Person();
            p.Name = "Tim";
            p.Name = "Tam";
        }

        static void M2(Person p) {
            p.Name = "Tim";
            p = new Person();
            p.Name = "Tam";
        }

        // 始终记得在方法调用时，会创建实参和形参两个变量！！！
        // 方法在调用时会将栈内存中实参 p 的数据（即对象在堆内存中的地址）拷贝一份给形参 p：这里形参与实参的变量名相同但是并不是同一个变量而是两个不同的变量
        static void M1(Person p) {
            Person newP = new Person();
            newP.Name = "Tim";
            p = newP;// 此时将新创建的 Person 对象指向了形参 p，而对实参没有影响
        }
    }

    public class Person {
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Name;
    }

}

//简单工厂设计模式：就是说一个方法的返回值是父类类型，在这个方法中动态的返回一个子类对象（即被父类引用变量引用的子类对象）
// 设计模式就是对多态各种各样巧妙的运用，设计模式就是前人总结出来的经验，即遇到这种情况可以这样写，可以这样封装一个方法
namespace 封装计算方法面向对象计算器 {
    public class Program {
        static void Main0(string[] args) {
            Console.WriteLine("pls input a number:");
            double d1 = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("pls input a calculator:");
            string calcu = Console.ReadLine(); 
            Console.WriteLine("pls input another number:");
            double d2 = Convert.ToDouble(Console.ReadLine());
            Calculator cal = /*null*/ GetCalculatorObject(calcu,d1,d2);
            //switch (calcu) {
            //    case "+":
            //        cal = new Add(d1, d2);
            //        break;
            //    case "-":
            //        cal = new Subtraction(d1,d2);
            //        break;
            //}
            if (cal != null) {
                Console.WriteLine($"计算结果是：{cal.Calculate()}");
            } else {
                Console.WriteLine("There is no such function.");
            }

        }

        /// <summary>
        /// 简单工厂设计模式：就是说一个方法的返回值是父类类型，在这个方法中动态的返回一个子类对象（即被父类引用变量引用的子类对象）
        /// 设计模式就是对多态各种各样巧妙的运用，设计模式就是前人总结出来的经验，即遇到这种情况可以这样写，可以这样封装一个方法
        /// </summary>
        /// <param name="calcu"></param>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        private static Calculator GetCalculatorObject(string calcu, double d1, double d2) {
            Calculator cal = null;
            switch (calcu) {
                case "+":
                    cal = new Add(d1, d2);
                    break;
                case "-":
                    cal = new Subtraction(d1,d2);
                    break;
                case "*":
                    cal = new Multiplication(d1,d2);
                    break;
            }
            return cal;
        }
    }

    /// <summary>
    /// 计算器
    /// </summary>
    public abstract class Calculator {
        public Calculator() {

        }

        public Calculator(double d1,double d2) {
            this.Number1 = d1;
            this.Number2 = d2;
        }

        public double Number1 {
            get { return _Number1; }
            set { _Number1 = value; }
        }
        private double _Number1;

        public double Number2 {
            get { return _Number2; }
            set { _Number2 = value; }
        }
        private double _Number2;

        public abstract double Calculate();

    }

    /// <summary>
    /// 加法
    /// </summary>
    public class Add : Calculator {

        public Add() {

        }

        public Add(double d1,double d2) : base(d1,d2) {

        }

        public override double Calculate() {
            return this.Number1 + this.Number2;
        }
    }

    /// <summary>
    /// 减法
    /// </summary>
    public class Subtraction : Calculator {

        public Subtraction() {

        }

        public Subtraction(double d1, double d2) : base(d1, d2) {

        }

        public override double Calculate() {
            return this.Number1 - this.Number2;
        }
    }

    /// <summary>
    /// 乘法
    /// </summary>
    public class Multiplication : Calculator {

        public Multiplication() {

        }

        public Multiplication(double d1,double d2) : base(d1,d2) {

        }

        public override double Calculate() {
            return this.Number1 * this.Number2;
        }
    }
}

namespace 抽象类练习_U盘_计算器案例 {

    public class Program {
        static void Main0(string[] args) {
            //Computer deli = new Computer();

            //UDisk sandisk = new UDisk();
            //MoblieDisk md = new MoblieDisk();
            //MP3 mp3 = new MP3();

            //deli.Dev = mp3;
            //deli.PC_Read();
            //deli.PC_Write();

            Duck duck = new RubberDuck();
            duck.Swim();
            duck.Bark();

        }
    }

    /*
     抽象类中可以有非抽象方法与属性等，但只要类中有一个抽象方法，那么此类就要被 abstract 修饰成为抽象类
     抽象方法不允许有任何实现，甚至是一对花括号{} 都不允许写，故因此继承抽象类的子类一定要实现抽象方法
     */
    public abstract class Duck {
        public void Swim() {
            Console.WriteLine("Duck is swimming ... ");
        }

        public abstract void Bark();

        public void Fuck(){
            Console.WriteLine("Fuck it!");
        }
    }

    public class RealDuck : Duck {
        public override void Bark() {
            Console.WriteLine("Real Duck is gagaga ...");
        }
    }

    public class RubberDuck : Duck {
        public override void Bark() {
            Console.WriteLine("Rubber Duck is jijiji ...");
        }
    }

    /// <summary>
    /// 电脑
    /// </summary>
    public class Computer {
        public MobileStorageDev Dev {
            get { return _Dev; }
            set { _Dev = value; }
        }
        private MobileStorageDev _Dev;

        public void PC_Read() {
            Dev.Read();
        }

        public void PC_Write() {
            Dev.Write();
        }
    } 

    /// <summary>
    /// 可移动存储设备
    /// </summary>
    public abstract class MobileStorageDev {
        public abstract void Read();

        public abstract void Write();
    }

    /// <summary>
    /// U盘
    /// </summary>
    public class UDisk : MobileStorageDev {
        public override void Read() {
            Console.WriteLine("UDisk reading data ... ");
        }

        public override void Write() {
            Console.WriteLine("UDisk writing data ... ");
        }
    }

    /// <summary>
    /// 移动硬盘
    /// </summary>
    public class MoblieDisk : MobileStorageDev {
        public override void Read() {
            Console.WriteLine("MoblieDisk reading data ... ");
        }

        public override void Write() {
            Console.WriteLine("MoblieDisk writing data ... ");
        }
    }

    /// <summary>
    /// MP3
    /// </summary>
    public class MP3 : MobileStorageDev {
        public override void Read() {
            Console.WriteLine("MP3 reading data ... ");
        }

        public override void Write() {
            Console.WriteLine("MP3 writing data ... ");
        }

        public void PlayMusic() {
            Console.WriteLine("MP3 is playing music.");
        }
    }

    

}
