namespace MailService
{
	public interface IMailProvider
	{
		public void Send(string title, string to, string body);
	}
}