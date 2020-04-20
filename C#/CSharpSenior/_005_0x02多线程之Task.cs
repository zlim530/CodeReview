using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpSenior {
    /*
    使用线程池使得创建线程已经很简单了，但是使用线程池不支持线程的取消，完成和失败通知等交互操作，为了解决这些问题
    .net 4.0带来了TPL(Task Parallel Library)任务并行库，下面就来总结下Task的使用。
    */
    class _005_0x02多线程之Task {

        static void Main0(string[] args) {
            //在.net 4.0下使用task创建一个线程非常简单，有两种方式，如下代码
            //需要注意的是：task也是基于线程池的，所以这两个任务的执行顺序是不固定的
            var task1 = new Task(() => {
                Console.WriteLine("Create and start task!");
            });
            task1.Start();

            Task.Factory.StartNew(() => {
                Console.WriteLine("Task Factory start new task!");
            });
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
