using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

/**
 * @author zlim
 * @create 2020/5/15 12:28:27
 */

namespace 控制台的输入输出与字符串格式化 {
    public class Program {
        static void Main0(string[] args) {
            Console.WriteLine("HelloWorld!");
            Console.WriteLine("pls input sth:");
            string input = Console.ReadLine();

            var items = input.Split(new char[] { ' ', ',' }).Where(item => !string.IsNullOrEmpty(item)).ToList();
        }
    }
}

namespace 单例工厂_泛型方法和反射机制_字典和动态编程 {
    public class Program {
        static void Main0(string[] args) {
            MyClient.SingletonRun();
        }
    }

    public abstract class LoggerBase {
        public LoggerLevel Level { get; protected set; }

        /// <summary>
        /// 单例的字典
        /// </summary>
        private static Dictionary<string, LoggerBase> _Instances;

        /// <summary>
        /// 动态创建指定的 Logger 类型的实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="level"></param>
        /// <returns></returns>
        public static LoggerBase EnsureCreate<T>(LoggerLevel level) where T : LoggerBase {
            string typeName = typeof(T).Name;
            if (_Instances == null) {
                _Instances = new Dictionary<string, LoggerBase>();
            }
            if (!_Instances.Keys.Contains(typeName)) {
                _Instances.Add(typeName, (T)Activator.CreateInstance(typeof(T), level));
            }
            _Instances[typeName].Level = level;
            return _Instances[typeName];
        }

        protected LoggerBase(LoggerLevel level) {
            this.Level = level;
        }

        public abstract void WriteLine(string msg);
    }

    public enum LoggerLevel {
        Debug,
        Infomation,
        Caution,
        Fatal
    }

    public class ConsoleLogger : LoggerBase {
        [Obsolete("不允许在外部生成实例", true)]
        public ConsoleLogger(LoggerLevel level) : base(level) {

        }

        public override void WriteLine(string msg) {
            Console.WriteLine("ConsoleLogger is printing ... ");
        }
    }

    public class DebugLogger : LoggerBase {
        [Obsolete("不允许在外部生成实例", true)]// 此时就不可以在外部调用此类的构造函数
        public DebugLogger(LoggerLevel level) : base(level) {

        }

        public override void WriteLine(string msg) {
            Debug.WriteLine("DebugLogger is printing ... ");
        }
    }

    public class LoggerGroup {
        public List<LoggerBase> Loggers { get; }

        public LoggerGroup(params LoggerBase[] loggers) {
            Loggers = loggers.ToList();
        }

        public void SimpleRun() {
            Loggers.ForEach(
                log => log.WriteLine("Simlpe run")
                );
        }

    }

    public class MyClient {
        public static void Run() {
            LoggerGroup group = new LoggerGroup(

            );

            group.SimpleRun();
        }

        // 单例工厂的测试代码
        public static void SingletonRun() {
            var logger1 = LoggerBase.EnsureCreate<ConsoleLogger>(LoggerLevel.Debug);
            var logger2 = LoggerBase.EnsureCreate<ConsoleLogger>(LoggerLevel.Debug);
            var logger3 = LoggerBase.EnsureCreate<ConsoleLogger>(LoggerLevel.Debug);
            var logger4 = LoggerBase.EnsureCreate<ConsoleLogger>(LoggerLevel.Debug);
            var logger5 = LoggerBase.EnsureCreate<ConsoleLogger>(LoggerLevel.Debug);
            var logger6 = LoggerBase.EnsureCreate<ConsoleLogger>(LoggerLevel.Fatal);

            var debug1 = LoggerBase.EnsureCreate<DebugLogger>(LoggerLevel.Fatal);
            var debug2 = LoggerBase.EnsureCreate<DebugLogger>(LoggerLevel.Debug);
        }
    }
}

namespace 工厂模式_面向抽象编程 {
    public class Program {
        static void Main0(string[] args) {
            MyClient.Run();
        }
    }

    public abstract class LoggerBase {
        public LoggerLevel Level { get; }

        protected LoggerBase(LoggerLevel level) {
            this.Level = level;
        }

        public abstract void WriteLine(string msg);
    }

    public enum LoggerLevel {
        Debug,
        Infomation,
        Caution,
        Fatal
    }

    public class ConsoleLogger : LoggerBase {

        public ConsoleLogger(LoggerLevel level) : base(level) {

        }

        public override void WriteLine(string msg) {
            Console.WriteLine("ConsoleLogger is printing ... ");
        }
    }

    public class DebugLogger : LoggerBase {

        public DebugLogger(LoggerLevel level) : base(level) {

        }

        public override void WriteLine(string msg) {
            Debug.WriteLine("DebugLogger is printing ... ");
        }
    }

    public class LoggerGroup {
        public List<LoggerBase> Loggers { get; }

        public LoggerGroup(params LoggerBase[] loggers) {
            Loggers = loggers.ToList();
        }

        public void SimpleRun() {
            Loggers.ForEach(
                log => log.WriteLine("Simlpe run")
                );
        }

    }

    public class MyClient {
        public static void Run() {
            LoggerGroup group = new LoggerGroup(
                new ConsoleLogger(LoggerLevel.Debug),
                new DebugLogger(LoggerLevel.Fatal)
            );

            group.SimpleRun();
        }
    }



}

namespace 工厂模式_面向接口编程 {
    public class Program {
        static void Main0(string[] args) {
            //PhoneGroup.Run();
            PhoneGroup group = new PhoneGroup(new Nokia(), new Apple(), new Apple(), new Apple());
            group.SimpleRun();
        }
    }

    public interface IPhone {
        void WriteLine(string msg);
    }

    public class Nokia : IPhone {
        public void WriteLine(string msg) {
            Console.WriteLine($"Message from Nokia:{msg}");
            Debug.WriteLine($"Message from Nokia:{msg}");
        }
    }

    public class Apple : IPhone {
        public void WriteLine(string msg) {
            Console.WriteLine($"Message from Apple:{msg}");
            Debug.WriteLine("HelloWorld");
            /*
            二者同样是输入，但 Debug 是输出到 output 窗口，而 Console 是输出到控件台窗口，   
            而且 Debug 必须要在 Debug 情况下才有效，你按 Ctrl+F5 后会看到Console的输出，
            按F5后也能看到Console的输出，还可以看到output中Debug的输出
            */
        }
    }

    public class PhoneGroup {
        public static void Run() {
            Nokia nokia = new Nokia();
            Apple apple = new Apple();

            nokia.WriteLine("What a happy day!");
            apple.WriteLine("What a bad day!");
        }

        public List<IPhone> Phones { get; }

        public PhoneGroup(params IPhone[] phones) {
            Phones = phones.ToList();
        }

        public void SimpleRun() {
            Phones.ForEach(
                phone => phone.WriteLine("Simlpe run")
                );
        }

    }

}

namespace 单例模式_简单的单例实现 {
    public class Program {
        static void Main0(string[] args) {
            SingletonTrap trap1 = SingletonTrap.Instance;
            SingletonTrap trap2 = SingletonTrap.Instance;
            SingletonTrap trap3 = SingletonTrap.Instance;
            SingletonTrap trap4 = SingletonTrap.Instance;
            /*
            SingletonTrap create x1
            SingletonTrap create x2
            SingletonTrap create x3
            SingletonTrap create x4
            */
        }

        static void Main1(string[] args) {
            SingletonSimple single1 = SingletonSimple.Instance;
            SingletonSimple single2 = SingletonSimple.Instance;
            SingletonSimple single3 = SingletonSimple.Instance;
            SingletonSimple single4 = SingletonSimple.Instance;
            // SingletonSimple create x1
        }

        static void Main2(string[] args) {
            Thread p1 = new Thread(() => SingletonUnsafe.GetInstance());
            Thread p2 = new Thread(() => SingletonUnsafe.GetInstance());
            Thread p3 = new Thread(() => SingletonUnsafe.GetInstance());
            Thread p4 = new Thread(() => SingletonUnsafe.GetInstance());
            Thread p5 = new Thread(() => SingletonUnsafe.GetInstance());

            p1.Start();
            p2.Start();
            p3.Start();
            p4.Start();
            p5.Start();

            p1.Join();
            p2.Join();
            p3.Join();
            p4.Join();
            p5.Join();
            /*
            SingletonSimple create x4
            SingletonSimple create x1
            Singleton is dummy created!!!
            SingletonSimple create x3
            Singleton is dummy created!!!
            Singleton is dummy created!!!
            SingletonSimple create x2
            SingletonSimple create x5
            Singleton is dummy created!!!
            Singleton is dummy created!!!
            */
        }

        static void Main3(string[] args) {
            Thread p1 = new Thread(() => { var tmp = SingletonSafe.Instance; });
            Thread p2 = new Thread(() => { var tmp = SingletonSafe.Instance; });
            Thread p3 = new Thread(() => { var tmp = SingletonSafe.Instance; });
            Thread p4 = new Thread(() => { var tmp = SingletonSafe.Instance; });
            Thread p5 = new Thread(() => { var tmp = SingletonSafe.Instance; });

            p1.Start();
            p2.Start();
            p3.Start();
            p4.Start();
            p5.Start();

            p1.Join();
            p2.Join();
            p3.Join();
            p4.Join();
            p5.Join();
            /*
            Singleton [Safe] is at the door
            Singleton [Safe] is at the door
            Singleton [Safe] is at the door
            Singleton [Safe] is at the door
            SingletonSimple create x1
            Singleton [Safe] is at the door
            */
        }

    }

    public class SingletonSafe {
        private static int _Count = 0;
        public string CreateProperty { get; set; }

        private static readonly object _Lock = new object();

        private static SingletonSafe _Instance;

        public static SingletonSafe Instance {
            get {
                if (_Instance == null) {
                    Console.WriteLine($"Singleton [Safe] is at the door");
                    lock (_Lock) {
                        if (_Instance == null) {
                            _Instance = new SingletonSafe();
                        }
                    }
                }
                return _Instance;
            }
        }

        private SingletonSafe() {
            _Count += 1;
            CreateProperty = $"SingletonSimple create x{_Count}";
            Console.WriteLine(CreateProperty);
            if (_Count > 1) {
                Console.WriteLine("Singleton is dummy created!!!");
            }
        }
    }

    public class SingletonUnsafe {
        private static int _Count = 0;
        public string CreateProperty { get; set; }

        private static SingletonUnsafe _Instance;

        public static SingletonUnsafe GetInstance() => _Instance ?? (_Instance = new SingletonUnsafe());

        private SingletonUnsafe() {
            _Count += 1;
            CreateProperty = $"SingletonSimple create x{_Count}";
            Console.WriteLine(CreateProperty);
            if (_Count > 1) {
                Console.WriteLine("Singleton is dummy created!!!");
            }
        }
    }


    public class SingletonSimple {
        private static int _Count = 0;
        public string CreateProperty { get; set; }

        private static SingletonSimple _Instance;

        //public static SingletonSimple Instance {
        //    get {
        //        if (_Instance == null) {
        //            _Instance = new SingletonSimple();
        //        }
        //        return _Instance;
        //    }
        //}
        // 或者写为一行：
        public static SingletonSimple Instance => _Instance ?? (_Instance = new SingletonSimple());

        private SingletonSimple() {
            _Count += 1;
            CreateProperty = $"SingletonSimple create x{_Count}";
            Console.WriteLine(CreateProperty);
        }
    }

    public class SingletonTrap {
        private static int _Count = 0;
        public string CreateProperty { get; set; }

        /*
        适用属性实现单例模式的陷阱：每调用一次 Instance 属性，就会生成一个 SingletonTrap 对象
        */
        public static SingletonTrap Instance {
            get {
                return new SingletonTrap();
            }
        }

        private SingletonTrap() {
            _Count += 1;
            CreateProperty = $"SingletonTrap create x{_Count}";
            Console.WriteLine(CreateProperty);
        }
    }
}

namespace 字段_属性_方法 {
    public class Program {
        static void Main0(string[] args) {
            Person person = new Person();
            person.Age = -1;
            person.Age = 200;
            person.Age = 22;

            Student student = new Student();
            student.BirthDate = DateTime.Parse("1998-04-28");
            Console.WriteLine($"\nStudent Age = {student.Age}");

            // 非链式编程
            Student student1 = new Student();
            student1.SetBirthDate("2014-07-29");
            student1.SetNameAndClassName("YuanYuan", "一班大班");
            Console.WriteLine(student1.ToString() + Environment.NewLine);
            // \n 换行操作仅适用于 Windows 系统
            // 如果你需要跨平台需要用这个 Environment.NewLine 进行换行

            // 链式编程
            Student student2 = new Student();
            //student2.SetBirthDate("1998-04-28").SetNameAndClassName("JiaJia","16电信1班").ToString();
            Console.WriteLine(student2.SetBirthDate("1998-04-28").SetNameAndClassName("JiaJia", "16电信1班").ToString() /*+ Environment.NewLine*/);
        }

        static void Main1(string[] args) {
            Student student3 = new Student("Tim", "1995-09-08");
            Console.WriteLine(student3.ToString());
        }
    }

    public class Person {
        private int _Age = 0;

        public int Age {
            get { return _Age; }
            set {
                if (_Age < 0) {
                    _Age = 0;
                } else if (value > 199) {
                    _Age = 199;
                } else {
                    _Age = value;
                }
                // 在属性中还可以调用方法
                Console.WriteLine($"Age = {_Age}");
            }
        }
    }

    public class Student {
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;

        public string Name { get; private set; }

        public string ClassName { get; set; }

        // 主构造函数
        public Student(string name, string birthDate, string className) {
            this.Name = name;
            this.BirthDate = DateTime.Parse(birthDate);
            this.ClassName = className;
        }

        public Student(string name, string birthDate) : this(name, birthDate, "新生") {

        }

        public Student() {

        }

        public Student SetBirthDate(string birthDat) {
            BirthDate = DateTime.Parse(birthDat);
            return this;
        }

        public Student SetNameAndClassName(string name, string className) {
            this.Name = name;
            this.ClassName = className;
            return this;
        }

        public override string ToString() {
            return $"名字：{this.Name}，班级：{this.ClassName}，年龄：{this.Age}";
        }
    }
}
