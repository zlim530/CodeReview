using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCoreConsoleDemo
{
    [Table("T_Teachers")]
    public class Teacher
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();
    }
}