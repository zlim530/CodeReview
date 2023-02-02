using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleDemo
{
	[Table("T_Users")]
	public class User
	{
		public long Id { get; set; }

		[MaxLength(255)]
		[Required]
		public string Name { get; set; }
	}
}