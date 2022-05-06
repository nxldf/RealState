using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Cities.Dto;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Addresses.Cities
{
    public interface ICityAppService
    {
        Task<PagedResultDto<CityListDto>> GetAllCity(GetAllCityInput input);
        Task DeleteCity(EntityDto input);
        Task<GetForEditCityDto> GetCityForEdit(NullableIdDto input);
        Task CreateOrEditCity(CreateOrUpdateCityDto input);
        Task<List<IdTitleDto>> GetCityDropdown(int provinceId);
    }
}
