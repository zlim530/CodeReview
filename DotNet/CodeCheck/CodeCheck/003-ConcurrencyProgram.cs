using System.Diagnostics;
using System;

public class _003_ConcurrencyProgram
{
    public static void Main00(string[] args)
    {
        Console.WriteLine("Main-Begin");

        //Standard();
        //UseThread();
        //UseThreadPool();
        //UseTask();
        //UseTaskCPU();
        UseTaskWait();
        //UseTaskCPU();
        UseAsyncAwait();
        //UseLinq();
        //UsePLinq();

        Console.WriteLine("Main-End");

        while (true) { Thread.Sleep(1000); }
    }

    public static async Task Main()
    {
        Console.WriteLine("Main-Begin");
        await UseAsyncAwaitAsync();
        Console.WriteLine("Main-End");
    }

    //模拟高耗时任务
    public static void Work(int id)
    {
        Console.WriteLine($"{id} Begin");
        char[] chars = new char[4] { 'A', 'B', 'C', 'D' };
        foreach (var item in chars)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"{id} {item}");
        }
        Console.WriteLine($"{id} End");
    }

    //优点：代码简单
    //缺点：串行执行，相互阻塞
    public static void Standard()
    {
        Work(1);
        Work(2);
    }

    //线程：创建和控制线程，设置其优先级并获取其状态。
    //优点：可以并行
    //缺点：开销大
    public static void UseThread()
    {
        Thread t1 = new Thread(x =>
        {
            Work(1);
        });
        t1.Start();
        Thread t2 = new Thread(x =>
        {
            Work(2);
        });
        t2.Start();
    }

    #region 线程

    //线程池：把线程托管到.NET线程池中，减少了线程创建的开销
    //优点：线程开销减少
    public static void UseThreadPool()
    {
        ThreadPool.QueueUserWorkItem(state =>
        {
            Work(1);
        });
        ThreadPool.QueueUserWorkItem(state =>
        {
            Work(2);
        });
    }

    #endregion

    #region 异步

    //Task：表示一个异步操作。
    //优点：线程开销减少,异步方法内部可以使用外部变量
    public static void UseTask()
    {
        var id1 = 1;
        var t1 = Task.Run(() =>
        {
            Work(id1);
        });

        var id2 = 2;
        var t2 = Task.Run(() =>
        {
            Work(id2);
        });
    }

    //优点：默认根据CPU数量开启线程数量,
    public static void UseTaskCPU()
    {
        Console.Read();
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < 100; i++)
        {
            var id = i;
            tasks.Add(Task.Run(() =>
            {
                WorkSingle(id);
            }));
        }
        Task.WaitAll(tasks.ToArray());
    }
    public static void WorkSingle(int id)
    {
        Thread.Sleep(1000);
        Console.WriteLine($"{id}");
        Thread.Sleep(2000);
    }

    //优点：增强了ThreadPool的能力，支持线程停止，状态返回、线程等待
    public static void UseTaskWait()
    {
        var t1 = Task.Run(() =>
        {
            Work(1);
        });

        var t2 = Task.Run(() =>
        {
            Work(2);
            Work(3);
        });

        Task.WaitAll(t1, t2);// 等待这两个线程，等这两个 task 执行完成后再执行 UseTaskWait 方法后面的代码：相当于将异步操作变成同步执行，可以手动控制异步代码的执行顺序
        // 在本代码中实现的效果是：Main 方法中打印的 Main-Begin 和 Main-End 会把 Work 方法中打印的值包起来
    }

    //async/await：异步方法同步执行：相当于 Task.WaitXXX() 方法执行的操作，简化写 Task.WaitXXX() 的代码，只需要使用 async/await 关键字即可
    //优点：UI操作方便，代码简单，线程开销减少,默认根据CPU数量开启线程数量
    public static async void UseAsyncAwait()
    {
        var r1 = await RunLongTimeTask(1, "A");// await 关键字：等待 RunLongTimeTask 函数执行完成后才会执行后续的代码
        Console.WriteLine($"1 {r1}");
        var r2 = await RunLongTimeTask(2, "B");
        Console.WriteLine($"2 {r2}");
    }

    // 如果在 Main 方法中调用此方法时还需要等待，那么可以在 Main 函数中也使用 async/await 代码
    public static async Task UseAsyncAwaitAsync() 
    {
        var r1 = await RunLongTimeTask(1, "A");// await 关键字：等待 RunLongTimeTask 函数执行完成后才会执行后续的代码
        Console.WriteLine($"1 {r1}");
        var r2 = await RunLongTimeTask(2, "B");
        Console.WriteLine($"2 {r2}");
    }

    //模拟高耗时任务
    public static Task<string> RunLongTimeTask(int id, string str)
    {
        return Task.Run(() =>
        {
            Thread.Sleep(1000);
            Console.WriteLine($"{id} {str}");
            Thread.Sleep(2000);
            return "Return " + str;
        });
    }

    #endregion

    #region 并行Linq

    //数组
    static List<int> Nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

    //顺序执行
    public static void UseLinq()
    {
        Nums.ForEach(x =>
        {
            WorkSingle(x);
        });
    }

    //并行执行
    public static void UsePLinq()
    {
        Nums.AsParallel().WithDegreeOfParallelism(2).ForAll(x =>
        {
            WorkSingle(x);
        });
    }

    #endregion
}

