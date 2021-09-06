using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace PipelineDemo
{
    public class DoubleStep : IPipelineStep<int, int>
    {
        public int Process(int input)
        {
            return input * input;
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");
            double input = 1024.1024;

            #region 基础实现
            // 基础实现
            // string result = input.Step(new DoubleToIntStep())
            //                      .Step(new IntToStringStep());
            // Console.WriteLine(result);
            #endregion

            #region 依赖注入
            // 依赖注入
            // 需要安装 Microsoft.Extensions.DependencyInjection
            // var services = new ServiceCollection();
            // services.AddTransient<TrivalPipeline>();
            // var provider = services.BuildServiceProvider();
            // var trival = provider.GetService<TrivalPipeline>();
            // string result = trival.Process(input);
            // Console.WriteLine(result);
            #endregion

            #region 条件式组装
            // 条件式组装
            // PipelineWithOptionalStep step = new PipelineWithOptionalStep();
            // Console.WriteLine(step.Process(1024.1024));
            // Console.WriteLine(step.Process(520.520));
            #endregion

            #region 事件监听
            // var input = 10;
            // Console.WriteLine($"Input Value:{input}[{input.GetType()}]");
            // var pipeline = new EventStep<int, int>(new DoubleStep());
            // pipeline.OnInput += i => Console.WriteLine($"Input Value:{i}");
            // pipeline.OnOutput += o => Console.WriteLine($"Output Value:{o}");
            // var output = pipeline.Process(input);
            // Console.WriteLine($"Output Value: {output} [{output.GetType()}]");
            // Console.WriteLine("\r\n");
            //
            // Console.WriteLine(10.Step(new DoubleStep(), i =>
            // {
            //     Console.WriteLine($"Input Value:{i}");
            // },
            // o =>
            // {
            //     Console.WriteLine($"Output Value:{o}");
            // }));
            #endregion

            #region 可迭代执行
            // 可迭代执行是指当我们的管道中注册了多个功能模块时，不是一次性执行完所有的功能模块，而是每次只执行一个
            // 功能，后续功能会在下次执行该管道对应的代码块时接着执行，直到该管道中所有的功能模块执行完毕为止。
            // 该特性主要是通过 yield return 来实现
            var list = Enumerable.Range(0, 10);
            foreach (var item in list.Step(new DoubleStep()))
            {
                Console.WriteLine(item);
            }
            #endregion

        }
    }
}