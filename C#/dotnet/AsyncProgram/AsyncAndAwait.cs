using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

/**
 * @author zlim
 * @create 2020/7/31 22:22:24
 */
namespace AsyncProgram {
    /// <summary>
    /// 异步方法：async&await 与并行
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


        /// <summary>
        /// 异步方法2
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {
            Wrap();

            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"in Main() after async-{3 + i} with thread {Thread.CurrentThread.ManagedThreadId}");
            }
        }

        public static async void Wrap() {
            // 不能直接使用 await 获取 Get() 的值，而是调用它，让它先跑起来
            Task<int> task = Get();

            // 运行 Get()，碰到里面的 await 后，控制权返回到这里
            // 于是 Get() 中的 Task 与以下语句开始异步执行（节省就节省在这一点）
            Console.WriteLine($"in Wrap() after invoke Get() with thread {Thread.CurrentThread.ManagedThreadId}");
            // Console.WriteLine($"do something ... with thread {Thread.CurrentThread.ManagedThreadId}");

            // int y = task.Result;    // 阻塞当前线程进行等待
            // Result 获取到 task 中的值后只能往下走，而 await 在获取 task 值时即可以继续执行下面的代码也可以返回到 Main 方法 Wrap 调用处执行那里的代码

            // 直到 await 调用 Get() 取值
            // 如果 Get() 已经执行完毕,则马上取值;否则还得再把控制权往上抛
            int x = await task;     // 不阻塞当前线程，当前线程和 Task 并发/异步运行：即会到主线程调用 Wrap 方法后面的代码继续走
            // 如果是同步运行则会继续执行下面的代码，但是 await 是异步运行的

            // 只有上述 await task 中的 task 执行完毕后，才会执行异步方法 Wrap 中剩下的内容，也即下列代码：
            Console.WriteLine("in Wrap() ... ");
            Thread.Sleep(50);
            Console.WriteLine($"in Wrap() after Thread-{Thread.CurrentThread.ManagedThreadId}.Sleep(50) ... ");
            Console.WriteLine("in Wrap() return from Get():" + x );
        }

        public static async Task<int> Get() {
            Console.WriteLine("into Get");
            Console.WriteLine($"in Get() before await-1 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"in Get() before await-2 with thread {Thread.CurrentThread.ManagedThreadId}");

            int x =  await Task<int>.Run(() => {
                Console.WriteLine($"in Get() at await Thread-{Thread.CurrentThread.ManagedThreadId}.Sleep(1000) ... ");
                Thread.Sleep(1000);
                Console.WriteLine($"in Get() in await with thread {Thread.CurrentThread.ManagedThreadId}");
                return new Random().Next();
            });

            return x;
        }


        #region 错误示范 Wrap、Get 方法
        public static async void Wrap0() {
            Console.WriteLine("into Wrap");
            /*
            一、第一个产生代码分叉异步运行是这里，也即 Wrap 代码中 await 关键字中调用的 Get 方法与主线程中调用 Wrap 方法后面的代码产生了
            分叉，也即两者是异步运行：注意异步运行并不代表就新开了一个线程，不一定
            await 应该是要碰到 Task 才会新开一个线程进行异步执行，否则是在主线程（同一线程）中进行异步执行
            */
            int x = await Get();
            Console.WriteLine("in Wrap():" + x);
        }

        public static async Task<int> Get0() {
            Console.WriteLine("into Get");
            Console.WriteLine($"before await-1 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"before await-2 with thread {Thread.CurrentThread.ManagedThreadId}");

            /*
            二、第二个产生代码分叉异步运行的地方是这里，也即 Get 方法中 await 关键字中的 Task 与 Wrap 方法中调用 Get 方法后面的代码产生
            了分叉，也即这两者又是异步运行的：注意异步运行并不代表就新开了一个线程，不一定
            */
            int x = await Task<int>.Run(() => {
                Console.WriteLine($"in await with thread {Thread.CurrentThread.ManagedThreadId}");
                return new Random().Next();
            });

            x = x > 100 ? 100 : x;

            Console.WriteLine($"after await-3 with thread {Thread.CurrentThread.ManagedThreadId}");
            Console.WriteLine($"after await-4 with thread {Thread.CurrentThread.ManagedThreadId}");

            // 方法体内，返回的是 int
            return x;
        }

        #endregion



        /// <summary>
        /// 基于 Task 的并行
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {

            for (int i = 0; i < 5; i++) {
                // class System.Threading.Tasks.Parallel:提供对并行循环的和区域的支持
                // void Parallel.Invoke(params Action[] actions)：尽可能并行执行提供的每个操作
                Parallel.Invoke(() => {
                    Console.WriteLine(i);
                    Console.WriteLine($"task-{Task.CurrentId} begin:");
                    Console.WriteLine($"task-{Task.CurrentId} in thread-{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"task-{Task.CurrentId} end.");
                }, () => {
                    Console.WriteLine(i);
                    Console.WriteLine($"task-{Task.CurrentId} begin:");
                    Console.WriteLine($"task-{Task.CurrentId} in thread-{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"task-{Task.CurrentId} end.");
                });
            }
            #region 输出结果：并不是10个任务就开了10的线程，但是2个任务循环5次就有10个任务：这10个任务的执行时完全并行的，类似于一条马路中划分出了多条道路，不同任务之间是并行执行的，但是同一任务中是按照代码的编写顺序执行的，也即多条道路一起执行，但是每条道路中是按顺序一辆车一辆车的走
            /*
            0
            0
            task - 1 begin:
            task - 2 begin:
            task - 1 in thread - 4
            task - 2 in thread - 1
            task - 1 end.
            task - 2 end.
            1
            1
            task - 4 begin:
            task - 3 begin:
            task - 4 in thread - 1
            task - 3 in thread - 4
            task - 3 end.
            task - 4 end.
            2
            task - 6 begin:
            task - 6 in thread - 1
            task - 6 end.
            2
            task - 5 begin:
            task - 5 in thread - 5
            task - 5 end.
            3
            task - 8 begin:
            task - 8 in thread - 1
            task - 8 end.
            3
            task - 7 begin:
            task - 7 in thread - 5
            task - 7 end.
            4
            4
            task - 10 begin:
            task - 10 in thread - 1
            task - 10 end.
              task - 9 begin:
            task - 9 in thread - 5
            task - 9 end.*/

            #endregion              

            // Parallel 的其他方法
            Parallel.For(0, 10, x => Console.WriteLine(x));
            /*
            2
            0
            4
            8
            9
            7
            1
            5
            6
            3
            */

            Parallel.ForEach(Enumerable.Range(1, 10), x => Console.WriteLine(x));

        }


        /// <summary>
        /// 任务并行库（Task Parallel Library）
        /// </summary>
        /// <param name="args"></param>
        public static void Main3(string[] args) {
            //GetUpTasks().Wait();

            try {
                IEnumerable<int> numbers = Enumerable.Range(0,1000);
                
                //var filterd = numbers.Where(n => n % 11 == 0);// 串行运行
                //foreach (var item in filterd) {
                //    Console.WriteLine(item);
                //}

                // (扩展)ParallelQuery System.Collections.IEnumerable.AsParallel():启用查询的并行化
                var filterd = numbers.AsParallel()
                                     .Where(n => n % 11 == 0)
                                     /*.Where(n => 8 % (n > 100 ? n : 0) == 0)*/;// 异常：Attempted to divide by zero.

                // （扩展）void ParallelQuery<int>.ForAll<int>(Action<int action>):对 source 中的每个元素进行调用指定的操作
                filterd.ForAll(f => Console.WriteLine(f));

            } catch (AggregateException ae) {
                ae.Handle( e => {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }

        }


        /// <summary>
        /// 引入线程数组：Task[]
        /// </summary>
        /// <returns></returns>
        public static async Task GetUpTasks() {
            // 使用下面这种方式则会各个任务会按照顺序执行，类似于 new Task().ContinueWith() 并且是 ContinueWith 下面三个任务
            await Task.Run(() => Console.WriteLine("洗脸"));
            await Task.Run(() => Console.WriteLine("刷牙"));
            await Task.Run(() => Console.WriteLine("吃早餐"));
            await Task.Run(() => Console.WriteLine("背单词"));

            //Task[] tasks = {
            //    Task.Run( () => Console.WriteLine("洗脸")),
            //    Task.Run( () => Console.WriteLine("刷牙")),
            //    Task.Run( () => Console.WriteLine("吃早餐")),
            //    Task.Run( () => Console.WriteLine("背单词")),
            //};
            //（可等待）Task Task.WhenAll(System.Collections.Generic.IEnumerable < Task > tasks):
            //创建一个任务，该任务将在可枚举集合中的所有 Task 对象都已完成时完成
            //使用情况：await WhenAll(...);
            //await Task.WhenAll(tasks);
            /*
            执行效果是无序的：并发执行
                背单词
                洗脸
                刷牙
                吃早餐
            */
        }


    }

}
