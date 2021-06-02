using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace DailyLocalCode
{
    class Program
    {
        static void Main0(string[] args)
        {
            Console.WriteLine("Hello World!");

            var cesp = new ConcurrentExclusiveSchedulerPair();
            Task.Factory.StartNew(() => {
                Console.WriteLine(TaskScheduler.Current == cesp.ExclusiveScheduler);// True
            },default,TaskCreationOptions.None,cesp.ExclusiveScheduler).Wait();
        }

        public void DoWork(Action worker, Action completion)
        {
            SynchronizationContext sc = SynchronizationContext.Current;
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    worker();
                }
                finally
                {
                    sc.Post( _ => completion(),null);
                }
            });
        }

        static void Main2(string[] args)
        {
            DateTime tempFinishTime = new DateTime(1, 1, 1);
            Console.WriteLine(tempFinishTime);// 0001/1/1 0:00:00
            tempFinishTime = new DateTime(1, 1, 1, 0, 0, 0);
            Console.WriteLine(tempFinishTime);// 0001/1/1 0:00:00
            var duringTime = (DateTime.Now - tempFinishTime).TotalHours.ToString("#0.00");
            Console.WriteLine(duringTime);
        }

        static void Main3(string[] args)
        {
            IEnumerable<int> numbers = Enumerable.Range(1,10);

            var filter = numbers.Where(n => n % 2 == 0)
                                .Select(n =>
                                {
                                    throw new Exception("故意抛个异常");
                                    return n * 10;
                                })
                                .Take(3);
            /*
            我们知道Linq是由一系列基于IEnumerable的扩展方法组成，返回值也都是IEnumerable，而IEnumerable只是一个迭代器对象，每次读取IEnumerable对象时，其实只是遍历里面的一个元素。上面方法的真正流程应该是流式的类似管道的操作，即读一个数字处理一个，边读边处理。
            其实代码中的filter变量可以理解成一堆算法的包装器，只是封装了一系列对数据的操作，但只有元素被使用时才会执行。我们在代码后加一段遍历filter并输出的代码，这时元素被使用到了，就会报错了。
            */

            int sum = 0;
            foreach (var item in filter)
            {
                sum += item;
            }

            Console.ReadKey();
        }

        static void Main1(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine((DateTime.Now - new DateTime(2020, 11, 16, 10, 57, 24)).TotalHours.ToString("#0.00"));
            Console.WriteLine(DateTime.Now - new DateTime(2020,12,16,10,57,24));
        }

        static async Task Main4(string[] args)
        {
            await SingleProducerSingleConsumer();
            Console.ReadKey();
        }

        public static async Task SingleProducerSingleConsumer()
        {
            var channel = Channel.CreateUnbounded<int>();
            var reader = channel.Reader;
            for (int i = 0; i < 10; i++)
            {
                await channel.Writer.WriteAsync(i + 1);
            }

            while (await reader.WaitToReadAsync())
            {
                if (reader.TryRead(out var number))
                {
                    Console.WriteLine(number);
                }
            }
        }

    }

}
