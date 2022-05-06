using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using DF.RealEstate.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Linq.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic;
using System.Linq.Dynamic.Core;
using DF.RealEstate.Homes.Dto;
using DF.RealEstate.Homes;

namespace DF.RealEstate.Entities.Homes
{
    public class HomeAppService : RealEstateAppServiceBase, IHomeAppService
    {
        private readonly IRepository<Home, long> _homeRepository;

        public HomeAppService(IRepository<Home, long> homeRepository)
        {
            _homeRepository = homeRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes)]
        //[SwaggerHidden]
        public async Task<PagedResultDto<HomeListDto>> GetAll(GetAllHomeInput input)
        {
            var query = _homeRepository.GetAll().Include(p=>p.District).
                ThenInclude(c=>c.City).ThenInclude(x=>x.Province).ThenInclude(y=>y.Country).
                WhereIf(!string.IsNullOrEmpty(input.Filter), p => p.Name.Contains(input.Filter)).
                WhereIf(!string.IsNullOrEmpty(input.Filter), p => p.ZipCode.Contains(input.Filter));

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<HomeListDto>>(data);

            return new PagedResultDto<HomeListDto>(cnt, mappedData);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_Delete)]
        //[SwaggerHidden]
        public Task DeleteCountry(EntityDto<long> input)
        {
            return _homeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        //[SwaggerHidden]
        public async Task<GetForEditHomeDto> GetHomeForEdit(NullableIdDto<long> input)
        {
            if (input.Id.HasValue)
            {
                var output =  _homeRepository.GetAll().Include(n => n.District)
                     .ThenInclude(p => p.City).ThenInclude(x => x.Province).ThenInclude(y => y.Country)
                     .Where(n=>n.Id == input.Id.Value).FirstOrDefault();
                return ObjectMapper.Map<GetForEditHomeDto>(output);


            }
            return new GetForEditHomeDto();
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        //[SwaggerHidden]
        public Task CreateOrEditHome(CreateOrUpdateHomeDto input)
        {
            if (input.Id.HasValue)
                return _homeRepository.UpdateAsync(ObjectMapper.Map<Home>(input));
            return _homeRepository.InsertAsync(ObjectMapper.Map<Home>(input));
        }
    }
}
