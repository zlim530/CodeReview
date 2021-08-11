using System;

namespace PipelineDemo
{
    public class DoubleToIntStep : IPipelineStep<double, int>
    {
        public int Process(double input)
        {
            return Convert.ToInt32(input);
        }
    }
}