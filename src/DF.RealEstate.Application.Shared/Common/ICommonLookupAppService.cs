using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DF.RealEstate.Common.Dto;
using DF.RealEstate.Editions.Dto;

namespace DF.RealEstate.Common
{
    public interface ICommonLookupAppService : IApplicationService
    {
        Task<ListResultDto<SubscribableEditionComboboxItemDto>> GetEditionsForCombobox(bool onlyFreeItems = false);

        Task<PagedResultDto<NameValueDto>> FindUsers(FindUsersInput input);

        GetDefaultEditionNameOutput GetDefaultEditionName();
    }
}