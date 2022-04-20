using System.ComponentModel.DataAnnotations;

namespace DF.RealEstate.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}