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

        /// <summary>
        /// WhenAll
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
        /// CancellationToken
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