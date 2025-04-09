using System.ComponentModel.DataAnnotations;

namespace Global.Project.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}