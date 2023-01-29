using ConfigService;
using LogService;

namespace MailService
{
	public class MailProvider : IMailProvider
	{
		private readonly ILogProvider log;
		//private readonly IConfigProvider config;
		private readonly IConfigReader config;

        //public MailProvider(ILogProvider log, IConfigProvider config)
        public MailProvider(ILogProvider log, IConfigReader config)
        {
			this.log = log;
			this.config = config;
		}

        public void Send(string title, string to, string body)
        {
			log.LogInfo("Ready to send mail");
			var smtpServer = config.GetValue("SmtpServer");
			var userName = config.GetValue("UserName");
			var password = config.GetValue("Password");
			Console.WriteLine($"Send Mail server info:{smtpServer}, {userName}, {password}");
            Console.WriteLine($"Send mail:to {to} with {title} as title");
			log.LogInfo("Mail send done");
        }
    }
}