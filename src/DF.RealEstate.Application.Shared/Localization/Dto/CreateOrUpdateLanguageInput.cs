using System.ComponentModel.DataAnnotations;

namespace DF.RealEstate.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}