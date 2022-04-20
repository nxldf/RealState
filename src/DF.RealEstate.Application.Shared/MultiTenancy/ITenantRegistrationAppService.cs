using System.Threading.Tasks;
using Abp.Application.Services;
using DF.RealEstate.Editions.Dto;
using DF.RealEstate.MultiTenancy.Dto;

namespace DF.RealEstate.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}