using Abp.Extensions;

namespace DF.RealEstate.Authentication
{
    public class TwitterExternalLoginProviderSettings
    {
        public string ConsumerKey { get; set; }
        public string ConsumerSecret { get; set; }
        
        public bool IsValid()
        {
            return !ConsumerKey.IsNullOrWhiteSpace() && !ConsumerSecret.IsNullOrWhiteSpace();
        }
    }
}
 
