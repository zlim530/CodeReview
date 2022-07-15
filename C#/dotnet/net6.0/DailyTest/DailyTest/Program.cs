namespace DailyTest
{
    internal class Program
    {
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

        static async Task Main(string[] args)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(5000);
            CancellationToken token = cts.Token;
            await DownloadHtmlAsync("https://www.baidu.com",100, token);
        }

        static async Task<int> ReadCharsCount(string fileName)
        {
            string s = await File.ReadAllTextAsync(fileName);
            return s.Length;
        }


        static async Task DownloadHtmlAsync(string url, int n, CancellationToken cancellationToken)
        {
            using (HttpClient client = new HttpClient())
            {
                for (int i = 0; i < n; i++)
                {
                    string html = await client.GetStringAsync(url);
                    Console.WriteLine($"{DateTime.Now} : {html}");
                    /*if (cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("The request is cancelled.");
                        break;
                    }*/
                    cancellationToken.ThrowIfCancellationRequested();
                }
            }
        }
        
    }
}