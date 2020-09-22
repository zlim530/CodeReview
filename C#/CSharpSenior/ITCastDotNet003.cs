using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

/**
 * @author zlim
 * @create 2020/5/13 17:31:35
 */

namespace 弱引用 {
    public class Program {
        static void Main0(string[] args) {
            Person p = new Person();
            p.Name = "Tim";
            WeakReference wr = new WeakReference(p);// 这个就表示将对象 p 弱引用起来
            Console.WriteLine("======将 p 置为 null=========");
            p = null;

            Console.WriteLine("======将 p 置为 null，又执行了一些其他逻辑=======");
            Console.WriteLine("======现在又想使用 p 这个对象，如果没有将 p 弱引用起来，则此时只能重新创建一个对象=====");
            Console.WriteLine("======因为之前将 p 使用 WeakReference wr = new WeakReference(p) 弱引用起来，此时如果想使用 p，可以通过 wr.Target 获得======");

            // 重新使用 p 对象
            if (wr.IsAlive) {
                object o = wr.Target;
                if (o != null) {
                    Person p1 = o as Person;
                    // 然后就可以使用 p1 对象了，此时对象 p1 与之前的对象 p 是一模一样的，包括各种属性的值等等
                    Console.WriteLine(p1.Name);
                }
            }

            //object o = wr.Target;
            //if (o != null) {
            //    Person p1 = o as Person;
            //    // 然后就可以使用 p1 对象了，此时对象 p1 与之前的对象 p 是一模一样的，包括各种属性的值等等
            //}

            // 注意：不要这么写：因为我们先判断不为空再将 wr 中的对象引用起来，很有可能在多线程中在 if 判断时的那一瞬间 GC 会
            // 因为内存不够用而回收掉这个对象
            //if (wr.Target != null) {
            //    object o = wr.Target;
            //    Person p2 = new Person;
            //}

        }
    }

    public class Person {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}

/*
值类型变量，在栈内存的变量用完之后立刻回收，根本不需要进行垃圾回收；只有堆内存中的变量才需要垃圾回收。
CLR 的核心功能：垃圾回收（Garbage Collection,GC），垃圾回收是 CLR 自动执行的，一般不需要手动干预。
垃圾回收的目的：提高内存利用率
垃圾回收器：只会回收托管堆中内存资源，而不会回收其他资源（数据库连接、文件句柄、网络端口等）
什么样的对象才会被回收？
    没有变量引用的对象：此时仅表示可以被回收（null），但不代表 GC 就会立即回收这些对象
什么时候回收？
    不确定，当程序需要内存时会 CLR 就会开始执行垃圾回收
    GC.Collect(); 手动调用垃圾回收器：不建议使用，垃圾回收会暂停一下（非常短暂），让程序去自动 GC 
垃圾回收器中“代”的概念：
    共三代：第0代、第1代、第2代
    各代的回收频率比较：第0代最高，其次第1代，再次第2代。也就是说越老的对象生存几率越大，因为越老的对象在越后面的代中，
    而越后面的代要等到前面的代满了才会回收第二代
.NET 中垃圾回收机制：mark-and-compact(标记和压缩)，一开始假设所有对象都是垃圾。
除了内存资源外的其他资源怎么办？使用 Dispose() 方法,需要程序员手动调用
*/    
namespace 垃圾回收 {
    public class Program {
        static void Main0(string[] args) {
            //int n = GC.MaxGeneration;// 2:即0、1、2代
            //Console.WriteLine(n);
        }
    }
}

namespace 字符串练习 {
    public class Program {
        static void Main0(string[] args) {
            #region 练习1：123-456---7---89----123----2把类似的字符串中重复符号“-”去掉，并且得到123-456-7-89-123-2

            //string msg = "123-456---7---89----123----2";
            //string[] parts = msg.Split(new char[] { '-'},StringSplitOptions.RemoveEmptyEntries);
            //msg = string.Join("-",parts);
            //Console.WriteLine(msg);

            #endregion

            #region 练习2：从文件路径中提取出文件名（包括后缀）。比如从 c:\a\b.txt 中提取出 b.txt 这个文件名

            //string path = @"c:\a\b.txt";
            //path = path.Substring(path.LastIndexOf(@"\") + 1);
            //Console.WriteLine(path);

            #endregion

            #region 练习3：“192.168.10.2[port=21,type=ftp]”，这个字符串表示 IP 地址为 192.168.10.5 的服务器的21端口提供的是 ftp 服务，其中如果“,type=ftp”部分被省略则默认为 http 服务。请用程序解析字符串，然后打印出 ip 地址、端口号与服务类型

            //string msg = "192.168.10.5[port=21,type=ftp]";
            //string[] parts = msg.Split(new string[] { "[port=", "type=", "]" },StringSplitOptions.RemoveEmptyEntries);
            //Console.WriteLine($"ip:{parts[0]}");
            //Console.WriteLine($"port:{parts[1]}");
            //Console.WriteLine("service:{0}",parts.Length == 3 ? parts[2] : "http");

            #endregion
            #region "Hello,today is a nice day,hello mom,hello dad,hello grandmon,hello everyone!"统计 hello 出现的次数。

            //string msg = "Hello,today is a nice day,hello mom,hello dad,hello grandmon,hello everyone!";
            //int count = 0;
            //int index = 0;
            //while ( (index = msg.IndexOf("hello", index)) != -1) {
            //    count++;
            //    index = index + "hello".Length;
            //}
            //Console.WriteLine($"count = {count}");

            #endregion

        }
    }
}


/*
字符串是一个 sealed 密封类，其他任何类都不能从 string 类派生。
    为什么字符串类 string 要加上 sealed 关键字修饰？
        1.子类如果继承字符串类以后可能会对字符串类进行修改可能会改变字符串的特性；
        2.CLR 对字符串提供了各种各样的操作方式，如果有很多类继承了字符串类，则 CLR 需要对更多的类提供特殊操作，
          这样有可能会降低性能
字符串的两个特性：
1.不可变性：
2.字符串暂存池（拘留池）（仅针对字符串常量）：依赖于字符串的“不可变性”，如果没有不可变性不可能有“池”
    内部维护一个哈希表 key 作为字符串，value 作为地址，每次为一个新变量赋值都会找 key 中是否有，
    如果有则直接把 value 中的地址赋值给新变量
3.（暂留、拘留、驻留）字符串留用（Intern，针对变量常量），
    对于动态字符串本身在哈希表中没有，通过这种 Intern 可以添加到该哈希表中，目的是为了提高性能
    ·String.Intern(str)：Intern 方法使用暂存池来搜索是否有与 str 值相等的字符串。
        如果存在这样的字符串，则返回暂存池中此字符串的引用，如果不存在，则向暂存池中添加 str 
        该字符串对象，然后返回该引用；
    ·String.IsInterned(str)：此方法在暂存池中查找 str，如果将 str 放入暂存池中，则返回对
        该字符串对象的引用，否则返回 ArgumentNullException 异常
*/
namespace 字符串特性之不可变性_字符串池 {

    public class Program {
        static void Main0(string[] args) {
            // 1，字符串一旦被创建就不可被修改：字符串的不可变性
            string s1 = "Hello,World!";
            string s2 = s1.ToUpper();
            Console.WriteLine(s1);// Hello,World!
            Console.WriteLine(s2);// HELLO,WORLD!

            string s3 = "abc";
            string s4 = "x";
            s3 = s3 + s4;
            Console.WriteLine(s3);

        }
        
        static void Main1(string[] args) {
            // 字符串默认只会将字符串常量加入到字符串常量池中
            string s1 = "abc";
            string a = "a";
            string b = "b";
            string c = "c";
            string s2 = a + b + c;
            // 字符串池中的字符串在程序退出时才会释放池中字符串对象的内存
            // 因此字符串常量池中仅会存储字符串常量（字符串字面量），而不会将字符串变量也存入池中
            string s3 = "a" + "b" + "c";
            Console.WriteLine(object.ReferenceEquals(s1, s2));// False
            Console.WriteLine(object.ReferenceEquals(s1, s3));// True

        }
    
    }

    public class Person {
        public string Name { get; set; }
        public int Age { get; set; }

        /*
         抽象类中可以有非抽象方法与属性等，但只要类中有一个抽象方法，那么此类就要被 abstract 修饰成为抽象类
         抽象方法不允许有任何实现，甚至是一对花括号{} 都不允许写，故因此继承抽象类的子类一定要实现抽象方法

         而 virtual 关键字表示此方法为虚方法可以被子类重写，因此虚方法可以有默认实现
         */
        public virtual void SayHi() {
            Console.WriteLine("Hi");
        }
    }


    public class MyClass : Person {

        /*
        2.sealed 关键字用法二：在方法重写的 override 前面标记，表示这个方法其子类不能再重写
        即“MyClass2.SayHi()”：继承成员“MyClass.SayHi()”是密封的，无法进行重写
        */
        public sealed override void SayHi() {
            base.SayHi();
        }
    }

    //public class MyClass2 : MyClass {
    //    public override void SayHi() {
    //        base.SayHi();
    //    }
    //}
    
    // static 关键字在编译器进行编译之后生成的 IL 语言中会变成 abstract 和 sealed 修饰
    // 这表示静态类既不能被实例化，也不能被继承
    static class MyStaticClass { 
    
    }

    /*
    1.sealed 关键字用法一：在类前面标记，表示这个类无法被继承
    如果一个类标记为 sealed 则表示为密封类，则此类不可以作为父类或者基类
    也即其他类都不能从密封类中派生
    public sealed class MyClass {
    }

    public class MyClass2  : MyClass{
    }
    */

    namespace 字符串格式化 {
        public class Program {
            static void Main0(string[] args) {
                Console.WriteLine("hi i'm {0},i'm {1} years old.", "Tim", 22);
                string s = string.Format("hi i'm {0},i'm {1} years old.", "Tim", 22);
                Console.WriteLine(s);
            }
        }
    }
}

/*
Equals、==、ReferenceEquals方法：判断是否为同一个对象
为什么字符串的 Equals 方法和别的不一样？
    string 的 Equals 方法判断的是字符串的内容是否相同（重写了 object 中的 Equals 方法）
    如果两个字符串中的内容完全一样，则返回 true，并不能达到判断两个对象是否为同一个对象的效果
    并且 string 内存重载了 Equals 方法，其判断效果与重写的 Equals 一样
    而 object 中默认的 Equals 方法是判断对象的地址是否相同
    并且 string 内部对 == 操作符进行了重载，也是对两个字符串的内容进行判断，所以 == 表现出了与 Equals() 一样的效果
object.Equals; 用来比较两个对象的地址是否相同，但是此方法可以被任何类重写
    如何判断 str1 和 str2 是否为同一个对象？Equals 已经不可靠了，“==”也不靠谱。
    （“==”运算符重载后也是调用的 EqualsHelper() 来判断的，与 Equals 方法一样）
判断两个对象是否相同要用：object.ReferenceEquals(object objA,object objB) 方法
    如果必须判断两个对象的地址是否相同，请使用 object.ReferenceEquals(object objA,object objB) 方法
*/
namespace 判断两个变量是否为同一个对象 {
    public class Program {
        static void Main0(string[] args) {
            Person p1 = new Person();
            p1.Name = "Tim";
            p1.Age = 22;

            Person p2 = new Person();
            p2.Name = "Tim";
            p2.Age = 22;
            
            Console.WriteLine(p1 == p2);// False
            Console.WriteLine(p1.Equals(p2));// False：如果 Person 内部重写了 Equals 方法则结果为 True
            Console.WriteLine(object.ReferenceEquals(p1,p2));// False

            // 两个引用变量引用了同一个对象/实例
            p1 = p2;

            Console.WriteLine(p1 == p2);// True
            Console.WriteLine(p1.Equals(p2));// True
            Console.WriteLine(object.ReferenceEquals(p1, p2));// True

        }

        static void Main1(string[] args) {
            // s1 和 s2 是同一个对象
            string s1 = "abc";
            string s2 = "abc";

            Console.WriteLine(s1 == s2);// True
            Console.WriteLine(s1.Equals(s2));// True
            Console.WriteLine(object.ReferenceEquals(s1,s2));// True

            // s3 和 s4 是两个不同的对象，因为 new 了两次，在堆内存中确实存在两块不同的内存
            string s3 = new string(new char[] { 'a', 'b', 'c' });
            string s4 = new string(new char[] { 'a', 'b', 'c' });

            Console.WriteLine(s3 == s4);// True
            Console.WriteLine(s3.Equals(s4));// True
            // 总结1：使用 object.ReferenceEquals() 可以始终准确验证两个变量是否为同一个对象
            Console.WriteLine(object.ReferenceEquals(s3, s4));// False
            /*  
            &s1
            0x00000063b538e510
                *&s1: 0x0000023d608dabb0
            &s2
            0x00000063b538e508
                *&s2: 0x0000023d608dabb0
            &s3
            0x00000063b538e500
                *&s3: 0x0000023d608ddf70
            &s4
            0x00000063b538e4f8
                *&s4: 0x0000023d608ddff0
            */
        }

        /*
        string 内部重写的 Equals 方法：
        public override bool Equals(object obj) {
            if (this == null) {
                throw new NullReferenceException();
            }
            string strB = obj as string;
            return ((strB != null) ? (!ReferenceEquals(this, obj) ? ((this.Length == strB.Length) ? EqualsHelper(this, strB) : false) : true) : false);
        }

        并且在内部对 Equals 方法进行了重载
        public bool Equals(string value){
            if (this == null)
            {
                throw new NullReferenceException();
            }
            return ((value != null) ? (!ReferenceEquals(this, value) ? ((this.Length == value.Length) ? EqualsHelper(this, value) : false) : true) : false);
        }
        上面的重载代码逻辑相当于：
        public bool Equals (string value){
            if(this == null){
                throw new NullReferenceException();
            }
            if(value == null){
                return false;
            }
            if(object.ReferenceEquals(this,value)){
                return true;
            }
            if(this.Length != value.Length){
                return false;
            }
            return EqualsHelper(this,value);// 将两个字符串中的字符使用 ASCII 码逐一进行比较，如果两个字符串的内容一致，则返回 true
        }
        字符串内部对 == 运算符进行了重载，即函数名是 == ：
        [__DynamicallyInvokable]
        public static bool operator ==(string a, string b) => Equals(a, b);
        */


    }

    public class Person {

        public Person() {

        }

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

        public override bool Equals(object obj) {
            //return obj is Person person &&
            //       Age == person.Age &&
            //       _Age == person._Age &&
            //       Name == person.Name &&
            //       _Name == person._Name;
            Person p = obj as Person;
            if (p == null) {
                return false;
            } else if (this.Name == p.Name && this.Age == p.Age) {
                return true;
            } else {
                return false;
            }
        }

    }

}

namespace 可变参数和out参数 {
    public class Program {
        static void Main0(string[] args) {
            //Test(2,23,432,45);
            int m = 1000;
            Test1(out m);
            //Console.WriteLine(m);
            string s;
            int h;
            int age = GetInfomation(out s,out h);
            Console.WriteLine(age);
            Console.WriteLine(s);
            Console.WriteLine(h);
        }

        static int GetInfomation(out string name,out int height) {
            name = "Tim";
            height = 165;
            return 22;
        }

        // out 参数在使用之前必须在方法内部为 out 参数赋值
        // out 参数在内存中的机制与 ref 参数是一样的，但是编译器利用了一些特殊选项使得 out 参数不能使用了
        // 1.out 参数无法获取传递进来的变量的值，只能为传递进来的变量赋值
        // 2.out 参数在方法执行完毕之前，必须赋值
        static void Test1(out int x) {
            //Console.WriteLine(x);使用了未赋值的 out 参数“x”
            x = 100;
            x++;
        }

        /*
        1.如果方法有多个参数，可变参数必须作为最后一个参数
        2.可变参数可以传递参数也可以不传递参数，如果不传递参数，则此时 args 数组就是一个长度为0的数组，而不是 null
        3.可变参数也可以直接传递一个数组进来
        */
        public static void Test(params int[] args) {
            if (args != null) {

            }
        }


    }

    

}

/*
什么是异常？
    程序运行时发生的错误。（错误的出现并不总是程序员的原因，有时应用程序会因为最终用户或运行代码的环境改变而发生错误。比如：1.连接数据库时数据库
    服务停电了；2.操作文件时文件没了、权限不足等；3.计算器用户输入的被除数为0；4.使用对象时对象为 null等等）
    .NET 为我们把“发现错误(try)”的代码与“处理错误(catch)”的代码分离开来
异常处理一般代码模式：
    try{
        // 可能发生异常的代码
    } catch {
        // 对异常的处理
    } finally {
        // 无论发生异常、是否捕获异常都会执行的代码
    }
try 块：可能出现问题的代码。当遇到异常时，后续代码不会执行
catch 块：对异常的处理，记录日志（log4NET），继续向上抛出等操作（只有发生了异常才会执行）
finally 块：代码清理、资源释放等。无论是否发生异常都会执行
*/
namespace 异常处理 {
    public class Program {
        static void Main0(string[] args) {
            string s = "Hello,World!";
            s = null;
            try {
                // 当 try 块中某行代码发生异常后，从该行代码开始后面的代码都不会继续执行了
                // 程序会直接跳转到 catch 块中执行
                //Console.WriteLine(s.Length);
                //Console.WriteLine(s.ToString());
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            // ================catch 块的几种写法 ================
            try {
                int n = 10, m = 0;
                int r = n / m;
                Console.WriteLine(r);
            } /*catch  { // 第一种写法：可以捕获 try 块中的所有异常
                Console.WriteLine("发生异常了");
            }*/ 
            /*catch (Exception e) { // 第二种写法：可以捕获 try 块中的所有异常
                Console.WriteLine("发生异常了");
                Console.WriteLine(e.Message);// Attempted to divide by zero.
                Console.WriteLine(e.Source);// CSharpSenior
                Console.WriteLine(e.StackTrace);
                // at 异常处理.Program.Main(String[] args) in C:\Users\Lim\Desktop\code\CodeReview\C#\CSharpSenior\ITCastDotNet003.cs:line 44
            }*/ 
            // 对不同的异常，使用不同的方法是来处理（也即使用多个不同的 catch 块来捕获异常）
            catch (NullReferenceException e) {
                Console.WriteLine($"空指针异常：{e.Message}");
            } catch (DivideByZeroException e) {
                Console.WriteLine($"除数为0异常：{e.Message}");
            } catch (ArgumentException e) {
                Console.WriteLine($"参数异常：{e.Message}");
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }

        }

        static void Main1(string[] args) {
            try {
                Console.WriteLine("11111");
                int n = 10, m = 0;
                int r = n / m;
                Console.WriteLine(r);
                Console.WriteLine("222222");
            } catch (Exception) {
                Console.WriteLine("3333333");
            } finally {
                /*
                如果希望代码无论如何都要被执行，则一定要将代码放在 finally 块中
                1.当 catch 块中有无法捕获到的异常时，程序崩溃，但是程序在崩溃之前还是会执行 finally 块中的代码，
                  而 finally 块后面的代码则由于程序崩溃将无法执行
                2.如果在 catch 块中又引发了异常，则 finally 块中的代码也会在继承引发异常之前执行，但是 finally 
                  块后面的代码则不会执行
                3.当 catch 块中有 return 语句时，finally 块中的代码也会在 return 语句之前执行，但是 finally 块
                  后面的代码就不会执行了
                */
                Console.WriteLine("4444444");
            }
            Console.WriteLine("$$$$$$");

        }

        static void Main2(string[] args) {
            while (true) {
                try {
                    Console.WriteLine("pls input a name:");
                    string name = Console.ReadLine();
                    if (name == "Tim") {
                        // 手动抛出异常
                        // 尽量使用逻辑判断来避免异常处理代码，即尽量不要手动抛出异常，因为异常比较消耗资源
                        throw new Exception("name is illeage!");
                    } else {
                        Console.WriteLine($"name is lleage:{name}");
                    }
                } catch (Exception e) {
                    Console.WriteLine("Happen Exception");
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);
                } 
            }

        }

        static void Main3(string[] args) {
            try {
                M2();
            } catch (Exception) {
                // 此时在 Main 方法中处理异常，就不能再向上抛出，因为 Main 方法就是顶点了
                Console.WriteLine("M2 的在 Main 方法中调用发生了异常");
                //throw; // 此时再使用 throw 语句程序会崩溃
            }
        }
        static void M2() {
            Console.WriteLine("=======");
            Console.WriteLine("=======");
            try {
                M1();
            } catch (Exception) {
                Console.WriteLine("M1 的在 M2 方法中调用发生了异常");
                // M2 处理完异常，继续向上抛出，也即抛给了调用 M2 方法的方法，也即 Main 方法
                throw;
            }
            Console.WriteLine("=======");
            Console.WriteLine("=======");
        }

        static void M1() {
            try {
                int n = 10, m = 0;
                int r = n / m;
                Console.WriteLine($"r = {r}");
            } catch (Exception) {
                Console.WriteLine("M1 方法执行发生了异常");
                // 这种写法只能在 catch 块中写
                // 在 catch 块中使用 throw 语句，并且这种写法只能用在 catch 块中
                // 表示将当前异常继续向上抛出，在这里就是抛给 M2 方法
                throw;
            }
        }

        static void Main4(string[] args) {
            int n = M3();
            Console.WriteLine(n);// 101

            int n2 = M4();
            Console.WriteLine(n2);// 102
        }

        static int M4() {
            int result = 100;
            try {
                result = result + 1;// 101
                int x = 10, y = 0;
                Console.WriteLine(x / y);// 发生异常，try 块后续的代码将不会执行，而是执行 catch 块中的代码和 finally 块中的代码
                return result;
            } catch (Exception) {
                Console.WriteLine("catch 块被执行了。。。");
                result = result + 1;
                return result;
            } finally {
                Console.WriteLine("finally 块被执行了。。。");
                result = result + 1;// finally 中的代码一定是会执行的，但是最后 M1 方法的返回值仍然是101 
            }
        }

        static int M3() {
            int result = 100;
            try {
                result = result + 1;// 101
                return result;
            } catch (Exception) {
                result = result + 1;
                return result;
            } finally {
                result = result + 1;// finally 中的代码一定是会执行的，但是最后 M1 方法的返回值仍然是101 
            }
        }
        // 因为.NET 编译器在编译 C# 代码时，生成的中间语言 IL 在方法被调用时会为方法的参数和返回值分别创建变量
        // 也即就算我写一个方法 这个方法体就一条 return 语句：return 1000；
        // 我没有在方法内存生成任何变量，但在编译之后生成的 IL 语句中就会有一个变量，那个变量就是这个方法的返回值
        // 这里也是的，编译器为 M3 方法的返回值创建了一个变量，并在执行 try 块 return result; 语句时将这个变量赋值为
        // result + 1; 而后续 finally 块中的代码也得到了执行，只不过它这里是对 result + 1，而不是对那个返回值变量
        // 进行了 + 1,编译后的 IL 源码如下所示
        /*
        private static int M3(){
            int CS$1$0000;
            int result = 100;
            try {
                result++;
                CS$1$0000 = result;
            } catch (Exception) {
                result++;
                CS$1$0000 = result;
            } finally {
                result ++;
            }   
            return CS$1$0000;
        }

        */

        static void Main5(string[] args) {
            Person p = GetPerson();
            Console.WriteLine(p.Age);// 102 :没有引发异常时输出为 102，引发异常时输出为 103
        }

        static Person GetPerson() {
            Person p = new Person();
            p.Age = 100;
            try {
                p.Age = p.Age + 1;
                // =======引发异常代码=======
                int x = 10, y = 0;
                Console.WriteLine(x / y);
                // =======引发异常代码=======
                return p;
            } catch (Exception) {
                p.Age = p.Age + 1;
                return p;
            } finally {
                p.Age = p.Age + 1;
            }
        }
        /*
        编译之后的源码为：
        private static Person GetPerson(){
            Person CS$1$0000;
            Person p = new Person{
                Age = 100;
            };
            try {
                p.Age ++;
                int x = 10;
                int y = 0;
                Console.WriteLine((int)(x / y));
                CS$1$0000 =  p;// 引用变量赋值，就表示 CS$1$0000 和 p 都指向了堆内存中的同一个 Person 对象
            } catch (Exception) {
                p.Age ++;
                CS$1$0000 =  p;
            } finally {
                p.Age ++;// 因此在这里通过 p 去修改 Person 对象 Age 属性的值，再通过 CS$1$0000 去访问同一个对象的
                         // Age 属性值，自然而然也会修改· 
            }
            return CS$1$0000;
        }
        */

    }

    public class Person {

        public int Age {
            get { return _Age; }
            set { _Age = value; }
        }
        private int _Age;

    }
}

/*
类型转换 Cast 是在内存级别上的转换，内存中的数据没有变化，只是观看的视角不同而已。
什么情况下会发生隐式类型转换？
    1.把子类类型赋值给父类类型的时候会发生隐式类型转换
    2.把占用字节数小的数据类型赋值给占用字节数大的数据类型可以发生隐式类型转换（前提是这两种数据库类型在内存的同一个区域：例如均在栈内存中）
Math.Round(); 四舍五入
Convert.ToInt32(); 四舍五入
*/
namespace 显式类型转换 {
    public class ITCastDotNet003 {
        static void Main0(string[] args) {
            Console.WriteLine(sizeof(bool));// 1 
            Console.WriteLine(sizeof(byte));// 1
            Console.WriteLine(sizeof(char));// 2
            Console.WriteLine(sizeof(short));// 2
            Console.WriteLine(sizeof(int));// 4
            Console.WriteLine(sizeof(long));// 8
            Console.WriteLine(sizeof(float));// 4
            Console.WriteLine(sizeof(double));// 8
            Console.WriteLine(sizeof(decimal));// 16
        }
    }
}


