using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using DF.RealEstate.Authorization;
using DF.RealEstate.Homes.Advertisements.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Abp.Linq.Extensions;
using DF.RealEstate.Homes.Advertisements;

namespace DF.RealEstate.Entities.Homes
{
    public class AdvertisementAppService : RealEstateAppServiceBase , IAdvertisementAppService
    {
        private readonly IRepository<Advertisement,long> _advertisementRepository;

        public AdvertisementAppService(IRepository<Advertisement,long> advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }


        [AbpAuthorize(AppPermissions.Pages_Administration_Homes)]
        [SwaggerHidden]
        public async Task<PagedResultDto<AdvertisementListDto>> GetAll(GetAllAdvertisementInput input)
        {
            var query = _advertisementRepository.GetAll().
                WhereIf(input.Type.HasValue, x => x.Type == input.Type);

            var cnt = await query.CountAsync();
            var data = await query.OrderBy(input.Sorting).PageBy(input).ToListAsync();
            var mappedData = ObjectMapper.Map<List<AdvertisementListDto>>(data);

            return new PagedResultDto<AdvertisementListDto>(cnt, mappedData);

        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_Delete)]
        [SwaggerHidden]
        public Task Delete(EntityDto input)
        {
            return _advertisementRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        [SwaggerHidden]
        public Task CreateOrEdit(CreateOrEditAdvertisementDto input)
        {
            if (input.Id.HasValue)
                return _advertisementRepository.UpdateAsync(ObjectMapper.Map<Advertisement>(input));
            return _advertisementRepository.InsertAsync(ObjectMapper.Map<Advertisement>(input));
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        [SwaggerHidden]
        public async Task<GetForEditAdvertisementDto> GetForEdit(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var query = await _advertisementRepository.GetAsync(input.Id.Value);
                return ObjectMapper.Map<GetForEditAdvertisementDto>(query);
                    }
            return new GetForEditAdvertisementDto();
        }
    }
}
