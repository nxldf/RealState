using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using DF.RealEstate.Authorization;
using DF.RealEstate.Homes.HomePhotos.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Text;
using System.Threading.Tasks;
using DF.RealEstate.Homes.HomePhotos;

namespace DF.RealEstate.Entities.Homes
{
    public class HomePhotoAppService : RealEstateAppServiceBase , IHomePhotoAppService
    {
        private readonly IRepository<HomePhoto> _homePhotoRepository;

        public HomePhotoAppService(IRepository<HomePhoto> homePhotoRepository)
        {
            _homePhotoRepository = homePhotoRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes)]
        [SwaggerHidden]
        public async Task<PagedResultDto<HomePhotoDto>> GetAll(GetHomePhotosInput input)
        {
            var query = _homePhotoRepository.GetAll().Where(x => x.HomeId == input.HomeId);

            var cnt = await query.CountAsync();
            var data = await query
                    .OrderBy(input.Sorting)
                    .PageBy(input)
                    .ToListAsync();
            var mappedData = ObjectMapper.Map<List<HomePhotoDto>>(data);


            return new PagedResultDto<HomePhotoDto>(cnt, mappedData);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_Delete)]
        [SwaggerHidden]
        public async Task Delete(EntityDto<int> input)
        {
            var res = await _homePhotoRepository.FirstOrDefaultAsync(x => x.Id == input.Id);
            await _homePhotoRepository.DeleteAsync(res);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        [SwaggerHidden]
        public async Task CreateOrEdit(CreateOrEditHomePhotoDto input)
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

        private async Task CreateAsync(CreateOrEditHomePhotoDto input)
        {
            var obj = ObjectMapper.Map<HomePhoto>(input);
            await _homePhotoRepository.InsertAsync(obj);
        }

        private async Task UpdateAsync(CreateOrEditHomePhotoDto input)
        {
            var user = await GetCurrentUserAsync();

            var res = await _homePhotoRepository.GetAll()
                .FirstOrDefaultAsync(x => x.Id == input.Id);

            ObjectMapper.Map(input, res);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        [SwaggerHidden]
        public async Task<HomePhotoDto> GetForEdit(NullableIdDto<int> input)
        {
            if (input.Id.HasValue)
            {
                var res = await _homePhotoRepository.GetAll().FirstOrDefaultAsync(x => x.Id == input.Id);
                return ObjectMapper.Map<HomePhotoDto>(res);
            }

            return new HomePhotoDto();
        }

    }
}
