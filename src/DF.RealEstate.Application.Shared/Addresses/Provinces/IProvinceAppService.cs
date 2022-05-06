using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Provinces.Dto;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Addresses.Provinces
{
    public interface IProvinceAppService
    {
        Task<PagedResultDto<ProvinceListDto>> GetAllProvince(GetAllProvinceInput input);
        Task DeleteProvince(EntityDto input);
        Task<GetForEditProvinceDto> GetProvinceForEdit(NullableIdDto input);
        Task CreateOrEditProvince(CreateOrUpdateProvinceDto input);
        Task<List<IdTitleDto>> GetProvinceDropdown(int countryId);
    }
}
