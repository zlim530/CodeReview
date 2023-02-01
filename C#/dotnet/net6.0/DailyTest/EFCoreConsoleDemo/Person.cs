namespace EFCoreConsoleDemo
{
	public class Person
	{
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }
		
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 薪资
		/// </summary>
		public double? Salary { get; set; }

		/// <summary>
		/// 生日
		/// </summary>
		public DateTime BirthDay { get; set; }
	}
}