namespace PipelineDemo
{
    public static class PipelineStepExtensions
    {
        public static OUTPUT Step<INPUT, OUTPUT>(this INPUT input, IPipelineStep<INPUT, OUTPUT> step)
        {
            return step.Process(input);
        }
    }
}