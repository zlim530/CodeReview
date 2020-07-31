using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/**
 * @author zlim
 * @create 2020/7/31 22:22:24
 */
namespace AsyncProgram {
    /// <summary>
    /// 异步方法：async 与 await
    /// </summary>
    public class AsyncAndAwait {
        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            Console.WriteLine($"before async-1 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"before async-2 with thread {Thread.CurrentThread.ManagedThreadId}");

            GetUp();
            //GetUpWait();
            Console.WriteLine();
            Console.WriteLine("in Main() after invoking GetUp()");
            for (int i = 0; i < 10; i++) {
                // GetUp() 里 await部分的运行，会打乱这里代码的同步运行，也即 GetUp() 里 await部分中的代码会与 for 循环中的代码异步运行
                Console.WriteLine($"after async-{3 + i} with thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        public static async void GetUp() {
            Console.WriteLine("into GetUp");
            Console.WriteLine($"before await-1 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"before await-2 with thread {Thread.CurrentThread.ManagedThreadId}");
            // await 之间的代码，在主线程上运行

            Console.WriteLine();
            Console.WriteLine("is going to await ... ");
            // await 开始异步运行，方法运行到这时会从这里开始返回方法调用者处执行代码
            await Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"in await with thread {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine();
            });

            Console.WriteLine();
            Console.WriteLine("after await:");
            // 直到 await 中内容执行完毕，才开始（但不是立即或同步）执行 await 之后的代码
            Console.WriteLine($"after await-3 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"after await-4 with thread {Thread.CurrentThread.ManagedThreadId}");
        }

        //如果是 wait 那么 Task 后面的语句是运行在主线程中的，并且 Task 后面的语句会跟着 Task 执行
        public static void GetUpWait() {
            Console.WriteLine("into GetUp");
            Console.WriteLine($"before await-1 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"before await-2 with thread {Thread.CurrentThread.ManagedThreadId}");

            Console.WriteLine();
            Console.WriteLine("is going to await ... ");
            Task task = Task.Run(() => {
                Console.WriteLine();
                Console.WriteLine($"in await with thread {Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine();
            });
            task.Wait();

            Console.WriteLine();
            Console.WriteLine("after task.Wait():");
            Console.WriteLine($"after await-3 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"after await-4 with thread {Thread.CurrentThread.ManagedThreadId}");
        }

    }
}
