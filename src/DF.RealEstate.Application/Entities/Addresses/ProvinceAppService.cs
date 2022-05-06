using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DF.RealEstate.Addresses.Provinces;
using DF.RealEstate.Addresses.Provinces.Dto;
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
    public class ProvinceAppService : RealEstateAppServiceBase, IProvinceAppService
    {
        private readonly IRepository<Province> _provinceRepository;

        public ProvinceAppService(IRepository<Province> provinceRepository)
        {
            _provinceRepository = provinceRepository;
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

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_Delete)]
        [SwaggerHidden]
        public Task DeleteProvince(EntityDto input)
        {
            return _provinceRepository.DeleteAsync(input.Id);
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
        public Task CreateOrEditProvince(CreateOrUpdateProvinceDto input)
        {
            if (input.Id.HasValue)
                return _provinceRepository.UpdateAsync(ObjectMapper.Map<Province>(input));
            return _provinceRepository.InsertAsync(ObjectMapper.Map<Province>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses_CreateOrEdit)]
        [SwaggerHidden]
        public async Task<List<IdTitleDto>> GetProvinceDropdown(int countryId)
        {
            var data = await _provinceRepository
                .GetAll()
                .Where(x => x.CountryId == countryId)
                .OrderBy(l => l.Id).ToListAsync();
            var mappedData = ObjectMapper.Map<List<ProvinceListDto>>(data);
            return mappedData.Select(x => new IdTitleDto
            {
                Id = x.Id,
                Title = x.Name,
            }).ToList();
        }
    }
}
