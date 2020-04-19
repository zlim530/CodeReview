using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CSharpSenior {
    class _005_0x01多线程之ThreadPool {
        /*
        使用Thread类已经可以创建并启动线程了，但是随着开启的线程越来越多，线程的创建和终止都需要手动操作，非常繁琐，
        另一个问题是，开启更多新的线程但是没有用的线程没有及时得到终止的时候，会占用越来越多的系统资源，影响性能。
        所以,.net 为我们引入了ThreadPool(线程池)，我们只需要把要执行的任务放到线程池中即可，线程的开启包括资源的释放都由线程池帮我们完成。
        下面看一下怎么使用线程池。

        一、线程池ThreadPool
        核心类：System.Threading.ThreadPool， 线程池受.Net CLR管理的，每一个CLR都有一个线程池实例。
        每个进程都有一个线程池，线程池的默认大小为：每个可用的处理器有 25 个线程。
        使用 SetMaxThreads 方法可以更改线程池中的线程数。每个线程使用默认的堆栈大小并按照默认的优先级运行.
        ThreadPool 类型拥有一个 QueueUserWorkItem 的静态方法。该静态方法接收一个委托，代表用户自定义的一个异步操作。
        在该方法被调用后，委托会进入到内部队列中。如果线程池中没有任何线程，将创建一个新的工作者线程（worker thread）并将队列中的第一个委托放入到该工作者线程中。
        如果向线程池中放入新的操作，当之前的所有操作完成后，很可能只需重用一个线程来执行这些新的操作。
        如果QueueUserWorkItem执行的频率过快，线程池将创建更多的线程来执行这些新放入的异步委托。
        线程池中的线程数是有限的，如果没有空闲的线程来执行这些异步委托操作，这种情况下，新的异步委托操作将在线程池的内部队列中等待，直到线程池中年的工作者线程空闲（有能力）来执行。
        当停止向线程池中放入新的异步委托操作时，线程池会删除一定事件后过期的不在使用的线程，同时释放不再使用的系统资源。

        二、不适合使用线程池的场景
        使用线程池创建线程这么简单，然后性能又好，是不是以后创建线程都去使用线程池呢？答案是否定的。

        那么，以下场景是不适合使用线程池，而是自己创建并管理线程：

        需要前台线程时。因为线程池默认创建的都是后台线程。
        需要线程具有特定的优先级。因为放到线程池中的线程都是由线程池来调度的，无法对其优先级进行设置。
        需要长时间运行的任务。由于线程池具有最大线程数限制，因此大量阻塞的线程池线程可能会阻止任务启动。
        */


        static void Main(string[] args) {
            Console.WriteLine("Main thread starting ... ");

            ThreadPool.QueueUserWorkItem(AsynAction);
            Thread.Sleep(TimeSpan.FromSeconds(3));

            ThreadPool.QueueUserWorkItem(AsynAction,"Async state");
            Thread.Sleep(TimeSpan.FromSeconds(3));

            ThreadPool.QueueUserWorkItem(item => {
                Console.WriteLine("Sub thread starting ... ");
                Console.WriteLine("Action state:{0}",item ?? string.Empty);
                Console.WriteLine(string.Format("Current thread id:{0}",Thread.CurrentThread.ManagedThreadId));
            },"Lambda state");

            Console.WriteLine("Main thread completed!");
            /*
            输出结果：
            Main thread starting ...
            Sub thread starting ...
            Action state:
            Current thread id:5
            Sub thread starting ...
            Action state:Async state
            Current thread id:4
            Main thread completed!
            Sub thread starting ...
            Action state:Lambda state
            Current thread id:4
            */
        }

        static void AsynAction(object obj) {
            Console.WriteLine("Sub thread starting ... ");
            Console.WriteLine("Action state:{0}",obj ?? string.Empty);
            Console.WriteLine(string.Format("Current thread id:{0}",Thread.CurrentThread.ManagedThreadId));
        
        }

    }
}
