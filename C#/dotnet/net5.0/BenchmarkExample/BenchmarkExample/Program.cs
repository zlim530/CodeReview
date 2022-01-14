using System;
using System.Collections.Generic;
using System.Net.Http;
using BenchmarkDotNet.Running;
using System.Runtime.Caching;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;

namespace BenchmarkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // Console.WriteLine(DateTime.Now);
            // Console.WriteLine(DateTime.UtcNow);
            // BenchmarkRunner.Run<DateParserBenchmarks>();
            var summary = BenchmarkRunner.Run<BenchMarkService>();
            /*
            |            Method |     Mean |   Error |  StdDev |     Gen 0 | Allocated |
            |------------------ |---------:|--------:|--------:|----------:|----------:|
            |      LoadDataTask | 131.4 ms | 0.49 ms | 0.46 ms | 9250.0000 |     42 MB |
            | LoadDataValueTask | 134.5 ms | 0.33 ms | 0.26 ms | 4750.0000 |     21 MB |
             由此可见，除去第一次，随后当缓存数据中有数据存储时，并不会执行后续的 await 语句
             而此时创建 Task<object> 造成的内存空间消耗是 ValueTask<object> 的两倍之多
            */

        }
    }

    [MemoryDiagnoser]
    public class BenchMarkService
    {
        private readonly IEnumerable<string> webSites = new string[]
        {
            "https://www.zhihu.com",
            "https://www.baidu.com",
            "https://www.weibo.com"
        };

        [Benchmark]
        public async Task LoadDataTask()
        {
            DownloadService downloadService = new DownloadService();
            
            for (int i = 0; i < 100_000; i++)
            {
                foreach (var webSite in webSites)
                {
                    await downloadService.DownloadDataTask(webSite);
                }
            }
        }

        [Benchmark]
        public async ValueTask LoadDataValueTask()
        {
            DownloadService downloadService = new DownloadService();
            
            for (int i = 0; i < 100_000; i++)
            {
                foreach (var webSite in webSites)
                {
                    await downloadService.DownloadDataValueTask(webSite);
                }
            }
        }

    }

    public class DownloadService
    {
        private readonly MemoryCache _cache;
        private readonly HttpClient _httpClient;
        private readonly CacheItemPolicy _cacheItemPolicy;

        public DownloadService()
        {
            _cache = MemoryCache.Default;
            _httpClient = new HttpClient();
            _cacheItemPolicy = new CacheItemPolicy()
            {
                SlidingExpiration =  TimeSpan.FromDays(1)
            };
        }

        public async Task<object> DownloadDataTask(string website)
        {
            if (_cache.Contains(website))
            {
                return _cache.Get(website);
            }

            var response = await _httpClient.GetAsync(website);
            _cache.Set(website, response, _cacheItemPolicy);

            return response;
        }

        public async ValueTask<object> DownloadDataValueTask(string website)
        {
            if (_cache.Contains(website))
            {
                return _cache.Get(website);
            }

            var response = await _httpClient.GetAsync(website);
            _cache.Set(website, response, _cacheItemPolicy);

            return response;
        }
        
    }
}