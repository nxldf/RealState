using Abp.Application.Services.Dto;
using DF.RealEstate.Entities.Addresses.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Addresses
{
    public interface IAddressAppService
    {
        Task<PagedResultDto<CountryListDto>> GetAllCountry(GetAllCountryInput input);
        Task<PagedResultDto<ProvinceListDto>> GetAllProvince(GetAllProvinceInput input);
        Task<PagedResultDto<CityListDto>> GetAllCity(GetAllCityInput input);
        Task<PagedResultDto<DistrictListDto>> GetAllDistrict(GetAllDistrictInput input);
        Task DeleteCountry(EntityDto input);
        Task DeleteProvince(EntityDto input);
        Task DeleteCity(EntityDto input);
        Task DeleteDistrict(EntityDto input);
        Task<GetForEditCountryDto> GetCountryForEdit(NullableIdDto input);
        Task<GetForEditProvinceDto> GetProvinceForEdit(NullableIdDto input);
        Task<GetForEditCityDto> GetCityForEdit(NullableIdDto input);
        Task<GetForEditDistrictDto> GetDistrictForEdit(NullableIdDto input);
        Task CreateOrEditCountry(CreateOrUpdateCountryDto input);
        Task CreateOrEditCity(CreateOrUpdateCityDto input);
        Task CreateOrEditProvince(CreateOrUpdateProvinceDto input);
        Task CreateOrEditDistrict(CreateOrUpdateDistrictDto input);

    }
}
