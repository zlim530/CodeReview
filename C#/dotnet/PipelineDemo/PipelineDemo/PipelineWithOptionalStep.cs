namespace PipelineDemo
{
    public class PipelineWithOptionalStep : Pipeline<double, double>
    {
        public PipelineWithOptionalStep()
        {
            PipelineSteps = input =>
                input.Step(new OptionalStep<double, double>(i => i > 1024, new ThisStepIsOptional()));
        }
    }
}