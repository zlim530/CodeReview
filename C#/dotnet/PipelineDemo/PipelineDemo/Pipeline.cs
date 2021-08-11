using System;

namespace PipelineDemo
{
    // 修改 Pipeline<INPUT, OUTPUT> 类，使其继承 IPipelineStep<INPUT, OUTPUT> 接口
    public abstract class Pipeline<INPUT, OUTPUT> : IPipelineStep<INPUT, OUTPUT>
    {
        public Func<INPUT, OUTPUT> PipelineSteps { get; protected set; }

        public OUTPUT Process(INPUT input)
        {
            return PipelineSteps(input);
        }
    }
}