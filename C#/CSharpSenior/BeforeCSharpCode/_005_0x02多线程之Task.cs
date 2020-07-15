using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpSenior {
    /*
    C#中的任务与线程的区别不是很大，因为 C#的任务就是基于线程实现的，而任务比线程更友好，不过对于开发者而言，任务取消了线程的状态切换，
    只保留了有限的一部分。而且，在 C#中更推荐使用惹任务，任务也是对线程的进一步抽象和改进。

    使用线程池使得创建线程已经很简单了，但是使用线程池不支持线程的取消，完成和失败通知等交互操作，为了解决这些问题
    .net 4.0带来了TPL(Task Parallel Library)任务并行库，下面就来总结下Task的使用。

    C#中任务基于线程，对其做了更多的抽象和封装，将线程的粒度进一步细分。所以线程在C#中就没有那么重要了，任务逐渐替代了线程在C#程序中的地位。
    任务与线程，有共通的地方，也有完全不一样的地方。线程的运行环境相对封闭，所以线程出现错误导致线程中断，不会影响主线程的运行。
    但任务则不一样了，任务与主线程的关联性更大，一旦任务出现异常导致任务中断，如果没有正确处理，则会影响主线程的运行。
    */
    class _005_0x02多线程之Task {

        /*
        调用任务，可以期待任务时又返回值的。
        任务的命名空间 System.Threading.Tasks，任务类有以下两种声明：
            public class Task : IAsyncResult,IDisposable;
            public class Task<TResult> : System.Threading.Tasks.Task;
        第一个，没有泛型的 Task 类表示一个没有返回值的任务；
        第二个，泛型的 Task 类表示该任务有一个返回值，返回值的类型为传递进来的泛型参数。
        */

        static void Main0(string[] args) {
            //在.net 4.0下使用task创建一个线程非常简单，有两种方式，如下代码
            //需要注意的是：task也是基于线程池的，所以这两个任务的执行顺序是不固定的
            // 任务的创建有一下方法：
            // 1.通过构造函数创建：
            var task1 = new Task(() => {
                Console.WriteLine("Create and start task!");
            });
            /*
            var task2 = new Task<int>(() => {
                int i = 0;
                return i;
            });
            */
            task1.Start();
            /*
            执行任务：
                实际上通过构造方法初始化的任务并不会自动执行，而通过Task.Run和Task.Factory.StartNew 创建的任务则会自动执行。
                通过构造方法构件的任务需要手动调用Start方法。
            注意点：
                1.任务的运行不会阻塞主线程；
                2.主线程结束后，任务也一定会结束。

            任务完成：
            任务可以 IsCompleted 属性确定任务是否执行完成，所以可以通过访问任务对象的 IsCompleted 确认该任务是否执行完成，
            但有一个问题，这个属性只会表示当前任务是否完成。所以如果需要等待任务完成，则可以通过访问 Wait() 方法，强制主线程等待任务结束。

            如果使用的任务是泛型 Task 也就是有返回值的任务，可以通过访问 Result 属性获取任务执行结果。有意思的地方就是，这个属性能获取到结果的时候，
            也是任务执行完成的时候，所以不需要调用 Wait() 或 IsCompleted 来判断任务是否完成。
            */
            
            // 2.使用任务工厂：
            Task.Factory.StartNew(() => {
                Console.WriteLine("Task Factory start new task!");
            });
            /*
            var task2 = Task.Factory.StartNew(() => {
                int i = 0;
                return i;
            });
            */

            // 3.使用 Task.Run()创建：
            /*
            var task1 = Task.Run(() => {});
            var task2 = Task.Run(() => {
                int i = 0;
                return i;
            });

            */
            Console.ReadKey();

            // 输出结果：
            //Task Factory start new task!
            //Create and start task!
        }


        // 取消任务
        // 创建一个新的任务之后，我们随时都可以取消它，取消方法如下代码
        static void Main1(string[] agrs) {

            Console.WriteLine("Main thread starting ... ");
            var cts = new CancellationTokenSource();
            var task1 = Task.Factory.StartNew(() => {
                TaskAction(cts.Token);
            });

            Thread.Sleep(3000);
            Console.WriteLine(string.Format("current task status :{0}", task1.Status));

            cts.Cancel();// 取消 Task（任务）
            Console.WriteLine("Start cancel task!");
            for (int i = 0; i < 5; i++) {
                Thread.Sleep(500);
                Console.WriteLine(string.Format("current task status :{0}", task1.Status));
            }

            Console.WriteLine("Main thread completed!");
            Console.ReadKey();
            /*
            输出结果：
            Main thread starting ...
            Sub thread starting ...
            Sub thread is running!
            current task status :Running
            Start cancel task!
            current task status :Running
            Sub thread be cancelled!
            current task status :RanToCompletion
            current task status :RanToCompletion
            current task status :RanToCompletion
            current task status :RanToCompletion
            Main thread completed!
            */
        }


        public static void TaskAction(CancellationToken token) {
            Console.WriteLine("Sub thread starting ... ");
            while (true) {
                Thread.Sleep(1000);
                if (token.IsCancellationRequested) {
                    Console.WriteLine("Sub thread be cancelled!");
                    return;
                }
                Console.WriteLine("Sub thread is running!");
            }
        }


        // 创建任务集合并输出结果
        static void Main2(string[] args) {

            var tasks = new List<Task<string>>();

            var task1 = Task.Factory.StartNew<string>(() => {
                Console.WriteLine("Task1 running on thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                return "Task1";
            });

            tasks.Add(task1);

            var task2 = Task.Factory.StartNew<string>(() => {
                Console.WriteLine("Task2 running on thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                return "Task2";
            });

            tasks.Add(task2);

            var task3 = Task.Factory.StartNew<string>(() => {
                Console.WriteLine("Task3 running on thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                return "Task3";
            });

            tasks.Add(task3);

            foreach (var item in tasks) {
                Console.WriteLine(item.Result);// 调用 Task 的 Result 方法相当于调用了 Task.WaitAll(tasks.ToArray());
            }
            Console.ReadKey();
            /*
            输出结果
            Task2 running on thread id:5
            Task3 running on thread id:6
            Task1 running on thread id:4
            Task1
            Task2
            Task3
            这里需要注意2点：
            1. 每个任务都会开启一个新的线程，并且运行顺序不固定
            2. Task.Result 相当于调用了 Wait 方法，等待异步任务完成
            */
        }


        // 多任务的串行化
        static void Main3(string[] args) {

            var task1 = Task.Factory.StartNew(() => {
                Console.WriteLine("Start Task1 ... ");
                Console.WriteLine("Current thread id:{0}", Thread.CurrentThread.ManagedThreadId);
            });

            var task2 = task1.ContinueWith((item) => {
                Console.WriteLine("Start Task2 ... ");
                Console.WriteLine("Current thread id:{0}", Thread.CurrentThread.ManagedThreadId);
            });

            var task3 = task2.ContinueWith((item) => {
                Console.WriteLine("Start Task3 ... ");
                Console.WriteLine("Current thread id:{0}", Thread.CurrentThread.ManagedThreadId);
            });

            Console.ReadKey();
            /*
            输出结果：
            Start Task1 ...
            Current thread id:4
            Start Task2 ...
            Current thread id:5
            Start Task3 ...
            Current thread id:5
            注意：
                多任务串行化后，就相当于顺序执行了，而且有可能使用的是同一个线程，从上述的输出结果中的 thread id 就可以看出
            
            */
        }


        // 多任务等待执行完成
        static void Main4(string[] args) {

            var tasks = new List<Task<string>>();

            var task1 = Task.Factory.StartNew<string>(() => {
                Console.WriteLine("Task1 running on thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                return "Task1";
            });

            tasks.Add(task1);

            var task2 = Task.Factory.StartNew<string>(() => {
                Console.WriteLine("Task2 running on thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                return "Task2";
            });

            tasks.Add(task2);

            var task3 = Task.Factory.StartNew<string>(() => {
                Console.WriteLine("Task3 running on thread id:{0}", Thread.CurrentThread.ManagedThreadId);
                return "Task3";
            });

            tasks.Add(task3);

            /*
            public static void WaitAll(params Task[] tasks) {
                
            }
            等待所有任务（线程）完成，这里是卡主线程，一直等待所有子线程完成任务，才能继续往下执行
            */
            Task.WaitAll(tasks.ToArray());

            // 等价于下面的调用
            //foreach (var item in tasks) {
            //    Console.WriteLine(item.Result);// 调用 Task 的 Result 方法相当于调用了 Task.WaitAll(tasks.ToArray());
            //}
            Console.ReadKey();
            /*
            输出结果：
            Task3 running on thread id:6
            Task2 running on thread id:5
            Task1 running on thread id:4
            需要注意的是，如果是有返回值的 task，可以使用 Task.Result 获取返回值的同时，也在等待 Task 执行完成，相当于调用了 Task.Wait 方法
            */
        }


        // 创建子任务
        static void Main(string[] args) {

            var parentTask = Task.Factory.StartNew(() => {
                Console.WriteLine("Parent Task!");
                var childTask = Task.Factory.StartNew(() => {
                    Console.WriteLine("Child Task!");
                }, TaskCreationOptions.AttachedToParent);
            });

            Console.ReadKey();
            /*
            输出结果：
            Parent Task!
            Child Task!
            */
        }


    }
}
