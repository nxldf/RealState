using System.ComponentModel.DataAnnotations;

namespace DF.RealEstate.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
