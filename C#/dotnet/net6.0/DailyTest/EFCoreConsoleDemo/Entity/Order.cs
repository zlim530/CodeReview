using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace EFCoreConsoleDemo
{
	[Table("T_Orders")]
	public class Order
	{
		public long Id { get; set; }

		[Required]
		[MaxLength(50)]
		public string Name { get; set; }

		[AllowNull]
		public string? Address { get; set; }

		public Delivery? Delivery { get; set; }
	}
}