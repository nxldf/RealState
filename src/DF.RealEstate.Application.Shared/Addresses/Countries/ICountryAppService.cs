using Abp.Application.Services.Dto;
using DF.RealEstate.Addresses.Countries.Dto;
using DF.RealEstate.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Addresses.Countries
{
    public interface ICountryAppService
    {
        Task<PagedResultDto<CountryListDto>> GetAllCountry(GetAllCountryInput input);
        Task DeleteCountry(EntityDto input);
        Task<GetForEditCountryDto> GetCountryForEdit(NullableIdDto input);
        Task CreateOrEditCountry(CreateOrUpdateCountryDto input);
        Task<List<IdTitleDto>> GetCountryDropdown();
    }
}
