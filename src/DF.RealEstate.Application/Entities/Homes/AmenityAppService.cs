using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using DF.RealEstate.Authorization;
using DF.RealEstate.Entities.Homes.Aminities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DF.RealEstate.Entities.Homes
{
    public class AmenityAppService : RealEstateAppServiceBase
    {
        private readonly IRepository<Amenity> _amenityRepository;

        public AmenityAppService(IRepository<Amenity> amenityRepository)
        {
            _amenityRepository = amenityRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Addresses)]
        //[SwaggerHidden]
        public async Task<PagedResultDto<AminityListDto>> GetAll(GetAllAminitiesInput input)
        {
            var query = _amenityRepository.GetAllIncluding(x => x.Translations)
                      .WhereIf(!string.IsNullOrEmpty(input.Filter),
                    x => x.Translations.Any(a => a.Name.Contains(input.Filter)));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<AminityListDto>>(data);

            return new PagedResultDto<AminityListDto>(cnt, mappedData);
        }
    }
}
