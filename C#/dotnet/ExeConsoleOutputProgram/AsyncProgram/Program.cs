using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncProgram
{
    class Program0
    {
        static void Main0(string[] args)
        {
            Thread.CurrentThread.Name = "Main Thread ... ";
            // 开启了一个新的线程 Thread
            Thread t = new Thread(WriteY);
            t.Name = "Y Thread ... ";
            // 运行 WriteY()
            t.Start();

            Console.WriteLine(Thread.CurrentThread.Name);
            // 同时在主线程也做一些工作
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("x");
            }
        }

        static void WriteY()
        {
            Console.WriteLine(Thread.CurrentThread.Name);
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("y");
            }
        }
    }

    class Program
    {
        static void Main1()
        {
            Thread t = new Thread(Go);
            t.Start();
            t.Join();// 在主线程 Main 中使用 子线程.Join() 则主线程会等待子线程执行完毕再执行下面的语句
            Console.WriteLine("Thread t has ended!");
        }

        static void Go()
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.Write("y");
            }
        }
        
    }

    class Program1
    {
        static Thread thread1,thread2;
        
        public static void Main2()
        {
            thread1 = new Thread(ThreadProc);
            thread1.Name = "Thread1";
            thread1.Start();

            thread2 = new Thread(ThreadProc);
            thread2.Name = "Thread2";
            thread2.Start();
        }

        public static void ThreadProc()
        {
            Console.WriteLine("\nCurrent thread:{0}",Thread.CurrentThread.Name);
            if (Thread.CurrentThread.Name == "Thread1" && thread2.ThreadState != ThreadState.Unstarted)
            {
                if (thread2.Join(2000))
                {
                    Console.WriteLine("Thread2 has termminated.");
                }else
                {
                    Console.WriteLine("The timeout has elapsed and Thread1 will resume.");
                }
            }
            Thread.Sleep(2000);
            Console.WriteLine("\nCurrent thread:{0}",Thread.CurrentThread.Name);
            Console.WriteLine("Thread1:{0}",thread1.ThreadState);
            Console.WriteLine("Thread2:{0}\n",thread2.ThreadState);
        }
    }

    class Program2
    {
        // TimeSpan.TimeSpan(int hours,int minutes,int seconds);
        static TimeSpan waitTime = new TimeSpan(0,0,1);

        public static void Main3()
        {
            Thread newThread = new Thread(Work);
            newThread.Start();

            if (newThread.Join(waitTime + waitTime))
            {
                Console.WriteLine("New Thread terminated.");
            }
            else
            {
                Console.WriteLine("Join timed out.");
            }
        }

        static void Work()
        {
            Thread.Sleep(waitTime);
        }

        static void Main4()
        {
            // 也可以使用 TimeSpan 实现相同的效果
            TimeSpan interval = new TimeSpan(0,0,2);
            
            for(int i = 0;i < 5;i++)
            {
                Console.WriteLine("Sleep for 2 seconds.");
                // 程序执行结果就是：每隔2秒输出一次 Sleep for 2 seconds. 这句话
                //Thread.Sleep(2000);
                Thread.Sleep(interval);
            }
            
            Console.WriteLine("Main thread exits.");
        }
        
    }

    class Program3
    {
        static void Main5()
        {
            var state = ThreadState.Unstarted | ThreadState.Stopped | ThreadState.WaitSleepJoin;
            Console.WriteLine($"{Convert.ToString((int)state,2)}");
        }
    
        static void Main6(){
            new Thread(Go).Start();// 在新下线程上调用 Go()

            Go();// 在 main 线程上调用 Go()
        }

        static void Go()
        {
            // cycles 是本地变量
            // 在每个线程的内存栈上，都会创建 cycles 独立的副本
            for (int cycles = 0; cycles < 5; cycles++)
            {
                Console.WriteLine('?');
            }
        }

        // 结果会输出 10 个 ？


        static void Main7()
        {
            bool done = false;

            ThreadStart action = () => 
            {
                if (!done)
                {
                    done = true;
                    Console.WriteLine("Done3");

                    // 只要将代码改为下列代码，打印两次 Done3 的几率就会大大增加（就会打印两次 Done3 ）
                    // Console.WriteLine("Done3");
                    // Thread.Sleep(100);
                    // done = true;
                }
            };

            new Thread(action).Start();
            action();
        }

    }


    class Program4
    {
        // 前台线程 vs 后台线程
        static void Main0(string[] args)
        {
            Thread worker = new Thread(() => Console.ReadLine());
            if (args.Length > 0)
            {
                worker.IsBackground = true;
                Console.WriteLine(worker.IsBackground);
            }
            worker.Start();
        }


        // 信号简介
        static void Main1()
        {
            var signal = new ManualResetEvent(false);

            new Thread(() => {
                Console.WriteLine("Waiting for signal ... ");
                signal.WaitOne();
                signal.Dispose();
                System.Console.WriteLine("Got signal!");
            }).Start();

            Thread.Sleep(3000);
            signal.Set();// 主线程打开了信号，也即释放了信号
        }


        // Task
        static void Main2()
        {
            Task<int> primeNumberTask = Task.Run(() => Enumerable.Range(2,3000000).Count(n => 
                                        Enumerable.Range(2,(int)Math.Sqrt(n) - 1).All(i => n % i > 0)));

            /*Console.WriteLine("Task running ... ");
            Console.WriteLine($"The anwer is {primeNumberTask.Result}");*/

            /*
            在 task 上调用 GetAwaiter 会返回一个 awaiter 对象
                它的 OnCompleted 方法会告诉之前的 task：“当你结束/发生故障的时候要执行委托”
            可以将 Continuation 附加到已经结束的 task 上面，此时 Continuation 将会被安排立即执行

            如果发生故障：
              如果之前的任务发生故障，那么当 Continuation 代码调用 awaiter.GetResult() 的时候，异常会被重新抛出
              无需调用 GetResult ，我们可以直接方法 task 的 Result 属性
              但调用 GetResult 的好处时：如果 task 发生故障，那么异常会被直接的抛出，而不是包裹在 AggregateException 里面，这样 catch 块
              就简洁很多了
            */
            /*var awaiter = primeNumberTask.GetAwaiter();
            awaiter.OnCompleted( () => {
                int result = awaiter.GetResult();
                Console.WriteLine(result);// Writes result
            });*/

            var awaiter = primeNumberTask.ConfigureAwait(false).GetAwaiter();
            awaiter.OnCompleted(() => {
                int result = awaiter.GetResult();
                Console.WriteLine(result);// Writes result
            });

            Console.ReadLine();
        }


        // TaskCompletionSource
        public static void Main3(string[] args) {
            var tcs = new TaskCompletionSource<int>();

            new Thread(() => {
                Thread.Sleep(5000);
                tcs.SetResult(42);
            }) {
                IsBackground = true
            }.Start();

            Task<int> task = tcs.Task;
            Console.WriteLine(task.Result);
        }


        public static void Main4(string[] args) {
            /*Task<int> task = Run(() => {
                Thread.Sleep(5000);
                return 42;
            });*/
            //Console.WriteLine(task.Result);

            // TaskCompletionSource 的真正魔力：它创建 Task，但并不占用线程
            Delay(5000).GetAwaiter().OnCompleted(() => Console.WriteLine(42));
        }

        // 注意：没有非泛型版本的 TaskCompletionSource
        static Task Delay(int milliseconds) {
            var tcs = new TaskCompletionSource<Object>();
            var timer = new System.Timers.Timer(milliseconds) { AutoReset = false};
            timer.Elapsed += delegate { timer.Dispose(); tcs.SetResult(null); };
            timer.Start();
            return tcs.Task;
        }

        

        // 调用此方法相当于调用 Task.Factory.StartNew
        // 并使用 TaskCreationOptions.LongRunning 选项来创建非线程池的线程
        static Task<TResult> Run<TResult>(Func<TResult> function) {
            var tcs = new TaskCompletionSource<TResult>();
            new Thread(() => {
                try {
                    tcs.SetResult(function());
                } catch (Exception ex) {
                    tcs.SetException(ex);
                }
            }).Start();
            return tcs.Task;
        }

    }


    class ThreadTest
    {
        bool _done;

        static void Main0()
        {
            ThreadTest tt = new ThreadTest(); // 创建了一个共同的实例
            new Thread(tt.Go).Start();
            tt.Go();
        }

        void Go() // 这是一个实例方法
        {
            if (!_done)
            {
                _done = true;
                Console.WriteLine("Done");
            }
        }

        // 由于两个线程是在同一个 ThreadTest 实例上调用的 Go()，所以它们共享 _done
        // 结果就是只打印一次 Done

    }

    class ThreadTest2
    {
        static bool _done; // 静态字段在同一应用域下的所有线程中被共享

        static void Main0()
        {
            new Thread(Go).Start();
        }

        static void Go() // 这是一个实例方法
        {
            if (!_done)
            {
                _done = true;
                Console.WriteLine("Done");
            }
        }

        // 由于静态字段在同一应用域下的所有线程中被共享
        // 结果就是只打印一次 Done

    }


    class ThreadSafe
    {
        static bool _done;

        static readonly object _locker = new object();

        static void Main0()
        {
            new Thread(Go).Start();
            Go();
        }

        static void Go()
        {
            lock(_locker)
            {
                if (!_done)
                {
                    Console.WriteLine("Done with the lock block.");
                    _done = true;
                }
            }
        }
    }
}




namespace 源栈.NET培训异步与并行
{
    public class Program
    {
        public static void Main0()
        {
            Thread worker = new Thread(Process);
            worker.Start();

            Thread current = Thread.CurrentThread;// 获取当前正在运行的线程

            // 然后，可以获取线程的相关信息：
            Console.WriteLine(Thread.GetDomain().FriendlyName);// 返回当前线程正在其中运行的当前应用程序域的友好名称
            Console.WriteLine(current.ManagedThreadId);        //托管线程Id：获取当前托管线程的唯一标识符
            Console.WriteLine(current.Priority);               //优先级：获取或设置指示线程的调度优先级的值
            /* 
            public enum ThreadPriority
            {
                Lowest = 0,
                
                BelowNormal = 1,
                
                Normal = 2,
                
                AboveNormal = 3,
                
                Highest = 4
            }
             */
            Console.WriteLine(current.ThreadState);            //线程状态：获取一个值，该值包含当前线程的状态
            Console.WriteLine(current.IsThreadPoolThread);     //是否线程池线程：获取指示线程是否属于托管线程池的值
            /* 
            In worker thread:ThreadId:4
            AsyncProgram
            1
            Normal
            Running
            False
             */
            
        }

        public static void Process()
        {
            System.Console.WriteLine($"In worker thread:ThreadId:{Thread.CurrentThread.ManagedThreadId}");
        }
    }


    public class Program1
    {
        static void Main1()
        {
            for (int i = 0; i < 10; i++)
            {
                // Console.WriteLine($"{i}：ThreadId-{Thread.CurrentThread.ManagedThreadId}");
                /* 
                输出结果：
                0：ThreadId-1
                1：ThreadId-1
                2：ThreadId-1
                3：ThreadId-1
                4：ThreadId-1
                5：ThreadId-1
                6：ThreadId-1
                7：ThreadId-1
                8：ThreadId-1
                9：ThreadId-1
                 */

                // new 出来的 Thread 需要调用Start()来启动。
                new Thread(() => {
                    System.Console.WriteLine($"in for loop thread:{i}:ThreadId-{Thread.CurrentThread.ManagedThreadId}");
                }).Start();
                // 多线程会带来异步和并发的效果：
                /* 
                输出结果：
                in for loop thread:8:ThreadId-12
                in for loop thread:6:ThreadId-5
                in for loop thread:6:ThreadId-8
                in for loop thread:6:ThreadId-7
                in for loop thread:6:ThreadId-10
                in for loop thread:6:ThreadId-4
                in for loop thread:6:ThreadId-6
                in for loop thread:8:ThreadId-11
                in for loop thread:6:ThreadId-9
                in for loop thread:10:ThreadId-13
                 */
            }
        }
    }


    public class Program2
    {
        static void Main2()
        {
            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread( () => {
                    Thread.Sleep(1000);
                    System.Console.WriteLine($"{i}：ThreadId-{Thread.CurrentThread.ManagedThreadId}");
                });
                // 将线程设置为后台线程
                thread.IsBackground = true;
                thread.Start();
                System.Console.WriteLine("main thread, i = " + i);
            }
            /* 
            输出结果：
            main thread, i = 0
            main thread, i = 1
            main thread, i = 2
            main thread, i = 3
            main thread, i = 4
            main thread, i = 5
            main thread, i = 6
            main thread, i = 7
            main thread, i = 8
            main thread, i = 9
            控制台可以显示所有前台线程的输出，后台线程中的控制台输出无法呈现
             */
        }
    }


    public class Program3
    {
        public static void Main3()
        {
            Action getUp = () => {
                // int? Task.CurrentId{ get; } 返回当前正在执行 Task 的ID
                System.Console.WriteLine($"Get up!!! Task : {Task.CurrentId}" + $"  ThreadId : {Thread.CurrentThread.ManagedThreadId}");
            };
            // getUp();
            // Get up!!! Task :   ThreadId : 1
            

            for (int i = 0; i < 10; i++)
            {
                System.Console.WriteLine($"第{i+1}次：");

                // getUp();
                /* 
                第1次：
                Get up!!! Task :   ThreadId : 1
                第2次：
                Get up!!! Task :   ThreadId : 1
                第3次：
                Get up!!! Task :   ThreadId : 1
                第4次：
                Get up!!! Task :   ThreadId : 1
                第5次：
                Get up!!! Task :   ThreadId : 1
                第6次：
                Get up!!! Task :   ThreadId : 1
                第7次：
                Get up!!! Task :   ThreadId : 1
                第8次：
                Get up!!! Task :   ThreadId : 1
                第9次：
                Get up!!! Task :   ThreadId : 1
                第10次：
                Get up!!! Task :   ThreadId : 1
                 */


                Task t = new Task(getUp);
                t.Start();
                /* 
                第1次：
                第2次：
                第3次：
                第4次：
                第5次：
                第6次：
                第7次：
                第8次：
                第9次：
                第10次：
                Get up!!! Task : 3  ThreadId : 6
                Get up!!! Task : 2  ThreadId : 4
                Get up!!! Task : 1  ThreadId : 5
                Get up!!! Task : 4  ThreadId : 7
                Get up!!! Task : 8  ThreadId : 7
                Get up!!! Task : 6  ThreadId : 4
                Get up!!! Task : 7  ThreadId : 5
                Get up!!! Task : 5  ThreadId : 6
                Get up!!! Task : 9  ThreadId : 7
                 */


                // System.Console.WriteLine($"Task t:{t.Id}, ThreadId : {Thread.CurrentThread.ManagedThreadId}");
                /* 
                第1次：
                Task t:1, ThreadId : 1
                第2次：
                Task t:2, ThreadId : 1
                第3次：
                Task t:3, ThreadId : 1
                Get up!!! Task : 2  ThreadId : 5
                Get up!!! Task : 3  ThreadId : 6
                Get up!!! Task : 1  ThreadId : 4
                第4次：
                Task t:4, ThreadId : 1
                第5次：
                Get up!!! Task : 4  ThreadId : 4
                Task t:5, ThreadId : 1
                第6次：
                Task t:6, ThreadId : 1
                第7次：
                Get up!!! Task : 6  ThreadId : 4
                Get up!!! Task : 5  ThreadId : 6
                Task t:7, ThreadId : 1
                第8次：
                Task t:8, ThreadId : 1
                Get up!!! Task : 7  ThreadId : 5
                Get up!!! Task : 8  ThreadId : 6
                第9次：
                Task t:9, ThreadId : 1
                第10次：
                Task t:10, ThreadId : 1
                Get up!!! Task : 9  ThreadId : 6
                Get up!!! Task : 10  ThreadId : 5
                 */

            }


        }
    }

}