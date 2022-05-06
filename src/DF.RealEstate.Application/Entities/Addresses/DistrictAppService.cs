using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using DF.RealEstate.Addresses.Districts;
using DF.RealEstate.Addresses.Districts.Dto;
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
    public class DistrictAppService : RealEstateAppServiceBase, IDistrictAppService
    {
        private readonly IRepository<District> _districtRepository;

        public DistrictAppService(IRepository<District>  districtRepository)
        {
            _districtRepository = districtRepository;
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
        public Task DeleteDistrict(EntityDto input)
        {
            return _districtRepository.DeleteAsync(input.Id);
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
        public Task CreateOrEditDistrict(CreateOrUpdateDistrictDto input)
        {
            if (input.Id.HasValue)
                return _districtRepository.UpdateAsync(ObjectMapper.Map<District>(input));
            return _districtRepository.InsertAsync(ObjectMapper.Map<District>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration)]
        [SwaggerHidden]
        public async Task<List<IdTitleDto>> GetDistrictDropdown(int cityId)
        {
            var data = await _districtRepository
                .GetAll()
                .Where(x => x.CityId == cityId)
                .OrderBy(l => l.Id).ToListAsync();
            var mappedData = ObjectMapper.Map<List<DistrictListDto>>(data);
            return mappedData.Select(x => new IdTitleDto
            {
                Id = x.Id,
                Title = x.Name,
            }).ToList();
        }
    }
}
