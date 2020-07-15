using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading;

namespace CSharpSenior {
    class _005_多线程 {
        /*
        程序最小执行单元是线程，最小资源分配单位是进程。进程里必然至少有一个线程，而一个程序也必然至少有一个进程。
        对于单核 CPU 而言，多线程和多进程一样，都不会产生并行的效果；对于多核 CPU 而言，多进程必然是并行的，但是多线程则不一定是
        并行。所以 C#中，线程更多的用作异步处理上，而不是并行计算上。
            所谓并行就是多任务同时执行，这里的任务指的是程序需要完成的事，而不是 C#中的任务机制
        前台线程和后台线程的区别
        1.前台线程和后台线程的区别在于，应用程序必须运行完所有的前台线程才可以退出，而对于后台线程，可以不考虑其是否运行完而直接退出并且不会抛出异常，所有的后台线程在应用程序退出时就自动结束了。

        2.默认情况下，主线程和使用Thread创建的线程都是前台线程(使用线程池和Task创建的线程默认都是后台线程)，除非手动设置IsBackground= true。
        多线程和异步的区别
        1.多线程和异步在很多时候被认为是同一个东西，都是为了让主线程不需要等待而继续执行。
        2.但是从辩证关系上来看，两者还是有区别的，可以用一句话来概括。
        3.异步是目的，多线程是实现异步的其中的一种方式(比如还可以通过创建另一个进程实现异步)。
            异步通俗的将就是不暂停也不等待当前耗时的流程执行完成，继续执行后续的流程。

        线程是低级别的抽象，线程池虽然高级一点，但同样很低，而现在C#给我们提供了很多高级的并发编程技术工具，
        所以原则上我们不建议直接操作Thread对象。但是为了让大家很好的理解C#多线程的来龙去脉，这里介绍C#最初操作多线程的方法。

        线程的状态，一般情况下线程分为五个阶段，也就是五种状态，分别为：准备、就绪、运行、阻塞、死亡。
        各状态之间的切换如下：
            线程 ---> 准备/创建 ---> 就绪 ---> 运行 ---> 销毁 ---> 结束
                                    |         |         |
                                    |         |--->阻塞 --
                                    --------------------|   
        线程状态之间的切换顺序有着严格的限制，而且只能从就绪状态由 CPU 切换为运行状态，无法从其他状态切换为运行状态，并且这一步的切换
        开发者不能控制。
        */

        static void Main0(string[] args) {
            Thread thread = new Thread(new ThreadStart(DoWork));
            // public delegate void ThreadStart();
            
            thread.Start();// 线程此时为就绪状态
            // 注意：线程不能直接进入运行态，该状态只能由CPU决定

            Thread.Sleep(10);
            thread.Abort();

            Thread parameterizedThread = new Thread(new ParameterizedThreadStart(DoWorkWithParam));
            // public delegate void ParameterizedThreadStart(object obj);

            parameterizedThread.Start();// 等于 parameterizedThread.Start(null); 

        }

        static void Main2(string[] args) {
            /*
            public Thread(ThreadStart start) {
            }
            */
            var thread = new Thread(PrintNumbers);
            thread.Start();
            PrintNumbers();//可以看出，C#的多线程执行顺序是不确定的。PrintNumbers 方法同时被工作线程和主线程调用，可以看到工作线程和主线程是同步运行的，互不干扰

            // Thread 的声明还有其他方式，可以使用Lambda风格来定义。方法如下：
            var workThread = new Thread(() => {
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine($"The number is {i}");
                }
            });
        }

        /// <summary>
        /// 匹配委托的方法：
        /// namespace System.Threading {
        //     //Represents the method that executes on a System.Threading.Thread.
        //    public delegate void ThreadStart();
        //  }
        /// </summary>
        public static void PrintNumbers() {
            Console.WriteLine("Starting PrintNumbers ... ... ");
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(i);
            }
        }



        // 假如需要暂停当前线程，可以调用Thread.Sleep方法，使当前线程处于阻塞状态
        // 暂停一个线程
        // 暂停一个线程是让一个线程等待一段时间而不消耗操作系统资源
        static void Main3(string[] args) {
            var threadWithDelay = new Thread(PrintNumbersWithDelay);
            threadWithDelay.Start();
            PrintNumbersWithDelay();
        }

        public static void PrintNumbersWithDelay() {
            Console.WriteLine("Starting with Delay ... ... ");
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(string.Format("{0} {1}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),i));
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));// 使当前线程阻塞1s
                // Thread.Sleep 方法被调用后，线程处于休眠状态，会尽可能的少占用系统资源，起到了暂停线程的效果
            }
        }

        // 如果需要等待某个子线程执行行，主线程才继续执行时，可以使用Thread.Join方法来实现
        // 线程等待
        // 线程等待是指多线程编程中，一个线程等待另一个线程完成后再执行。
        static void Main4(string[] args) {
            var thread = new Thread(PrintNumberWithDelay2);
            thread.Start();
            thread.Join();// 合并线程，即等待子线程执行完成，主线程才继续执行
            // 我们使用了 Join 方法，该方法允许我们等待知道此线程完成，当线程完成后，下面的代码才可以执行
            PrintNumberWithDelay2();

        }

        public static void PrintNumberWithDelay2() {
            Console.WriteLine("Starting with Delay and Join ... ... ");
            for (int i = 0; i < 10; i++) {
                Console.WriteLine(string.Format("当前时间：{0}，线程状态：{1}，结果：{2}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),Thread.CurrentThread.ThreadState,i));
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));// 线程阻塞1s，此时线程状态为 WaitSleepJoin
            }
        
        }

        //结束线程
        //结束线程是指在某个线程运行中间停止该线程
        static void Main5(string[] agrs) {

            var thread = new Thread(PrintNumberWithDelay2);
            thread.Start();

            Thread.Sleep(TimeSpan.FromMilliseconds(4000));
            thread.Abort();

            Console.WriteLine("Thread has been Abort!");// 抛出异常：System.PlatformNotSupportedException:“Thread abort is not supported on this Platform”
            // 可以看出.net core 对 Thread 的支持不够，是因为 Thread 已经完全落伍了，同样在Windows我们也不建议这么做
        }

        // 检测线程状态
        // 一个线程可以用 ThreadState 枚举来表示，
        // 下面是ThreadState的源码：
        public enum ThreadState { 
            Running = 0,
            StopRequested = 1,
            SuspendRequested = 2,
            Background = 4,
            Unstarted = 8,
            Stopped = 16,           // 0x0000_0010
            WaitSleepJoin = 32,     // 0x0000_0020
            Suspended = 64,         // 0x0000_0040
            AbortRequested = 128,   // 0x0000_0080
            Aborted = 256,          // 0x0000_0100
        }

        static void Main6(string[] args) {
            var workThread = new Thread(NumberCountCoudDelay);
            Console.WriteLine($"work thread state: {workThread.ThreadState}");
            workThread.Start();
            workThread.Join();
            Console.WriteLine($"work thread state: {workThread.ThreadState}");
            Console.WriteLine("Work thread is stopped!!!");
        }

        static void NumberCountCoudDelay() {
            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"Delay thread number is {i}");
                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }


        /*
        线程传递参数
        通过分析可以发现，Thread接受的实际上是一个委托，包括无参数的委托和接受一个Object类型的委托:
            public delegate void ThreadStart();
            public delegate void ParameterizedThreadStart(object obj);
        */
        static void Main(string[] args) {
            Console.WriteLine("Main thread starting ... ");
            var thread = new Thread(PrintNumberWithCount);
            thread.Start(5);
            thread.Join();
            Console.WriteLine("Main thread completed!");
        }

        /// <summary>
        /// 匹配委托的方法，带参数
        /// </summary>
        public static void PrintNumberWithCount(object obj) {
            Console.WriteLine("Sub thread starting ... ");
            var number = Convert.ToInt32(obj);
            for (int i = 0; i < number; i++) {
                Console.WriteLine(string.Format("当前时间：{0}，线程状态：{1}，结果：{2}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),Thread.CurrentThread.ThreadState,i));
            }

        }

        private static void DoWork() {
            try {
                for (int i = 0; i < 10; i++) {
                    Console.WriteLine("Work Thread:",i.ToString());
                }
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                Thread.ResetAbort();
            }

            Console.WriteLine("Work Thread:still alive and working.");
            Thread.Sleep(500);
            Console.WriteLine("Work Thread:finished working.");
        
        }

        public static void DoWorkWithParam(object obj) {
            string msg = (string)obj;
            Console.WriteLine("Parameterized Work Thread:"+msg);
        }










        #region 反射相关
        /// <summary>
        /// 获取程序集中的所有公共类型
        /// </summary>
        /// <param name="assembly"></param>
        static void GetExportedTypes(Assembly assembly) {
            var types = assembly.GetExportedTypes();

            foreach (var item in types) {
                Console.WriteLine(item.Name);
            }
        }

        static void Main1(string[] args) {
            var assembly = Assembly.Load("ReflectionDemo.A,Version=1.0.0.0,Culture=neutral,PublicKeyToken=null");

            var assembly2 = Assembly.LoadFrom(@"http://www.a.com/ReflectionDemo.A.dll");

            var path = string.Format(@"{0}\{1}",AppDomain.CurrentDomain.BaseDirectory,@"plugins\ReflectionDemo.A.dll");
            var assembly3 = Assembly.LoadFile(path);

        }

        #endregion
    }
}
