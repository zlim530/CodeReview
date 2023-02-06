using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleDemo
{
	[Table("T_Houses")]
	public class House
	{
		public long Id { get; set; }

		[MaxLength(500)]
		public string? Name { get; set; }

		[MaxLength(255)]
		public string? Owner { get; set; }

		public byte[] RowVer { get; set; }
	}
}