﻿using System;

namespace PipelineDemo
{
    // 支持事件监听的具体功能类
    public class EventStep<INPUT, OUTPUT> : IPipelineStep<INPUT, OUTPUT>
    {
        public event Action<INPUT> OnInput;
        public event Action<OUTPUT> OnOutput;

        private readonly IPipelineStep<INPUT, OUTPUT> _innerStep;

        public EventStep(IPipelineStep<INPUT, OUTPUT> innerStep)
        {
            _innerStep = innerStep;
        }
        
        public OUTPUT Process(INPUT input)
        {
            OnInput?.Invoke(input);
            var output = _innerStep.Process(input);
            OnOutput?.Invoke(output);
            return output;
        }
    }
}