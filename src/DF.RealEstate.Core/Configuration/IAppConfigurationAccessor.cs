using Microsoft.Extensions.Configuration;

namespace DF.RealEstate.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
