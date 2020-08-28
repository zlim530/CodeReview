using System.ComponentModel.DataAnnotations;

namespace LearningAbpDemo.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}