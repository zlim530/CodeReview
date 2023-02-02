namespace EFCoreConsoleDemo
{
	public class Leave
	{

		public long Id { get; set; }

		public User? Requester { get; set; }

		public User? Approver { get; set; }

		public string Remarks { get; set; }
	}
}