using System.Threading.Tasks;
using Abp.Application.Services;
using DF.RealEstate.Configuration.Tenants.Dto;

namespace DF.RealEstate.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearLogo();

        Task ClearCustomCss();
    }
}
