using System;
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

            System.Console.WriteLine(Thread.CurrentThread.Name);
            // 同时在主线程也做一些工作
            for (int i = 0; i < 1000; i++)
            {
                System.Console.Write("x");
            }
        }

        static void WriteY()
        {
            System.Console.WriteLine(Thread.CurrentThread.Name);
            for (int i = 0; i < 1000; i++)
            {
                System.Console.Write("y");
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
            System.Console.WriteLine("Thread t has ended!");
        }

        static void Go()
        {
            for (int i = 0; i < 1000; i++)
            {
                System.Console.Write("y");
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
            System.Console.WriteLine("\nCurrent thread:{0}",Thread.CurrentThread.Name);
            if (Thread.CurrentThread.Name == "Thread1" && thread2.ThreadState != ThreadState.Unstarted)
            {
                if (thread2.Join(2000))
                {
                    System.Console.WriteLine("Thread2 has termminated.");
                }else
                {
                    System.Console.WriteLine("The timeout has elapsed and Thread1 will resume.");
                }
            }
            Thread.Sleep(2000);
            System.Console.WriteLine("\nCurrent thread:{0}",Thread.CurrentThread.Name);
            System.Console.WriteLine("Thread1:{0}",thread1.ThreadState);
            System.Console.WriteLine("Thread2:{0}\n",thread2.ThreadState);
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
                System.Console.WriteLine("New Thread terminated.");
            }
            else
            {
                System.Console.WriteLine("Join timed out.");
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
            System.Console.WriteLine($"{Convert.ToString((int)state,2)}");
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