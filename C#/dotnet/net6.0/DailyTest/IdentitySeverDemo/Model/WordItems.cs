using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentitySeverDemo.Model;

[Table("T_WordItems")]
public class WordItems
{
    public long Id { get; set; }

    [MaxLength(255)]
    [Required]
    public string Word { get; set; }

    [MaxLength(255)]
    public string? Phonetic { get; set; }

    public string? Definition { get; set; }

    public string? Translation { get; set; }
}