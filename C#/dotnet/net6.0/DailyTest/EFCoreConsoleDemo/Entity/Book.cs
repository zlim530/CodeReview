namespace EFCoreConsoleDemo
{
	public class Book
	{
		/// <summary>
		/// ����
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// ��������
		/// </summary>
		public DateTime PubTime { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public double Price { get; set; }

		/// <summary>
		/// ����
		/// </summary>
		public string AuthorName { get; set; }

		/// <summary>
		/// �Ƿ�ɾ��
		/// </summary>
		public bool IsDeleted { get; set; }

	}
}