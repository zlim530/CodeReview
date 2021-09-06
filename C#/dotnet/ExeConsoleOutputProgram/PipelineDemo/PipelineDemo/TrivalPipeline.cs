namespace PipelineDemo
{
    public class TrivalPipeline : Pipeline<double, string>
    {
        public TrivalPipeline()
        {
            PipelineSteps = input => input.Step(new DoubleToIntStep())
                                            .Step(new IntToStringStep());
        }
    }
}