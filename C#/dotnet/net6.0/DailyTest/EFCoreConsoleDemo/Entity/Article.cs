using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleDemo
{
	[Table("T_Articles")]
	public class Article
    {
		public long Id { get; set; }

		[Required]
		[MaxLength(255)]
		[Unicode]
		public string Title { get; set; }

		[Required]
		[MaxLength(5000)]
		[Unicode]
		public string Content { get; set; }

		/// <summary>
		/// 一对多关系配置
		/// </summary>
		public List<Comment> Comments { get; set; } = new List<Comment>();
	}
}