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
        static void Main(string[] agrs) {

            Console.WriteLine("Main thread starting ... ");
            var cts = new CancellationTokenSource();
            var task1 = Task.Factory.StartNew(() => {
                TaskAction(cts.Token);
            });

            Thread.Sleep(3000);
            Console.WriteLine(string.Format("current task status :{0}",task1.Status));

            cts.Cancel();
            Console.WriteLine("Start cancel task!");
            for (int i = 0; i < 5; i++) {
                Thread.Sleep(500);
                Console.WriteLine(string.Format("current task status :{0}",task1.Status));
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



    }
}
