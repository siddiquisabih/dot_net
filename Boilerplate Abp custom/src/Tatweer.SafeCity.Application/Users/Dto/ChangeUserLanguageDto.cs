using System.ComponentModel.DataAnnotations;

namespace Tatweer.SafeCity.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}