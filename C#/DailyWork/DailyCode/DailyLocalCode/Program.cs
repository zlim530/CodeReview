using Fleck;
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
        /// <summary>
        /// ConcurrentExclusiveSchedulerPair
        /// </summary>
        /// <param name="args"></param>
        static void Main0(string[] args)
        {
            Console.WriteLine("Hello World!");

            var cesp = new ConcurrentExclusiveSchedulerPair();
            Task.Factory.StartNew(() => {
                Console.WriteLine(TaskScheduler.Current == cesp.ExclusiveScheduler);// True
            },default,TaskCreationOptions.None,cesp.ExclusiveScheduler).Wait();
        }

        /// <summary>
        /// Delegate
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="completion"></param>
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

        /// <summary>
        /// DateTime
        /// </summary>
        /// <param name="args"></param>
        static void Main2(string[] args)
        {
            DateTime tempFinishTime = new DateTime(1, 1, 1);
            Console.WriteLine(tempFinishTime);// 0001/1/1 0:00:00
            tempFinishTime = new DateTime(1, 1, 1, 0, 0, 0);
            Console.WriteLine(tempFinishTime);// 0001/1/1 0:00:00
            var duringTime = (DateTime.Now - tempFinishTime).TotalHours.ToString("#0.00");
            Console.WriteLine(duringTime);

            Console.WriteLine(DateTime.Now);
            Console.WriteLine((DateTime.Now - new DateTime(2020, 11, 16, 10, 57, 24)).TotalHours.ToString("#0.00"));
            Console.WriteLine(DateTime.Now - new DateTime(2020, 12, 16, 10, 57, 24));
        }

        /// <summary>
        /// LINQ
        /// </summary>
        /// <param name="args"></param>
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

        /// <summary>
        /// Invoke SingleProducerSingleConsumer()
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static async Task Main4(string[] args)
        {
            await SingleProducerSingleConsumer();
            Console.ReadKey();
        }

        /// <summary>
        /// Creates an unbounded channel usable by any number of readers and writers concurrently.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Test LINQ
        /// </summary>
        /// <param name="args"></param>
        static void Main5(string[] args)
        {
            var personList = new List<Person> { 
                new Person() { Name = "jack", Age = 20},
                new Person() { Name = "elen", Age = 25},
                new Person() { Name = "john", Age = 22}
            };

            var query = personList.Where(m => m.Age > 20).ToList();

            Console.WriteLine($"query.Count = {query.Count}");

            Console.ReadLine();
        }

        /// <summary>
        /// Fleck of WebSocket
        /// </summary>
        /// <param name="args"></param>
        static void Main6(string[] args)
        {
            // Official demo:
            //var server = new WebSocketServer("ws://0.0.0.0:8181");
            //server.Start(socket =>
            //{
            //    socket.OnOpen = () => Console.WriteLine("Open!");
            //    socket.OnClose = () =>
            //    Console.WriteLine("Close!");
            //    socket.OnMessage = message => socket.Send(message);
            //});

            IDictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();
            WebSocketServer server = new WebSocketServer("ws://0.0.0.0:30000");
            server.RestartAfterListenError = true;

            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    dic_Sockets.Add(clientUrl, socket);
                    Console.WriteLine($"{DateTime.Now.ToString()}+|服务器：客户端网页：{clientUrl}建立WebSocket连接！");
                };

                socket.OnClose = () => 
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    if (dic_Sockets.ContainsKey(clientUrl))
                    {
                        dic_Sockets.Remove(clientUrl);
                    }
                    Console.WriteLine($"{DateTime.Now.ToString()}|服务器：【收到】来客户端网页：{clientUrl}断开WebSocket连接！");
                };

                socket.OnMessage = message =>
                {
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    Console.WriteLine(DateTime.Now.ToString() + "|服务器：【收到】来客户端网页："+ clientUrl+"的信息：\n"+ message);
                };


            });

            Console.ReadKey();
            foreach (var item in dic_Sockets.Values)
            {
                if (item.IsAvailable == true)
                {
                    item.Send($"服务器消息：{DateTime.Now.ToString()}");
                }
            }
            Console.ReadKey();

            foreach (var item in dic_Sockets.Values)
            {
                if (item != null)
                {
                    item.Close();
                }
            }
            Console.ReadKey();
        }

    }


    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
