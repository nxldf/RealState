using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DF.RealEstate.Caching.Dto;

namespace DF.RealEstate.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultDto<CacheDto> GetAllCaches();

        Task ClearCache(EntityDto<string> input);

        Task ClearAllCaches();
    }
}
