using System.Threading.Tasks;
using Abp.Application.Services;
using DF.RealEstate.Configuration.Host.Dto;

namespace DF.RealEstate.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
