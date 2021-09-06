using System.Collections;
using System.Collections.Generic;

namespace PipelineDemo
{
    public class LoopStep<INPUT, OUTPUT> : IPipelineStep<IEnumerable<INPUT>, IEnumerable<OUTPUT>>
    {
        private readonly IPipelineStep<INPUT, OUTPUT> _internalStep;

        public LoopStep(IPipelineStep<INPUT, OUTPUT> internalStep)
        {
            _internalStep = internalStep;
        }

        public IEnumerable<OUTPUT> Process(IEnumerable<INPUT> input)
        {
            foreach (var item in input)
            {
                yield return _internalStep.Process(item);
            }

            // 等价于下述代码段：
            // return from INPUT item in input
            //     select _internalStep.Process(item);
        }
    }
}