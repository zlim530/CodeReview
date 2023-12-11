namespace DailyTest
{
    internal class AsyncProgramming
    {
        /// <summary>
        /// async 和 awiat 基本使用
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main0(string[] args)
        {
            Console.WriteLine("Hello, World!");
            using (HttpClient httpClient = new HttpClient())
            {
                string html = await httpClient.GetStringAsync("https://www.baidu.com");
                Console.WriteLine(html);
            }
            string txt = "hello async";
            string fileName = @"d:\temp\1.txt";
            await File.WriteAllTextAsync(fileName,txt);
            Console.WriteLine("Write in Success");
            string s = await File.ReadAllTextAsync(fileName);
            Console.WriteLine("File Content: " + s);
        }

        #region async 背后的线程切换
        /// <summary>
        /// 在 await 关键字前后执行的代码不一定在同一个线程中
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main01(string[] args)
        {
            System.Console.WriteLine($"1-ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            string str = new string('a', 100_000_00);
            await File.WriteAllTextAsync(@"C:\localcode\DailyCode\DailyCode\1.txt", str);
            System.Console.WriteLine($"2-ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            await File.WriteAllTextAsync(@"C:\localcode\DailyCode\DailyCode\2.txt", str);
            System.Console.WriteLine($"3-ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            File.WriteAllText(@"C:\localcode\DailyCode\DailyCode\3.txt", str);
            System.Console.WriteLine($"4-ThreadId= {Thread.CurrentThread.ManagedThreadId}");
            /* 可能的输出结果：
            两次 await 前后仅切换了一次线程：
            1-ThreadId= 1
            2-ThreadId= 13
            3-ThreadId= 13
            4-ThreadId= 13
            
            两次 await 前后均切换了一个新线程：
            1-ThreadId= 1
            2-ThreadId= 12
            3-ThreadId= 9
            4-ThreadId= 9
            */
        }
        #endregion

        #region 异步方法不等于多线程
        /// <summary>
        /// 让 await 关键字一定切换使用新的线程: with await Task.Run(...)
        /// </summary>
        /// <param name="args"></param>
        static async Task Main02(string[] args)
        {
            System.Console.WriteLine($"1-Main Func = {Thread.CurrentThread.ManagedThreadId}");
            // System.Console.WriteLine(await CalcAsync(10_000));
            System.Console.WriteLine(await CalcWithTaskRunAsync(10_000));
            System.Console.WriteLine($"2-Main Func = {Thread.CurrentThread.ManagedThreadId}");
            /* CalcAsync 不涉及到线程的切换：
            1-Main Func = 1
            1.1-CalcAsync Func:1
            5017.4211974489274727402
            2-Main Func = 1

            CalcWithTaskRunAsync 在 Task.Run 方法中进行了线程的切换：
            1-Main Func = 1
            1.1-CalcAsync Func:1
            1.2-Task Run:4
            4984.1701744448534359045
            2-Main Func = 4
            */
        }

        /// <summary>
        /// 此异步方法缺少 "await" 运算符，将以同步方式运行。请考虑使用 "await" 运算符等待非阻止的 API 调用，或者使用 "await Task.Run(...)" 在后台线程上执行占用大量 CPU 的工作。
        /// 也即 CalcAsync 方法内部并没有 await 关键字，其实就是一个普通的同步方法，但是我们希望实现异步的效果，那么可以使用 await Task.Run(...) 委托
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        private static async Task<decimal> CalcAsync(int v)
        {
            System.Console.WriteLine($"1.1-CalcAsync Func:{Thread.CurrentThread.ManagedThreadId}");
            decimal result = 1;
            Random random = new Random();
            for (int i = 0; i < v; i++)
            {
                result = result + (decimal)random.NextDouble();
            }
            return result;
        }


        static async Task<decimal> CalcWithTaskRunAsync(int n)
        {
            System.Console.WriteLine($"1.1-CalcAsync Func:{Thread.CurrentThread.ManagedThreadId}");
            return await Task.Run(() => {
                System.Console.WriteLine($"1.2-Task Run:{Thread.CurrentThread.ManagedThreadId}");
                decimal result = 1;
                Random random = new Random();
                for (int i = 0; i < n; i++)
                {
                    result = result + (decimal)random.NextDouble();
                }
                return result;
            });
        }
        #endregion

        #region 为什么有的异步方法没有 async
        static async Task Main010(string[] args)
        {
            string s1 = await ReadFileAsync(1);
            string s2 = await ReadFileWithoutAsync(2);
            // System.Console.WriteLine(s1);
            /* 
            总结：
            如果一个异步方法只是对别的异步方法进行简单的调用，并没有太多复杂的逻辑，比如获取异步方法的返回值后再做进一步的处理，
            就可以去掉 async、await 关键字
            */
        }

        /// <summary>
        /// 用 async 修饰的方法：
        /// 编译器会把 ReadFileAsync 方法中代码编译成一个类，然后根据 await 把代码分成多段执行。
        /// 因此，这样会加大程序集的尺寸与复杂度，还可能会导致程序的运行效率比普通方法低
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static async Task<string> ReadFileAsync(int num)
        {
            // switch 表达式写法
            // return num switch
            // {
            //     1 => await File.ReadAllTextAsync(@"C:\localcode\DailyCode\DailyCode\1.txt"),
            //     2 => await File.ReadAllTextAsync(@"C:\localcode\DailyCode\DailyCode\2.txt"),
            //     _ => throw new ArgumentException("num invalid"),
            // };
            switch (num)
            {
                case 1:
                    return await File.ReadAllTextAsync(@"C:\localcode\DailyCode\DailyCode\1.txt");
                case 2:
                    return await File.ReadAllTextAsync(@"C:\localcode\DailyCode\DailyCode\2.txt");
                default:
                    return await Task.FromResult("Test");
            }

        }


        /// <summary>
        /// 只要方法的返回值是 Task 类型，我们就可以用 await 关键字对其进行调用，而不用管被调用的方法是否用 async 修饰
        /// 由于我们在调用 File.ReadAllTextAsync 方法是没有使用 await 关键字，因此就不需要在方法声明中加上 async，反之也成立，因为没有在方法声明中加 async 关键字，所以我们不能在方法内部用 await
        /// 而由于没有使用 await 调用 File.ReadAllTextAsync，因此会直接返回 Task<string> 类型，但是由于 ReadFileWithoutAsync 方法的返回值就是 Task<string>，因此可以直接把 File.ReadAllTextAsync 返回值作为 ReadFileWithoutAsync 的返回值（如果使用 await 进行调用，则框架自动帮我们将返回值类型转换为 string 类型）
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static Task<string> ReadFileWithoutAsync(int num)
        {
            return num switch
            {
                1 => File.ReadAllTextAsync(@"C:\localcode\DailyCode\DailyCode\1.txt"),
                2 => File.ReadAllTextAsync(@"C:\localcode\DailyCode\DailyCode\2.txt"),
                _ => throw new ArgumentException("num invalid"),
            };
        }

        /// <summary>
        /// 手动创建 Task 对象并返回
        /// </summary>
        /// <param name="num"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        static Task WriteFileAsync(int num, string content)
        {
            switch (num)
            {
                case 1:
                    return File.WriteAllTextAsync(@"C:\localcode\DailyCode\DailyCode\1.txt", content);
                case 2:
                    return File.WriteAllTextAsync(@"C:\localcode\DailyCode\DailyCode\2.txt", content);
                default:
                    System.Console.WriteLine("文件暂时不可用");
                    return Task.CompletedTask;
            }
        }

        #endregion

        /// <summary>
        /// WhenAll：同时等待多个 Task 的执行结束
        /// 适用于有一个任务需要拆分为多个子任务，然后放到多个线程中执行，并且在所有子任务执行完毕后，再进行汇总处理
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main1(string[] args)
        {
            string[] files = Directory.GetFiles(@"D:\temp");
            Task<int>[] countTasks = new Task<int>[files.Length];
            for (int i = 0; i < files.Length; i++)
            {
                string fileName = files[i];
                Task<int> t = ReadCharsCount(fileName);
                countTasks[i] = t;
            }
            int[] counts = await Task.WhenAll(countTasks);
            int sum = counts.Sum();
            Console.WriteLine(sum);
            
        }

        /// <summary>
        /// CancellationToken：可以让异步方法提前终止
        /// 比如在 ASP.NET Core 开发的网站中，一个操作比较耗时，如果希望用户可以提前终止这个操作，可以通过 CancellationToken 对象进行
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main2(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            //cts.CancelAfter(5000);
            CancellationToken token = cts.Token;
            //await DownloadHtmlAsync("https://www.baidu.com",100, token);
            DownloadHtmlAsync("https://www.baidu.com",200, token);
            while (Console.ReadLine() != "e")
            {

            }
            cts.Cancel();
            Console.ReadLine();
        }

        /// <summary>
        /// 异步其他问题：yield return
        /// </summary>
        /// <param name="args"></param>
        static async Task Main00(string[] args)
        {
            await foreach (var item in Test3())
            {
                Console.WriteLine(item);
            }
            /*
            ASP .NET Core 和控制台项目中没有 SynchronizationContext,ConfigureAwait(false) 等。
            因此不用管，不要同步、异步混用。
            */
        }

        static async Task<int> ReadCharsCount(string fileName)
        {
            string s = await File.ReadAllTextAsync(fileName);
            return s.Length;
        }

        /// <summary>
        /// 一般不需要自己处理 CancellationToken，只要做到“能转发 CancellationToken 就转发”即可
        /// </summary>
        /// <param name="url"></param>
        /// <param name="n"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        static async Task DownloadHtmlAsync(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    //var resp = await client.GetAsync(url, cancellationToken);
                    var resp = await client.GetAsync(url);
                    var html = await resp.Content.ReadAsStringAsync();
                    //string html = await client.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now} : {html}");
                    if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("The request is cancelled.");
                        break;
                    }
                    //cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
        
        /// <summary>
        /// 常规的写法是一次性组装完所有数据后返回，然后在 Main 函数中打印出来
        /// </summary>
        /// <returns></returns>
        static IEnumerable<string> Test1()
        {
            List<string> list = new List<string>();
            list.Add("hello");
            list.Add("async");
            list.Add("async.com");
            return list;
        }

        /// <summary>
        /// 而使用 yield return 调试后可以发现它是先 return hello, 在 Main 函数中进行打印，然后又进入方法返回 async 在屏幕上打印，是一次性一次性的将数据返回而不用等所有数据组装完毕后再返回；
        /// yield return 背后的原理是编译器将其编译为状态机，这与 async await 的语法糖类似，因此在 C# 8.0 之前 async 方法中不能用 yield
        /// </summary>
        /// <returns></returns>
        static IEnumerable<string> Test2()
        {
            yield return "hello";
            yield return "async";
            yield return "async.com";
        }

        /// <summary>
        /// 错误写法：编译不通过
        /// Task<IEnumerable<string>>”不是迭代器接口类型，因此“Program.Test3()”体不能是迭代器块 
        /// </summary>
        /// <returns></returns>
        /*static async Task<IEnumerable<string>> Test3()
        {
            yield return "hello";
            yield return "async";
            yield return "async.com";
        }*/

        /// <summary>
        /// 正确写法：编译不通过
        /// 从 C# 8.0 开始，把返回值声明为 IAsyncEnumerable(不要带 Task)，然后遍历时用 await foreach() 即可
        /// </summary>
        /// <returns></returns>
        static async IAsyncEnumerable<string> Test3()
        {
            yield return "hello";
            yield return "async";
            yield return "async.com";
        }
    }
}