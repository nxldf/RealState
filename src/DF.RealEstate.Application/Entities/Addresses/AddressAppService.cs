using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using DF.RealEstate.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using System.Linq.Dynamic;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using DF.RealEstate.Entities.Addresses.Dto;

namespace DF.RealEstate.Entities.Addresses
{
    public class AddressAppService : RealEstateAppServiceBase, IAddressAppService
    {
        private readonly IRepository<Country> _countryRepository;
        private readonly IRepository<Province> _provinceRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IRepository<District> _districtRepository;

        public AddressAppService(IRepository<Country> countryRepository,
            IRepository<Province> provinceRepository,
            IRepository<City> cityRepository,
            IRepository<District> districtRepository)
        {
            _countryRepository = countryRepository;
            _provinceRepository = provinceRepository;
            _cityRepository = cityRepository;
            _districtRepository = districtRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses)]
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

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses)]
        [SwaggerHidden]
        public async Task<PagedResultDto<ProvinceListDto>> GetAllProvince(GetAllProvinceInput input)
        {
            var query = _provinceRepository.GetAll().
                WhereIf(input.CountryId.HasValue, x => x.CountryId == input.CountryId).
                WhereIf(!string.IsNullOrEmpty(input.Filter), p => p.Name.Contains(input.Filter));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<ProvinceListDto>>(data);

            return new PagedResultDto<ProvinceListDto>(cnt, mappedData);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses)]
        [SwaggerHidden]
        public async Task<PagedResultDto<CityListDto>> GetAllCity(GetAllCityInput input)
        {
            var query = _cityRepository.GetAll().
                WhereIf(input.ProvinceId.HasValue, x => x.ProvinceId == input.ProvinceId).
                WhereIf(!string.IsNullOrEmpty(input.Filter), p => p.Name.Contains(input.Filter));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<CityListDto>>(data);

            return new PagedResultDto<CityListDto>(cnt, mappedData);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses)]
        [SwaggerHidden]
        public async Task<PagedResultDto<DistrictListDto>> GetAllDistrict(GetAllDistrictInput input)
        {
            var query = _districtRepository.GetAll().
                WhereIf(input.CityId.HasValue, x => x.CityId == input.CityId).
                WhereIf(!string.IsNullOrEmpty(input.Filter), p => p.Name.Contains(input.Filter));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<DistrictListDto>>(data);

            return new PagedResultDto<DistrictListDto>(cnt, mappedData);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteCountry(EntityDto input)
        {
            return _countryRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteProvince(EntityDto input)
        {
            return _provinceRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteCity(EntityDto input)
        {
            return _cityRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteDistrict(EntityDto input)
        {
            return _districtRepository.DeleteAsync(input.Id);
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
        public async Task<GetForEditProvinceDto> GetProvinceForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var output = await _provinceRepository.GetAsync(input.Id.Value);
                return ObjectMapper.Map<GetForEditProvinceDto>(output);
            }
            return new GetForEditProvinceDto();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public async Task<GetForEditCityDto> GetCityForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var output = await _cityRepository.GetAsync(input.Id.Value);
                return ObjectMapper.Map<GetForEditCityDto>(output);
            }
            return new GetForEditCityDto();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public async Task<GetForEditDistrictDto> GetDistrictForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var output = await _districtRepository.GetAsync(input.Id.Value);
                return ObjectMapper.Map<GetForEditDistrictDto>(output);
            }
            return new GetForEditDistrictDto();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public Task CreateOrEditCountry(CreateOrUpdateCountryDto input)
        {
            if (input.Id.HasValue)
                return _countryRepository.UpdateAsync(ObjectMapper.Map<Country>(input));
            return _countryRepository.InsertAsync(ObjectMapper.Map<Country>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public Task CreateOrEditCity(CreateOrUpdateCityDto input)
        {
            if (input.Id.HasValue)
                return _cityRepository.UpdateAsync(ObjectMapper.Map<City>(input));
            return _cityRepository.InsertAsync(ObjectMapper.Map<City>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public Task CreateOrEditProvince(CreateOrUpdateProvinceDto input)
        {
            if (input.Id.HasValue)
                return _provinceRepository.UpdateAsync(ObjectMapper.Map<Province>(input));
            return _provinceRepository.InsertAsync(ObjectMapper.Map<Province>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public Task CreateOrEditDistrict(CreateOrUpdateDistrictDto input)
        {
            if (input.Id.HasValue)
                return _districtRepository.UpdateAsync(ObjectMapper.Map<District>(input));
            return _districtRepository.InsertAsync(ObjectMapper.Map<District>(input));
        }


    }
}
