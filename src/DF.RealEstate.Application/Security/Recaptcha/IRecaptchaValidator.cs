using System.Threading.Tasks;

namespace DF.RealEstate.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}