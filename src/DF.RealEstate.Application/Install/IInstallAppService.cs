using System.Threading.Tasks;
using Abp.Application.Services;
using DF.RealEstate.Install.Dto;

namespace DF.RealEstate.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}