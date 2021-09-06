namespace PipelineDemo
{
    public class ThisStepIsOptional : IPipelineStep<double, double>
    {
        public double Process(double input)
        {
            return input * 10;
        }
    }
}