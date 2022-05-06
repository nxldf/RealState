using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DF.RealEstate.Addresses.Countries;
using DF.RealEstate.Addresses.Countries.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using Abp.Authorization;
using DF.RealEstate.Authorization;
using DF.RealEstate.Dto;

namespace DF.RealEstate.Entities.Addresses
{
    public class CountryAppService : RealEstateAppServiceBase , ICountryAppService
    {
        private readonly IRepository<Country> _countryRepository;
        public CountryAppService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public async Task<PagedResultDto<CountryListDto>> GetAllCountry(GetAllCountryInput input)
        {
            var query = _countryRepository.GetAll().
                WhereIf(!string.IsNullOrEmpty(input.Filter), p => p.Name.Contains(input.Filter));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<CountryListDto>>(data);

            return new PagedResultDto<CountryListDto>(cnt, mappedData);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteCountry(EntityDto input)
        {
            return _countryRepository.DeleteAsync(input.Id);
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public async Task<GetForEditCountryDto> GetCountryForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var output = await _countryRepository.GetAsync(input.Id.Value);
                return ObjectMapper.Map<GetForEditCountryDto>(output);
            }
            return new GetForEditCountryDto();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public Task CreateOrEditCountry(CreateOrUpdateCountryDto input)
        {
            if (input.Id.HasValue)
                return _countryRepository.UpdateAsync(ObjectMapper.Map<Country>(input));
            return _countryRepository.InsertAsync(ObjectMapper.Map<Country>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration)]
        [SwaggerHidden]
        public async Task<List<IdTitleDto>> GetCountryDropdown()
        {
            var data = await _countryRepository
                .GetAll()
                .OrderBy(l => l.Id).ToListAsync();
            var mappedData = ObjectMapper.Map<List<CountryListDto>>(data);
            return mappedData.Select(x => new IdTitleDto
            {
                Id = x.Id,
                Title = x.Name,
            }).ToList();
        }
    }
}
