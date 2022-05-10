﻿using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DF.RealEstate.Addresses.Cities;
using DF.RealEstate.Addresses.Cities.Dto;
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
using Slugify;

namespace DF.RealEstate.Entities.Addresses
{
    public class CityAppService : RealEstateAppServiceBase, ICityAppService
    {
        private readonly IRepository<City> _cityRepository;

        public CityAppService(IRepository<City> cityRepository)
        {
            _cityRepository = cityRepository;
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

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteCity(EntityDto input)
        {
            return _cityRepository.DeleteAsync(input.Id);
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
        public Task CreateOrEditCity(CreateOrUpdateCityDto input)
        {
            if (input.Id.HasValue)
                return _cityRepository.UpdateAsync(ObjectMapper.Map<City>(input));
            return _cityRepository.InsertAsync(ObjectMapper.Map<City>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration)]
        [SwaggerHidden]
        public async Task<List<IdTitleDto>> GetCityDropdown(int provinceId)
        {
            var data = await _cityRepository
                .GetAll()
                .Where(x => x.ProvinceId == provinceId)
                .OrderBy(l => l.Id).ToListAsync();
            var mappedData = ObjectMapper.Map<List<CityListDto>>(data);
            return mappedData.Select(x => new IdTitleDto
            {
                Id = x.Id,
                Title = x.Name,
            }).ToList();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public async Task CreateOrEdit(CreateOrUpdateCityDto input)
        {
            if (!input.Id.HasValue)
            {
                 await CreateAsync(input);
            }
            else
            {
                 await UpdateAsync(input);
            }

        }
        private async Task CreateAsync(CreateOrUpdateCityDto input)
        {


            var obj = ObjectMapper.Map<City>(input);
            await _cityRepository.InsertAndGetIdAsync(obj);

            //slug
            SlugHelper helper = new SlugHelper();
            var slug = helper.GenerateSlug(input.Name).Replace(".", "");
            var possibleDuplicate = _cityRepository.GetAll().Where(x => x.Id != obj.Id).FirstOrDefault(a => a.Slug == slug);
            obj.Slug = possibleDuplicate == null ? slug : helper.GenerateSlug(input.Name + " " + obj.Id);
            //endslug
        }
        private async Task UpdateAsync(CreateOrUpdateCityDto input)
        {
            var res = await _cityRepository.GetAllIncluding(x => x.Districts).FirstOrDefaultAsync(x => x.Id == input.Id);

            //slug
            SlugHelper helper = new SlugHelper();
            var slug = helper.GenerateSlug(input.Name).Replace(".", "");
            var possibleDuplicate = _cityRepository.GetAll().Where(x => x.Id != res.Id).FirstOrDefault(a => a.Slug == slug);
            input.Slug = possibleDuplicate == null ? slug : helper.GenerateSlug(input.Name + " " + res.Id);
            //endslug
            res.Districts.Clear();
            ObjectMapper.Map(input, res);

        }
    }
}
