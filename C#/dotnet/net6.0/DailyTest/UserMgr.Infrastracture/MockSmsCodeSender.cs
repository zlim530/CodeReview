using Microsoft.Extensions.Logging;
using UserMgr.Domain.Interfaces;
using UserMgr.Domain.ValueObjects;

namespace UserMgr.Infrastracture
{
    public class MockSmsCodeSender : ISmsCodeSender
    {
        private readonly ILogger<MockSmsCodeSender> logger;

        public MockSmsCodeSender(ILogger<MockSmsCodeSender> logger)
        {
            this.logger = logger;
        }

        public Task SendCodeAsync(PhoneNumber phoneNumber, string code)
        {
            // 在这里使用模拟类进行测试：真实的短信发送服务可以使用领域事件+微服务实现
            logger.LogInformation($"向{phoneNumber.RegionCode}-{phoneNumber.Number}发送验证码：{code}");
            Console.WriteLine($"向{phoneNumber.RegionCode}-{phoneNumber.Number}发送验证码：{code}");
            return Task.CompletedTask;
        }
    }
}
