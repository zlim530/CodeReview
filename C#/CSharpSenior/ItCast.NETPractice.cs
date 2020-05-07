using System;

namespace 通过虚方法实现方法重写_多态 {
    class Program {
        static void Main0(string[] args) {
            Person[] people = new Person[5];
            people[0] = new American();
            people[1] = new Japanese();
            people[2] = new Chinese();
            people[3] = new American();
            people[4] = new Japanese();

            for (int i = 0; i < people.Length; i++) {
                //if (people[i] is American) {
                //    ((American)people[i]).SayNationality();
                //} else if (people[i] is Chinese) {
                //    ((Chinese)people[i]).SayNationality();
                //} else if (people[i] is Japanese) {
                //    ((Japanese)people[i]).SayNationality();
                //}
                people[i].SayNationality();// 这句话就体现了多态：同一条语句，根据里面变量的对象不同，实际调用的方法是不同的；多态的目的是程序想扩展时，增加新的类，而不需要修改调用部分的源代码
            }
            Console.ReadLine();
        }
    }


    public class Person {
        public string Name { get; set; }

        public int Age { get; set; }

        // 第一步：将父类中的对应的方法前加上 virtual 关键字,即将对应的方法变为“虚方法”
        // 虚方法：即表示子类可以对此方法进行重写
        // 不仅方法可以重写，属性也可以重写
        public virtual void SayNationality() => Console.WriteLine("The Earth.");
    }

    public class American : Person {
        // 通过使用 override 关键字将父类中的虚方法 “SayNationality” 重写为子类自己想要的
        public override void SayNationality() {
            Console.WriteLine("USA");
        }
    }

    public class Japanese : Person{
        public override void SayNationality() {
            Console.WriteLine("Japan");
        }
    }

    public class Chinese : Person {
        public override void SayNationality() {
            Console.WriteLine("China");
        }
    }
}

namespace 访问级别约束问题 {

    // --------------------------情况一--------------------------
    //class Person {

    //}

    // 错误：可访问行性不一致：基类“Person”的可访问行低于类“Student”
    // public class Student : Person {
    // 
    // }


    // --------------------------情况二--------------------------

    //class Person {
    //    public string Name { get; set; }
    //}

    // 错误：可访问性不一致：参数类型“Person”的可访问性低于方法“MyClass.SayHi(Person)”
    // 方法的访问修饰符需要和方法参数类型的访问修饰符一致
    //public class MyClass {
    //    public void SayHi(Person per){
    //        Console.WriteLine(per.Name);
    //    }
    //}

    // --------------------------情况三--------------------------

    //class Person {

    //}

    // 没有问题：虽然方法是 public 的，但是方法所在类的访问级别为 internal，故没有问题
    //class MyClass {
    //    public void SayHi(Person per) {
    //        Console.WriteLine(per);
    //    }
    //}

    // --------------------------情况四--------------------------

    //class Person {

    //}

    // 有问题：可访问行不一致：返回类型“Person”的可访问行低于方法“MyClass.GetPerson()”
    // 方法的参数与方法的返回值都必须和方法保持一致的访问修饰符
    //public class MyClass {
    //    public Person GetPerson() {
    //        return new Person();
    //    }
    //}

    // --------------------------情况五--------------------------

    //class Person {

    //}

    // 错误：可访问行不一致：属性类型“Person”的可访问行低于属性“MyClass.Person”
    // 属性实际上最后也被编译成私有字段与对应的方法
    //public class MyClass {
    //    public Person Person { get; set; }
    //}

}

namespace 通过this与base来调用类中成员 {
    class Program {
        static void Main0(string[] args) {
            Student stu = new Student("Tim",23,101);// 这句话只创建一个对象，类只是一个模板而已
            stu.SayHi();
        }
    }

    class Person {

        public Person() {
            this.Name = "Tom";
        }

        public Person(string name,int age) {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }

        public int Age { get; set; }

    }

    class Student : Person {
        public Student(string name,int age,int sid) {
            this.Name = name;
            this.Age = age;
            this.Sid = sid;
        }

        public void SayHi() {
            // 现在 this.name 与 base.name 是一模一样的 
            Console.WriteLine(this.Name);// Tim
            Console.WriteLine(base.Name);// Tim
        }

        public int Sid { get; set; }
    }
}

namespace 访问修饰符_5种_ {
    class Program {
        static void Main0(string[] args) {
            // public protected internal private 默认
        }
    }

    // 命名空间中定义的元素只能声明为 public 或者 internal 或者 什么都不写
    //private class MyClass1 {

    //}

    class MyClass {

        // 类的成员变量（字段与方法），如果不写访问修饰符默认是 private 
        // 类本身如果不写访问修饰符，则默认是 internal 

        // 只有在当前类内部可以访问
        private int n = 10;

        // 当前类内部以及所有子类的内部
        protected int m = 100;

        // 只能在当前程序集内部访问
        internal int x = 1000;

        // 同时具有 protected 和 internal 的访问权限
        protected internal int y = 10000;

        // 不同程序集之间可以访问：即任何地方都能访问
        public void M1() { }

    }
}


namespace 使用this调用自己的构造函数 {
    public class Person {

        public Person() {

        }

        // 使用 this 类调用本类中的其他构造函数
        public Person(int age) : this(string.Empty,age) {

        }

        public Person(string name) : this(name,0) {
            
        }

        public Person(string name,int age) {
            this.Name = name;
            this.Age = age;
        }


        public string Name { get; set; }

        public int Age { get; set; }

    }
}


namespace CSharpSenior {

    #region 继承：是值类与类之间的关系

    /*
    BaseClass   -> 基类     Derived Class -> 派生类
    ParentClass -> 父类     Child Class   -> 子类

    继承的好处：代码重用 以及 多态
    继承的单根性：每个类有且仅有一个直接父类
    继承的传递性：子类可以拥有祖父类的所有属性与方法
    
    当一个子类继承父类之后，该子类中的所有构造函数默认情况下，在自己被调用之前都会先调用一次父类的无参构造函数
    如果此时父类中没有无参构造函数，则会报错！
    解决方法一：
        在父类中添加一个无参构造函数；
        在子类中的构造函数指定调用父类中有参数的构造函数
            在子类的构造函数后面通过 :base(arg1,arg2, ... ) 的方式明确指定要调用父类中的哪个构造函数
            : base() ：表示调用父类的构造函数；并且构造函数是不能被继承的，只能在子类中调用
    */

    class Program2 {
        static void Main0(string[] args) {
            //LSP：里氏替换原则
            // 多态 -> 增加程序的可扩展性、灵活性
            // 需要一个父类类型时，给一个子类类型对象是可以的，这个就叫做里氏替换原则：父类变量引用子类对象
            Person p1 = new Student();
            Person p2 = new Teacher();
            //Animal a1 = new Student();
            Person p = new Person();

            // 这样做不行！！！！！！！
            // Teacher t1 = new Student();

            // 这样是可以的，虽然 p1 是一个 Person 类型的变量
            // 但实际上这个变量中存储的是指向 Student 类型对象的引用 ：Person p1 = new Student(); 
            // 因此将这个变量转换为 Student 类型是可行的
            Student s1 = (Student)p1;

            //Student s = (Student)p;
            // System.InvalidCastException:“Unable to cast object of type 'CSharpSenior.Person' to type 'CSharpSenior.Student'.”
            // 这样是不行的，因为 p 是一个 Person 类型的变量，并且引用着 Person 类型的对象
            // 因此不能转换为 Student 类型的变量
            
            //Student s2 = (Student)p2;
            // System.InvalidCastException:“Unable to cast object of type 'CSharpSenior.Teacher' to type 'CSharpSenior.Student'.”
            // 虽然编译时没有错误，但是运行时抛出了异常，也即统一父类的子类之间是没有任何关系的
        }

        static void Main1(string[] args) {
            //Student stu = new Student("Tim",23,"001");
        }
    }

    class Animal {
        public void Bark() {
            Console.WriteLine("Barking ... ");
        }
    }

    class Person /*: Animal*/  {

        public Person() {

        }

        public Person(string name,int age) {
            this.Name = name;
            this.Age = age;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public void Eat() {
            Console.WriteLine("Eating ... ");
        }
    }

    class Student : Person {

        //public Student(string name,int age,string sid) {
        //    this.Name = name;
        //    this.Age = age;
        //    this.SId = sid;
        //}

        public Student() {

        }

        public Student(string name, int age, string sid) : base( name,  age) {
            this.SId = sid;
        }

        public string SId { get; set; }
    }

    class Teacher : Person {

        //public Teacher(string name, int age, string empid) {
        //    this.Name = name;
        //    this.Age = age;
        //    this.EmpId = empid;
        //}

        public Teacher() {

        }

        public Teacher(string name, int age, string empid) : base(name,age) {
            this.EmpId = empid;
        }

        public string EmpId { get; set; }
    }

    #endregion


    #region 静态类与静态成员

    // 在静态类中，所包含的所有成员必须都是“静态成员”
    public static class MyClass { 
        
    }

    class Program1 {
        // 不是所有的静态成员都必须写在静态类中
        static void Main0() {
            Person0 p1 = new Person0();
            p1.Name = "Tim";
            p1.Age = 23;

            // 在程序的任何地方访问静态成员，其实访问的都是同一块内存，故有一个地方将该值改变，则所有地方获取的值都变了
            Person0.Planet = "地球";

            // 实例成员是属于具体某个对象的。
            Person0 p2 = new Person0();
            p2.Name = "Tom";
            p2.Age = 24;
        }

    }

    public class Person0 {

        public string Name { get; set; }

        public int Age { get; set; }

        // 静态成员是属于“类”的，不是属于具体某个对象的
        // 所以访问静态成员时，不能使用 对象名.属性名 来进行访问，只能通过 类名.属性名 进行访问
        // 静态成员的数据直到程序退出后才会释放资源，而实例对象，只要使用完毕就可以被 GC（垃圾回收器）回收
        public static string Planet { get; set; }
    }

    #endregion

    #region 实现多态的手段1-虚方法 virtual

    class ParentClass {
        public virtual void Method1() {
            Console.WriteLine("Parent Class 中的 Method1");
        }
    }

    class A : ParentClass {
        //public override void Method1() {
        //    Console.WriteLine("A Class 中的 Method1");
        //}

        // 这个表示子类全新的添加一个 Method1 方法，为什么可以添加一个与父类 ParentClass 中一模一样的 Method1 方法呢？
        // 因为这里使用了 new 关键字，将从父类继承下来的那个 Method1 方法隐藏了（注意是隐藏而不是删除）
        // 所以此时，这个类中只有一个 Method1 方法，通过 this.Method1() 调用的一定是子类中的全新的这个 Method1 方法
        // 但是如果通过 base.Method1() 则调用的是父类中原来的那个 Method1 方法
        // 并且如果类 A 在隐藏 Method 方法时没有加上 virtual 关键字，则它的子类（如 B）在后面就不能重写这个方法
        public new virtual void Method1() {
            Console.WriteLine("A Class 中的 Method1");
        }
    }

    class B : A {
        public override void Method1() {
            Console.WriteLine("B Class 中的 Method1");
        }
    }

    class Program {
        static void Main0() {
            ParentClass a = new A();
            a.Method1();// Parent Class 中的 Method1 如果在子类 A 中，没有使用 new 关键字而是使用 override 关键字，则此时通过父类引用变量的去调用 Method1 方法，会调用子类中重写的那个 Method1 方法，而不是父类中的那个 Method1 方法
            ((A)a).Method1();// A Class 中的 Method1

            A a1 = new A();
            a1.Method1();// A Class 中的 Method1
        }
    }

    #endregion


    #region 基础练习题
    class ItCast {

        
        static void Main0() {

            #region 将字符串"   hello      world,你  好 世界   !     "两端的空格去掉，并且将其中的所有其他空格都替换成一个空格，输出结果为："hello world,你 好 世界 !"

            //string msg = "   hello      world,你  好 世界   !     ";
            //msg = msg.Trim();
            //string[] words = msg.Split(new char[] { ' '},StringSplitOptions.RemoveEmptyEntries);
            //msg = string.Join(" ",words);
            //Console.WriteLine("============" + msg + "============");

            #endregion
            #region 输入姓名小程序

            //string name = string.Empty;
            //int count = 0;
            //List<string> list = new List<string>();
            //do {
            //    Console.WriteLine("请输入姓名:");
            //    name = Console.ReadLine();
            //    if (name.IndexOf('王') == 0) {
            //        count++;
            //    }
            //    list.Add(name);
            //} while (name.ToLower() != "quit");

            //list.RemoveAt(list.Count - 1);
            //Console.WriteLine($"共输入学生个数：{list.Count}");
            //Console.WriteLine("分别为：");
            //for (int i = 0; i < list.Count; i++) {
            //    Console.WriteLine(list[i]);
            //}
            //Console.WriteLine($"姓王的人个数为:{count}");

            #endregion

            #region 请将字符串数组{"中国","美国","巴西","澳大利亚","加拿大"}中的内容反转，然后输出反转后的数组，不能使用数组的 Reverse() 方法

            //string[] msg = { "中国", "美国", "巴西", "澳大利亚", "加拿大" };

            //MyReverse(msg);

            //for (int i = 0; i < msg.Length; i++) {
            //    Console.WriteLine(msg[i]);
            //}
            //Console.ReadKey();

            #endregion


        }

        private static void MyReverse(string[] msg) {

            for (int i = 0; i < msg.Length / 2; i++) {
                string temp = msg[i];
                msg[i] = msg[msg.Length - 1 - i];
                msg[msg.Length - 1 - i] = temp;
            }
        
        }

        class TestIndexr {
            public int Count {
                get {
                    return _names.Length;      
                }
             }

            private string[] _names = { "Trm","Tum","Tam","Tim","Tom"};
            
            // 索引器其实就是一个属性，是一个非常特殊的属性，常规情况下索引器其实都是一个名字叫做 Item 的属性，因此此时我们不能再声明一个 Item 的属性
            public string this[int index] {
                get {
                    if (index < 0 || index >= _names.Length ) {
                        throw new ArgumentException();
                    }
                    return _names[index];
                }

                set {
                    _names[index] = value;
                }
            }


            public string this[string useranme] {
                get {
                    
                    return "";
                }

                set {
                }
            }

        }

        static void BobbleSort(int[] array) {
            for (int i = 0; i < array.Length - 1; i++) {
                for (int j = array.Length - 1; j > i; j--) {
                    if (array[j] > array[j - 1] ) {
                        int temp = array[j];
                        array[j] = array[j - 1];
                        array[j - 1] = temp;
                    }
                }
            }
        }

    }
    
    #endregion

}
