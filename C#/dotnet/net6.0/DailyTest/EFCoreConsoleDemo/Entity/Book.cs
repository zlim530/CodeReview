namespace EFCoreConsoleDemo
{
	public class Book
	{
		/// <summary>
		/// 主键
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 发布日期
		/// </summary>
		public DateTime PubTime { get; set; }

		/// <summary>
		/// 单价
		/// </summary>
		public double Price { get; set; }

		/// <summary>
		/// 作者
		/// </summary>
		public string AuthorName { get; set; }

		/// <summary>
		/// 是否删除
		/// </summary>
		public bool IsDeleted { get; set; }

	}
}