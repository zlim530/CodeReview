using System.ComponentModel.DataAnnotations;

namespace YSR.MES.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}