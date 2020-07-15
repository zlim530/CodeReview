using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

/**
 * @author zlim
 * @create 2020/6/20 23:24:42
 */
namespace CSharpSenior {
    public class ITCastDotNet007 {

        public static void Main0(string[] args) {
            // 2.使用这个委托：
            // 声明了一个委托变量 md，并且 new 了一个委托对象，而后把方法 M1 作为参数传递进去
            MyDelegate md = new MyDelegate(M1);

            // 3.调用 md 委托的时就相当于调用了 M1 方法
            md();

        }

        static void M1() {
            Console.WriteLine("This is a void Method.");
        }

    }
    /*
    1.定义委托类型：
        定义一个委托类型，用来保存无参数，无返回值的方法
        其中 MyDelegate 为委托的名字，void 表示只能存储没有返回值的方法 MyDelegate() 表示存储没有参数的方法
    */
    public delegate void MyDelegate();

    public class Program {
        public static void Main1(string[] args) {
            //TestClass tc = new TestClass();
            //WriteTimeDelegate writeTime = new WriteTimeDelegate(WritrTimeToFile);
            //WriteTimeDelegate writeTime = new WriteTimeDelegate(PrintTimeToConsole);
            //tc.DoSomething(writeTime);
            //Console.WriteLine("OK");

            string[] names = new string[] { "Tim", "Tom" };
            GetStringDelegate changeString = new GetStringDelegate(ChangeStrings);
            Program.ChangeStrings(names, changeString);
            for (int i = 0; i < names.Length; i++) {
                Console.WriteLine(names[i]);
            }
        }

        public static string ChangeStrings(string name) {
            return name.ToUpper();
        }

        public static void ChangeStrings(string[] strs) {
            for (int i = 0; i < strs.Length; i++) {
                strs[i] = "☆" + strs[i] + "☆";
            }
        }

        public static void ChangeStrings(string[] strs,GetStringDelegate changeString) {
            for (int i = 0; i < strs.Length; i++) {
                strs[i] = changeString(strs[i]);
            }
        }

        public static void WritrTimeToFile() {
            File.WriteAllText("time.txt", System.DateTime.Now.ToString());
        }

        public static void PrintTimeToConsole() {
            Console.WriteLine(System.DateTime.Now.ToString());
        }
    }

    public delegate string GetStringDelegate(string name);

    public class TestClass {
        public void DoSomething(WriteTimeDelegate writeTime) {
            Console.WriteLine("===============");
            Console.WriteLine("===============");
            //Console.WriteLine(System.DateTime.Now.ToString());
            if (writeTime != null) {
                writeTime();
            }
            Console.WriteLine("===============");
            Console.WriteLine("===============");
        }
    }

    public delegate void WriteTimeDelegate();
}

namespace 委托复习 {
    public class Program {
        public static void Main0(string[] args) {
            /*
            1.委托是一种数据类型，与类同级，一般是直接在命令空间中定义；
            2.定义委托时，需要指明返回值类型、委托名与参数列表，这样就可以确定该类型的委托可以存储什么样的方法；
            3.使用委托：
                3.1 声明委托变量
                3.2 委托是引用类型，因此当声明委托变量后，如果不能赋值则该委托变量默认值为 null，因此在使用委托变量之前最好是
                做非空检验：MyDelegate != null
                3.3 委托类型的变量只能赋值一个委托类型的对象，但是同一个委托变量可以委托多个方法
            */
            //MyDelegate md = new MyDelegate(M1);
            //md.Invoke();
            MyDelegate md = M1;
            md();
        }

        public static void Main1(string[] args) {
            while (true) {
                Console.WriteLine("请输入邮箱：");
                string email = Console.ReadLine();
                email = Regex.Replace(email, @"(\w+)(@\w+\.\w)", ReplaceMethod, RegexOptions.ECMAScript);
                Console.WriteLine(email);
            }
        }

        static string ReplaceMethod(Match match) {
            string uid = match.Groups[1].Value;
            string others = match.Groups[2].Value;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < uid.Length; i++) {
                sb.Append("*");
            }
            sb.Append(others);
            return sb.ToString();
        }

        static void M1() {
            Console.WriteLine("M1 function.");
        }
    }

    public delegate void MyDelegate();
}

namespace 匿名方法与Lambda表达式 {
    public class Program {
        public static void Main0(string[] args) {
            // 匿名方法：匿名方法不能直接在类中定义，而是在给委托变量赋值时，需要赋值一个方法，此时可以“现做现卖”，定义一个匿名方法给该委托
            //无参数、无返回值的匿名方法
            MyDelegate md = delegate () {
                Console.WriteLine("Hello,World!");
            };
            //无参数、无返回值的 lambda(λ) 表达式
            md += () => {
                Console.WriteLine("lambda!");
            };
            md();

            //有参数、无返回值的匿名方法
            MyDelegate1 md1 = delegate (string msg) {
                Console.WriteLine(msg);
            };
            md1("Hello,World!2");
            //有参数、无返回值的 lambda 表达式：lambda 表达式不需要表明数据类型，因为委托已经限定了参数的数据类型
            MyDelegate1 md2 = msg => {
                Console.WriteLine(msg);
            };
            md2("hello,Lambda!");

            //有参数、有返回值的匿名方法
            AddDelegate add = delegate (int n1, int n2, int n3) {
                return n1 + n2 + n3;
            };
            Console.WriteLine(add(10, 20, 30));
            //有参数、有返回值的 lambda 表达式
            AddDelegate add1 = (n1,n2,n3) => {
                return n1 + n2 + n3;
            };
            Console.WriteLine(add1(1,2,3) + "lambda!");

            AddDelegate1 add2 = (arr) => {
                for (int i = 0; i < arr.Length; i++) {
                    Console.WriteLine(arr[i]);
                }
                return arr.Sum();
            };
            Console.WriteLine(add2(3,4,5)+" lambda!!");
        }

        public static void Main1(string[] args) {
            // Action 委托的非泛型版本，就是一个无参数、无返回值的委托
            Action action1 = new Action(M1);
            action1();
            // Action 的泛型版本，就是一个无返回值，但是参数可以变化的委托
            Action<string> action2 = m => { Console.WriteLine(m); };
            action2("hello,GenericWorld!");
            Action<int, int> action3 = (x, y) => { Console.WriteLine(x + y); };
            action3(100, 100);
            
            // Func 委托只有一个泛型版本的，没有非泛型版本的
            Func<int, int, int, int> fun = M2;
            Console.WriteLine(fun(1, 2, 3));

            AddDelegate2 myDelegate = M3;
            int x = 0;
            int n = myDelegate(1, 2,out x);
            Console.WriteLine(x);
            Console.WriteLine(n);
            
        }

        static int M3(int n1, int n2, out int n3) {
            n3 = 100;
            return n1 + n2 + n3;
        }

        static int M2(int n1, int n2, int n3) {
            return n1 + n2 + n3;
        }

        static void M1() {
            Console.WriteLine("Hello,World!");
        }

        public static void Main2(string[] args) {
            #region 使用 lambda 表达式

            List<int> list = new List<int>(){1,2,3,4,5,6,7,89,99,11,10,14,13,15};
            // 原 list 集合是不变的
            // （method）lEnumerable<int> System.Linq.Enumerable.Where <int>（Func <int，bool> predicate）
            // IEnumerable<int> ie = list.Where(Condition);
            IEnumerable<int> ie = list.Where(x => { return x > 6; });
            foreach (var item in ie) {
                Console.WriteLine(item);
            }

            #endregion
        }

        static bool Condition(int x) {
            return x > 6;
        }

        public static void Main3(string[] args) {
            Action<string> action = T1;
            /*
             * 多播委托：委托绑定多个方法后，其中一个方法执行发生异常后面的方法不会继续执行；
             *         委托变量的一个重要方法：GetInvcationList(); 返回一个 Delegate[] 类型，其中 Delegate 类是一个抽象类，它是所有委托的父类
             * 多播委托的委托必须是同一个类型；
             * 多播委托相当于创建了一个按照组合的顺序依次调用的新委托对象。即委托与字符串一样，拥有不可变性
             * 委托的组合一般是给事件用的，用普通的委托的时候很少用
             */
            action += T2;
            action += T3;
            action += T4;
            action += T5;

            action -= T3;

            action("Hello,World!");
        }

        public static void Main4(string[] args) {
            MyDelegate md = new MyDelegate(T1);
            md = (MyDelegate)Delegate.Combine(md, new MyDelegate(T2), new MyDelegate(T3));
            // md();
            // T1: ok
            // T2: ook
            // T3: oook

            
            Delegate[] delegates = md.GetInvocationList();
            for (int i = 0; i < delegates.Length; i++) {
                (delegates[i] as MyDelegate)();
            }
            // T1: ok
            // T2: ook
            // T3: oook

        }

        static void T1() {
            Console.WriteLine("T1: ok");
        }
        
        static void T2() {
            Console.WriteLine("T2: ook");
        }
        static void T3() {
            Console.WriteLine("T3: oook");
        }
        
        static void T1(string msg) {
            Console.WriteLine("T1: "+msg);
        }
        
        static void T2(string msg) {
            Console.WriteLine("T2: "+msg);
        }
        static void T3(string msg) {
            Console.WriteLine("T3: "+msg);
        }
        static void T4(string msg) {
            Console.WriteLine("T4: "+msg);
        }
        static void T5(string msg) {
            Console.WriteLine("T5: "+msg);
        }

    }

    public delegate int AddDelegate2(int n1, int n2, out int n3);
    
    public delegate void MyGenericDelegate<T>(T agrs);
    
    public delegate int AddDelegate1(params int[] arr);
    public delegate int AddDelegate(int n1, int n2, int n3);
    public delegate void MyDelegate1(string msg);
    public delegate void MyDelegate();
}

/*
事件语法: event ProcessWorldDelegate 
加入 event 关键字实现事件机制的好处:用来 event 事件,不可以修改事件已近注册的值;也不可以冒充进行事件通知
委托与事件的区别：
委托和事件没有可比性，因为委托是数据类型，事件是对象（可以理解为堆委托变量的封装）。下面说的是委托实例和标准 event 事件的区别：
    ·事件的内部是用委托实现的；
    ·对于事件而言：外部只能“注册事件 +=、注销事件 -=”，并且外界不可以注销其他注册者，外界不可以主动触发事件；
     但是如果使用 Delegate 则没有上述约束，因此诞生了事件这种语法。
    ·事件使用来阉割委托实例的。
*/
namespace 事件 {
    #region 使用委托来模拟事件
    //public class Program {
    //    public static void Main0(string[] args) {
    //        MusicPlayer mp3 = new MusicPlayer();
    //        mp3.AfterStartedPlay = () => {
    //            Console.WriteLine("加载歌词 ... ");
    //            Console.WriteLine("加载动态皮肤 ... ");
    //        };

    //        mp3.StartPlay();

    //        mp3.BeforeMusicStop = () => {
    //            Console.WriteLine("删除歌词缓存中 ... ");
    //            Console.WriteLine("删除动态皮肤缓存中 ... ");
    //        };
    //        mp3.EndMusic();

    //        //因为是用委托来实现的，所以在外部可以随意调用触发，不符合代码逻辑
    //        //此时，不能在委托变为 private，如果改为私有的，则在外部无法为委托变量动态赋值
    //        //因此，需要使用事件来实现
    //        //mp3.AfterStartedPlay();
    //        //mp3.BeforeMusicStop();

    //        //此外,委托还可以直接使用 = 赋值,可以将之前注册的方法都覆盖掉
    //        //mp3.AfterStartedPlay = null;
    //        //mp3.BeforeMusicStop = null;
    //    }
    //}

    ///// <summary>
    ///// 音乐播放器类
    ///// </summary>
    //public class MusicPlayer {
    //    // 模拟两个事件
    //    // 1.音乐开始播放后触发某个事件
    //    public Action AfterStartedPlay;

    //    // 2.音乐停止播放之前触发某个事件
    //    public Action BeforeMusicStop;

    //    private void PlayMusic() {
    //        Console.WriteLine("开始播放音乐 ... ");
    //    }

    //    /// <summary>
    //    /// 按下【播放】按钮实现播放音乐
    //    /// </summary>
    //    public void StartPlay() {
    //        PlayMusic();
    //        if (AfterStartedPlay != null) {
    //            AfterStartedPlay();
    //        }
    //        Thread.Sleep(2000);
    //    }

    //    /// <summary>
    //    /// 音乐播放完毕按钮
    //    /// </summary>
    //    public void EndMusic() {
    //        if (BeforeMusicStop != null) {
    //            BeforeMusicStop();
    //        }
    //        Console.WriteLine("音乐播放完毕！");
    //    }

    //}
    #endregion

    /// <summary>
    /// 使用事件来实现音乐播放器逻辑
    /// </summary>
    public class Program {
        public static void Main1(string[] args) {
            MusicPlayer mp3 = new MusicPlayer();
            mp3.AfterStartedPlay += () => {
                Console.WriteLine("加载歌词 ... ");
                Console.WriteLine("加载动态皮肤 ... ");
            };

            mp3.StartPlay();

            mp3.BeforeMusicStop += () => {
                Console.WriteLine("删除歌词缓存中 ... ");
                Console.WriteLine("删除动态皮肤缓存中 ... ");
            };
            mp3.EndMusic();

            //此时再直接调用事件会报错：事件“MusicPlayer.BeforeMusicStop”只能出现在 += 或 -= 的左边(从类型“MusicPlayer”中使用时除外)    
            //mp3.AfterStartedPlay();
            //mp3.BeforeMusicStop();
            //同理，下面这种写法也会报错
            //mp3.AfterStartedPlay = null;
            //mp3.BeforeMusicStop = null;
        }
    }

    /// <summary>
    /// 音乐播放器类
    /// </summary>
    public class MusicPlayer {
        // 模拟两个事件
        // 1.音乐开始播放后触发某个事件
        public event Action AfterStartedPlay;

        // 2.音乐停止播放之前触发某个事件
        public event Action BeforeMusicStop;

        private void PlayMusic() {
            Console.WriteLine("开始播放音乐 ... ");
        }

        /// <summary>
        /// 按下【播放】按钮实现播放音乐
        /// </summary>
        public void StartPlay() {
            PlayMusic();
            if (AfterStartedPlay != null) {
                AfterStartedPlay();
            }
            Thread.Sleep(2000);
        }

        /// <summary>
        /// 音乐播放完毕按钮
        /// </summary>
        public void EndMusic() {
            if (BeforeMusicStop != null) {
                BeforeMusicStop();
            }
            Console.WriteLine("音乐播放完毕！");
        }

    }
}

/*
事件的本质论：
event 会自动生成一个 private delegate 变量和两个函数：add 和 remove；C# 编译器会用这两个方法使事件变量仅支持 += 和 -= 两个操作符
内部实现是：（示例性）
priavte MyDelegate OnEvent;
public void Add(MyDelegate d){
    OnEvent += d;
}
public void Remove(MyDelegate d){
    OnEvent -= d;
}
因为 OnEvent 是 private 的，所以在类外部不能使用 OnEvent() 触发事件，但是在类内部可以；
public 的方法只有 Add 和 Remove，所以事件变量在外部只能 += 和 -=，其他操作符都不可以作用于事件变量
*/
namespace 事件案例 {
    public interface IInterface {
        void SayHi();

        string Name {
            get;
            set;
        }

        string this[int index] {
            get;
            set;
        }

        event Action MyEvent;
    }

    public class MyClass : IInterface {
        public string this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action MyEvent;

        public void SayHi() {
            throw new NotImplementedException();
        }
    }

}












