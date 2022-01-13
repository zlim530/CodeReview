using System;
using BenchmarkDotNet.Running;

namespace BenchmarkExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            // Console.WriteLine(DateTime.Now);
            // Console.WriteLine(DateTime.UtcNow);
            BenchmarkRunner.Run<DateParserBenchmarks>();
        }
    }
}