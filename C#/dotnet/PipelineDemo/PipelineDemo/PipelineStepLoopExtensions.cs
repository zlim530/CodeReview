using System.Collections.Generic;

namespace PipelineDemo
{
    public static class PipelineStepLoopExtensions
    {
        public static IEnumerable<OUTPUT> Step<INPUT, OUTPUT>(this IEnumerable<INPUT> input,
            IPipelineStep<INPUT, OUTPUT> step)
        {
            LoopStep<INPUT, OUTPUT> loopDecorator = new LoopStep<INPUT, OUTPUT>(step);
            return loopDecorator.Process(input);
        }
    }
}