using System;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;

/**
 * @author zlim
 * @create 2020/7/29 15:41:45
 */
namespace AsyncProgram {
    /// <summary>
    /// Task 讲解
    /// </summary>
    public class TaskPractice {
        /// <summary>
        /// Task 之 Action 讲解
        /// </summary>
        /// <param name="args"></param>
        public static void Main0(string[] args) {
            Action getUp = () => {
                Console.WriteLine($"Task:{Task.CurrentId}:起床啦！..."+
                    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Task:{Task.CurrentId}:洗脸 ..." +
                    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
            };

            //推荐顺序：Task.Run() =>Task.Factory.StartNew() =>new Task()
            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"第{i + 1}次循环：");
                // new 出来的 Task 需要显式调用 Start() 才开始运行
                // 但是好处是，可以在 t1.Start(); 之前写代码
                Task t1 = new Task(getUp);
                //Console.WriteLine("Before t1.Start() ... ");
                //Console.WriteLine("Before t1.Start() ... ");
                //t1.Start();
                Console.WriteLine("t1 start ... ");

                //Task t1 = Task.Run(getUp);// 更灵活
                // 不需要再调用 Start()，通过 Run 方法生成 Task 会自动运行，此时再调用 Start() 会抛出异常
                //t1.Start();// Unhandled exception. System.InvalidOperationException: Start may not be called on a task that was already started.
                //Console.WriteLine("Task.Run(getUp) ... ");

                //Task t1 = Task.Factory.StartNew(getUp);// 拥有众多重载
                //Console.WriteLine("Task.Factory.StartNew(getUp) ... ");

                Console.WriteLine("HelloWorld");
                Console.WriteLine("HelloWorld");
                Console.WriteLine("HelloWorld");

                //t1.Wait();// 确保 t1 任务完成：会一直等着当前任务执行完毕，只有当当前任务执行完了才会继续往后执行程序中的代码
                //Console.WriteLine("t1.Wait() ... ");

                t1.RunSynchronously();// 对当前的 Task 同步运行 TaskScheduler：那么完全就没有异步运行的效果了
                Console.WriteLine("t1.RunSynchronously() ... ");

            }
            /*
            说明：
                因为 Task 是异步执行并且是基于线程池中线程的，因此在 Task 执行时会打乱时间顺序， 当一个 Task 拥有 CPU 的时间片是就会通过线程池中的一个线程执行，如果此时该 Task 失去了 CPU 的时间片，那么该 Task 就原地等待；当再次被分配到 CPU 的时间片时，该 Task 会按照上次执行到哪的顺序接着往下执行，而不是从头开始，并且接着往下执行时依然会通过上次线程池中的那个线程来执行；
                不同的 Task 执行，可能会有下个 Task 从线程池中分配到的线程是上个 Task 分配到的线程，这样是不影响的，因为 Task 是从线程池中拿线程，所以开发者是无法控制某一个 Task 分配到哪一个线程的。但是 Task 只要能分配到线程就表明此线程是可用的。
            通过 new Task 输出结果为：
                第1次循环：
                t1 start ...
                第2次循环：
                t1 start ...
                第3次循环：
                t1 start ...
                第4次循环：
                t1 start ...
                第5次循环：
                t1 start ...
                第6次循环：
                t1 start ...
                第7次循环：
                t1 start ...
                第8次循环：
                t1 start ...
                第9次循环：
                t1 start ...
                第10次循环：
                Task:3:起床啦！...ThreadId:6
                Task:4:起床啦！...ThreadId:4
                Task:4:洗脸 ...ThreadId:4
                Task:2:起床啦！...ThreadId:7
                Task:2:洗脸 ...ThreadId:7
                Task:6:起床啦！...ThreadId:7
                Task:1:起床啦！...ThreadId:5
                Task:5:起床啦！...ThreadId:4
                Task:5:洗脸 ...ThreadId:4
                Task:7:起床啦！...ThreadId:4
                Task:7:洗脸 ...ThreadId:4
                Task:8:起床啦！...ThreadId:4
                t1 start ...
                Task:3:洗脸 ...ThreadId:6
                Task:6:洗脸 ...ThreadId:7
                Task:10:起床啦！...ThreadId:7
            
            */

        }


        /// <summary>
        /// Task 之 Func 讲解
        /// </summary>
        /// <param name="args"></param>
        public static void Main1(string[] args) {

            #region Task 任务之 Func<out TResult> 型 
            /*Func<long> getUp = () => {
                Console.WriteLine($"Task:{Task.CurrentId}:起床啦！..." +
                    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                Console.WriteLine($"Task:{Task.CurrentId}:洗脸 ..." +
                    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                return DateTime.Now.Ticks;
            };

            //推荐顺序：Task.Run() =>Task.Factory.StartNew() =>new Task()
            for (int i = 0; i < 10; i++) {
                Console.WriteLine($"第{i + 1}次循环：");
                // new 出来的 Task 需要显式调用 Start() 才开始运行
                Task<long> t1 = new Task<long>(getUp);
                t1.Start();

                Show(t1);

                // long Task<long>.Result { get; }:获取此 Task<TResult> 的结果值
                //Console.WriteLine("t1 Result = {0}",t1.Result);


            }*/
            #endregion


            #region Task 任务之 Func<object TIn,out TResult> 型
            //方式一：在 new Task<long>() 时传入一个参数
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(2000);

                Task<long> t1 = new Task<long>((n) => {
                    Console.WriteLine($"第{n}次 Task：{Task.CurrentId}:起床啦！..." +
                    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                    return DateTime.Now.Ticks;
                }, i);
                t1.Start();

                //Task<long> task = Task<long>.Run( () => {
                //    Console.WriteLine($"第{i + 1}次 Task：{Task.CurrentId}:起床啦！..." +
                //    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                //    return DateTime.Now.Ticks;
                //});

                //Show(t1);
            }

            // ==================================================================

            // 方式2：通过闭包（在方法内部调用非方法内的参数）直接使用外部变量来实现：
            /*for (int i = 0; i < 10; i++) {
                // 可能会出现主线程中的代码运行的 {i + 1} 的结果与 Task 任务中运行的 {i + 1} 的结果不一样
                // 原因是：主线程上的 i 进行了 i++ 自增操作，但还没来得及进行 i < 10 比较时，就被 for 循环中执行任务所分配的子线程拿到的
                // 那么此时在任务中再进行 {i + 1} 计算就可能会发生打印出 （第11次）的结果：这其实就是并发下的线程不安全问题
                Console.WriteLine($"第{i + 1}次");

                Task<long> t1 = new Task<long>(() => {
                    Console.WriteLine($"（第{i + 1}次）Task-{Task.CurrentId}：起床啦！~" +
                                      $"ThreadId is {Thread.CurrentThread.ManagedThreadId}");
                    return DateTime.Now.Ticks;
                });

                t1.Start();

                Show(t1);

            }*/

            // 通过 while 循环来看：
            /*int i = 0;
            while (i < 10) {
                Console.WriteLine($"第{i}次：");

                Task<long> t1 = new Task<long>(() => {
                    Thread.Sleep(1);// 延长任务的执行时间，让它有机会在 i++ 语句后面执行 Task 中的代码
                    Console.WriteLine($"（第{i}次）Task-{Task.CurrentId}：起床啦！~" +
                                      $"ThreadId is {Thread.CurrentThread.ManagedThreadId}");
                    return DateTime.Now.Ticks;
                });

                t1.Start();

                Show(t1);

                Console.WriteLine("helloworld!");
                Console.WriteLine("helloworld!");

                i++;
                Console.WriteLine($"{i}++");
                *//*
                也就是说，如果是同步执行的代码，那执行完 Console.WriteLine($"{i}++"); 语句后会按照程序语句逻辑顺序返回去执行
                while(i < 10) 判断，此时判断为 false 于是就会跳出 while 循环
                但是由于 Task 是基于异步多线程执行的，那么 Task 中代码的执行顺序是混乱的，因此就有可能 Task 中的代码会在 i++ 语句
                后面执行。
                *//*
                //Task<long> t1 = new Task<long>(() => {
                //    Thread.Sleep(1);
                //    Console.WriteLine($"（第{i + 1}次）Task-{Task.CurrentId}：起床啦！~" +
                //                      $"ThreadId is {Thread.CurrentThread.ManagedThreadId}");
                //    return DateTime.Now.Ticks;
                //});
            }*/

            /*
            输出结果：
                第1次：
                Task-2.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                （第2次）Task-1：起床啦！~ThreadId is 4
                helloworld!
                2++
                第2次：
                Task-3.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                helloworld!
                3++
                第3次：
                （第4次）Task-2：起床啦！~ThreadId is 5
                Task-4.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                helloworld!
                （第4次）Task-3：起床啦！~ThreadId is 6
                4++
                第4次：
                Task-5.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                （第5次）Task-4：起床啦！~ThreadId is 6
                helloworld!
                helloworld!
                （第5次）Task-5：起床啦！~ThreadId is 4
                5++
                第5次：
                Task-6.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                helloworld!
                6++
                第6次：
                （第7次）Task-6：起床啦！~ThreadId is 7
                Task-7.Start() ... task.status is Running,task.IsCompleted is False,ThreadId is 1
                （第7次）Task-7：起床啦！~ThreadId is 7
                helloworld!
                helloworld!
                7++
                第7次：
                Task-8.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                helloworld!
                8++
                （第8次）Task-8：起床啦！~ThreadId is 7
                第8次：
                Task-9.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                helloworld!
                （第9次）Task-9：起床啦！~ThreadId is 7
                9++
                第9次：
                Task-10.Start() ... task.status is WaitingToRun,task.IsCompleted is False,ThreadId is 1
                helloworld!
                helloworld!
                10++
                （第10次）Task-10：起床啦！~ThreadId is 7
            */

            #endregion

        }


        /// <summary>
        /// Task 之 ContinueWith
        /// </summary>
        /// <param name="args"></param>
        public static void Main2(string[] args) {
            //for (int i = 0; i < 10; i++) {
                Task<int> getUpTask = Task<int>.Run(() => {
                    int seed = new Random().Next(100);
                    Console.WriteLine($"{seed}: Task-{Task.CurrentId}: 起床啦！" +
                                      $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                    return seed;
                });

                // Task Task<int>.ContinueWith(Action<Task<int>,object?> continuationAction,object? state);
                // 创建一个传递有状态信息并在目标 Task<TResult> 完成时执行的延续
                // 确保了不同 Task 之间的运行顺序，也即保证了在 getUpTask 运行完之后再运行 ContinueWith 中的那个 Task
                getUpTask.ContinueWith((x) => {
                    Console.WriteLine(x == getUpTask);// 这里的传入的 x 其实就是调用 ContinueWith 方法的任务
                    Console.WriteLine($"{x.Result}: Task-{Task.CurrentId}: 起床结束！" +
                                      $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"{x.Result}: Task-{Task.CurrentId}: 刷牙洗脸！" +
                                      $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                });
            //}

            // =============================================================================================

            // task.ContinueWith() 与 task.Wait() 的对比
            /*for (int i = 0; i < 10; i++) {
                Task<int> getUpTask = Task<int>.Run(() => {
                    int seed = new Random().Next(100);
                    Console.WriteLine($"{seed}: Task-{Task.CurrentId}: 起床啦！" +
                                      $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                    return seed;
                });

                getUpTask.Wait();

                Task task = Task.Run( () => {
                    // 因为这里没有传入参数，我们只能通过 Task.Result 来获得上个任务的返回值
                    // 并且调用 Task.Result 属性会让此任务等待 Task 任务完成再执行
                    Console.WriteLine($"{getUpTask.Result}: Task-{Task.CurrentId}: 起床结束！" +
                                      $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                    Console.WriteLine($"{getUpTask.Result}: Task-{Task.CurrentId}: 刷牙洗脸！" +
                                      $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                });

            }*/

            // =============================================================================================
            // 不使用 ContinueWith() 和 Wait()，而使用 getUpTask.Result，观察任务执行情况
            /*Task<DateTime> getUp = Task<DateTime>.Run(() => {
                //Thread.Sleep(10);
                Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}" +
                                  $"Task-{Task.CurrentId}:起床啦！！！");
                return DateTime.Now;
            });

            Task.Run(() => {
                Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}" +
                                  $"Task-{Task.CurrentId}:起床结束！！！");
                Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}" +
                                  $"Task-{Task.CurrentId}:刷牙洗脸 ... ");
                Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}" +
                                  $"Task-{Task.CurrentId}:{getUp.Result}");// Task.Result 会阻塞线程
            });*/
            /*
            观察程序输出结果，这两个 Task 是异步并行的关系，两者都不需要等待另外一方运行完成再执行；
            但是第二个 Task 中的最后一条语句，使用了别的 Task 的返回值，也即 Task.Result 属性，
            那么它就会等到前一个任务执行完成再执行，也即在程序输出结果中输出的时间那一行永远不可能
            出现在 起床啦！！！ 语句前面
            */


        }


        /// <summary>
        /// 打印当前任务与线程的信息
        /// </summary>
        /// <param name="task"></param>
        public static void Show(Task task) {
            Console.WriteLine($"Task-{task.Id}.Start() ... " + 
                              $"task.status is {task.Status}," +
                              $"task.IsCompleted is {task.IsCompleted}," +
                             /* $"task.AsyncState is {task.AsyncState},"+*/
                              $"ThreadId is {Thread.CurrentThread.ManagedThreadId}");
            // 只要不是在子线程（Thread）或者任务（Task）中执行的代码那么就会在主线程中执行
            // ThreadId is 1:只要是在主线程的代码那么其 ManagedThreadId 就是1
        }


        /// <summary>
        /// 多线程中的异常处理
        /// </summary>
        /// <param name="args"></param>
        public static void Main3(string[] args) {
            #region 演示单个多线程的异常处理
            /*Task<DateTime> getUp = Task<DateTime>.Run(() => {
                throw new Exception("ouuried a exception ... ");
                Thread.Sleep(10);
                Console.WriteLine($"ThreadId:{Thread.CurrentThread.ManagedThreadId}" +
                                  $"Task-{Task.CurrentId}:起床啦！！！");
                return DateTime.Now;
            });

            //多线程中一个线程是无法捕获另外一个线程中发生的异常的，也即主线程是无法捕获子线程的异常的，除非碰到了 == Task.Wait() == 方法
            getUp.Wait();*/
            // Unhandled exception. System.AggregateException: One or more errors occurred. (ouuried a exception ... )
            #endregion


            #region 多个 Task 异常处理之 catch

            /*Task[] getUps = new Task[10];
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(100);
                getUps[i] = Task.Run(() => {
                    throw new Exception(i.ToString());
                });
            }

            try {
                // void Task.WaitAll(params Task[] tasks) ：等到提供的所有 Task 对象完成执行过程
                Task.WaitAll(getUps);
            } catch (AggregateException ae) {
                foreach (var item in ae.InnerExceptions) {
                    Console.WriteLine(item.Message);// 这里的打印结果不保证是按顺序输出1到10，因为 Task 是异步执行的
                }
            }*/

            #endregion


            #region 多个 Task 异常处理之 Handle 

            /*Task[] getUps = new Task[10];
            for (int i = 0; i < 10; i++) {
                Thread.Sleep(1);
                getUps[i] = Task.Run(() => {
                    throw new Exception(i.ToString());
                });
            }

            try {
                Task.WaitAll(getUps);
            } catch (AggregateException ae) {
                // void AggregateException.Handle(Func<Exception,bool> predicate)
                // 在每个由此 AggregateException 包含的 Exception 上调用处理程序
                // Handle 的参数接收的是带 Exception 参数返回值为 bool 类型的方法
                // 并且返回 true 则不处理 Task 中的异常，返回 false 则表示处理异常
                // Handle 处理的是 InnerExceptions 中的异常，因此这里的 x 其实就是 InnerExceptions
                ae.Handle(x => {
                    Console.WriteLine(x.Message);
                    //return (x as AggregateException) == null;
                    //return (x is AggregateException);
                    return true;
                });
            }*/

            #endregion


            /*
            使用注意事项：
                throw：
                    ·异常会带来较大的资源开销（性能损耗），所以要尽可能避免异常被抛出（不是不写 throw exception 的代码）
                    ·不要使用 Exception 作为分支判断条件
                    ·尽可能的使用具体的、.NET 现有的异常
                try ... catch：
                    ·不知道怎么处理的，就不要处理
                    ·不要 catch 之后，什么都不做（或者就包裹一下），直接 throw
                    ·总是在程序入口（顶层方法）处 catch 未捕获的异常（看情况）
                finally：
                    ·如果仅仅是为了释放资源，推荐使用 using 代码块
                    ·使用 using 时注意和 try-catch 的搭配
            */
            #region Task 异常属性

            /*Task getup = Task<int>.Run(() => {
                Console.WriteLine($"at await in GetUp() with thread {Thread.CurrentThread.ManagedThreadId}");

                if (DateTime.Now.Ticks % 2 == 0) {
                    throw new LCEexception();
                } else {
                    throw new NotImplementedException();
                }
            });

            while (!getup.IsCompleted) {
                Console.WriteLine(getup.Status);
            }

            if (getup.IsFaulted) {
                foreach (var e in getup.Exception.InnerExceptions) {
                    if (e is LCEexception) {
                        Console.WriteLine((e as LCEexception).Name);
                    } else if (e is NotImplementedException) {
                        Console.WriteLine("Not Implement.");
                    } else {
                        Console.WriteLine(e.Message);
                    }
                }
            }*/

            #endregion

        }



        public static void Greet(Func<string, bool> predicate) {
            //bool result = predicate("hello");
            var name = "zlim";
            for (int i = 0; i < 5; i++) {
                Console.WriteLine(predicate(name));
                //if (!predicate(name)) {

                //}
            }
        }

        /// <summary>
        /// Func 复习
        /// </summary>
        /// <param name="args"></param>
        public static void Main4(string[] args) {
            /*var name = "zlim";
            Greet((x) => {
                return x == name;
            });

            // lambda 表达式中如果方法只有一个参数，方法体只有一条语句，那么可以省略 () 与 {} 
            // 如果方法体中的唯一的那条语句是 return 语句，那么 return 也可以省略
            Greet( x => x == name+"530");*/

            // =====================================================================

            Student s = new Student("zlim");
            s.Greet((s) => {
                return s.Name == "zlim530";            
            });

        }


        /// <summary>
        /// Task 的取消
        /// </summary>
        /// <param name="args"></param>
        public static void Main5(string[] args) {
            // System.Threading.CancellationTokenSource:通知 CancelationToken，告知其应被取消
            CancellationTokenSource source = new CancellationTokenSource();
            // readonly struct System.Threading.CancellationToken：传播有关应取消操作的通知
            CancellationToken token = source.Token;

            Task<DateTime> getup = Task<DateTime>.Run( () => {
                // void CancellationToken.ThrowIfCancellationRequest()：如果已请求取消此标记，则引发 OperationCanceledException
                token.ThrowIfCancellationRequested();// 如果 Task 被 Cancel()，则会抛出异常
                Console.WriteLine($"Task-{Task.CurrentId}:起床啦！！"+
                    $"ThreadId:{Thread.CurrentThread.ManagedThreadId}");
                return DateTime.Now;
            },token);// 传入 token 指令，确保 Cancel 通知能被侦听

            // void CancellationTokenSource.Cancel()：传达取消请求
            source.Cancel();// 使用 source 进行 Cancel

            try {
                getup.Wait();// 只有在 Wait() 时才能捕获异常
            } catch (AggregateException ae) {
                // 使用 Handle() 方法进行处理
                ae.Handle( ie => {
                    Console.WriteLine("Canceled?");
                    return true;// 表示已经成功处理，不需要再抛出异常；return false 则会抛出异常
                });
            }
        }

    }

    [Serializable]
    internal class LCEexception : Exception {

        public string Name { get; } = "zlim";

        public LCEexception() {
        }

        public LCEexception(string message) : base(message) {
        }

        public LCEexception(string message, Exception innerException) : base(message, innerException) {
        }

        protected LCEexception(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }

    public class Student {

        public Student(string name) {
            Name = name;
        }
    
        // 只读属性只能在构造函数与初始化时进行赋值
        public string Name { get; }

        public void Greet(Func<Student, bool> predicate) {
            for (int i = 0; i < 5; i++) {
                Console.WriteLine(predicate(this));
            }
        }

    }


}
