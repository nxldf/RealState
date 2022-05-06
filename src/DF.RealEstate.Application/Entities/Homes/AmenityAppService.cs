using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using DF.RealEstate.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using DF.RealEstate.Homes.Amenities.Dto;
using DF.RealEstate.Homes.Amenities;

namespace DF.RealEstate.Entities.Homes
{
    public class AmenityAppService : RealEstateAppServiceBase, IAmenityAppService
    {
        private readonly IRepository<Amenity> _amenityRepository;

        public AmenityAppService(IRepository<Amenity> amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Amenities)]
        [SwaggerHidden]
        public async Task<PagedResultDto<AmenityListDto>> GetAll(GetAllAminitiesInput input)
        {
            var query = _amenityRepository.GetAllIncluding(x => x.Translations)
                      .WhereIf(!string.IsNullOrEmpty(input.Filter),
                    x => x.Translations.Any(a => a.Name.Contains(input.Filter)));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<AmenityListDto>>(data);

            return new PagedResultDto<AmenityListDto>(cnt, mappedData);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Amenities_CreateOrEdit)]
        [SwaggerHidden]
        public async Task CreateOrEdit(CreateOrEditAmenityDto input)
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

        private async Task CreateAsync(CreateOrEditAmenityDto input)
        {
            var obj = ObjectMapper.Map<Amenity>(input);
            await _amenityRepository.InsertAsync(obj);
        }

        private async Task UpdateAsync(CreateOrEditAmenityDto input)
        {
            var user = await GetCurrentUserAsync();

            var res = await _amenityRepository.GetAllIncluding(p => p.Translations)
                .FirstOrDefaultAsync(x => x.Id == input.Id);

            res.Translations.Clear();

            ObjectMapper.Map(input, res);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Amenities_Delete)]
        [SwaggerHidden]
        public async Task Delete(EntityDto input)
        {
            var res = await _amenityRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            await _amenityRepository.DeleteAsync(res);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Amenities)]
        [SwaggerHidden]
        public async Task<GetForEditAmenityDto> GetForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var res = await _amenityRepository.GetAllIncluding(x => x.Translations).FirstOrDefaultAsync(x => x.Id == input.Id);
                return ObjectMapper.Map<GetForEditAmenityDto>(res);
            }

            return new GetForEditAmenityDto();
        }
    }
}
