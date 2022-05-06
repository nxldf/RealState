using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Districts.Dto;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Addresses.Districts
{
    public interface IDistrictAppService
    {
        Task<PagedResultDto<DistrictListDto>> GetAllDistrict(GetAllDistrictInput input);
        Task DeleteDistrict(EntityDto input);
        Task<GetForEditDistrictDto> GetDistrictForEdit(NullableIdDto input);
        Task CreateOrEditDistrict(CreateOrUpdateDistrictDto input);
        Task<List<IdTitleDto>> GetDistrictDropdown(int cityId);
    }
}
