namespace LogService
{
	public interface ILogProvider
	{
		public void LogError(string msg);
		public void LogInfo(string msg);
	}
}