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
using DF.RealEstate.GeHomesInformations;
using DF.RealEstate.Entities.Addresses;
using DF.RealEstate.Homes.Amenities.Dto;
using DF.RealEstate.Homes.Amenities;
using Abp.UI;

namespace DF.RealEstate.Entities.Homes
{
    public class HomeAppService : RealEstateAppServiceBase, IHomeAppService
    {
        private readonly IRepository<Home, long> _homeRepository;
        private readonly IRepository<Advertisement, long> _advertisementRepository;
        private readonly IRepository<District> _districtRepository;
        private readonly IAmenityAppService _amenityAppService;
        private readonly IGeHomesInformationAppService _geHomesInformation;

        public HomeAppService(IRepository<Home, long> homeRepository,
            IRepository<Advertisement, long> advertisementRepository,
            IRepository<District> districtRepository,
            IAmenityAppService amenityAppService,
            IGeHomesInformationAppService geHomesInformation)
        {
            _homeRepository = homeRepository;
            _advertisementRepository = advertisementRepository;
            _geHomesInformation = geHomesInformation;
            _amenityAppService = amenityAppService;
            _districtRepository = districtRepository;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes)]
        //[SwaggerHidden]
        public async Task<PagedResultDto<HomeListDto>> GetAll(GetAllHomeInput input)
        {
            var query = _homeRepository.GetAll().Include(p => p.District).
                ThenInclude(c => c.City).ThenInclude(x => x.Province).ThenInclude(y => y.Country).
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
        public Task Delete(EntityDto<long> input)
        {
            return _homeRepository.DeleteAsync(input.Id);
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        //[SwaggerHidden]
        public async Task<GetForEditHomeDto> GetHomeForEdit(NullableIdDto<long> input)
        {
            if (input.Id.HasValue)
            {
                var output = _homeRepository.GetAll().Include(n => n.District)
                     .ThenInclude(p => p.City).ThenInclude(x => x.Province).ThenInclude(y => y.Country)
                     .Where(n => n.Id == input.Id.Value).FirstOrDefault();
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

        [AbpAuthorize(AppPermissions.Pages_Administration_Homes_CreateOrEdit)]
        //[SwaggerHidden]
        public async Task<HomeInformation> CreateHomeFromUrl(string Url)
        {
            //Url = "https://www.willhaben.at/iad/immobilien/d/mietwohnungen/steiermark/murtal/wohnung-fohnsdorf-553183281";
            
            HomeInformation HomeInfo = await _geHomesInformation.HomeInformations(Url);

            var district = _districtRepository.GetAll().Where(x => x.Name.Contains(HomeInfo.DistrictName)).FirstOrDefault();
            if (district != null)
                HomeInfo.DistrictId = district.Id;
            else
                HomeInfo.DistrictId = 3;
            //throw new UserFriendlyException("District Name Dose not Exsist!");

            AddOrEditAmenitiesDto addAmenity = new AddOrEditAmenitiesDto();
            addAmenity = await _amenityAppService.AddAmenitiesFromUrl(HomeInfo);
            HomeInfo.HomeId = await _homeRepository.InsertAndGetIdAsync(ObjectMapper.Map<Home>(SetHomeInfoFromUrl(HomeInfo)));
            addAmenity.Id = HomeInfo.HomeId;
            await _amenityAppService.AddOrEditAmenities(addAmenity);
            await _advertisementRepository.InsertAsync(ObjectMapper.Map<Advertisement>(HomeInfo));
            return HomeInfo;
        }

        [AbpAuthorize(AppPermissions.Pages_Administration_Amenities)]
        [SwaggerHidden]
        public CreateOrUpdateHomeDto SetHomeInfoFromUrl(HomeInformation HomeInfo)
        {
            CreateOrUpdateHomeDto createHome = new CreateOrUpdateHomeDto();

            createHome.Name = HomeInfo.Name;
            createHome.Type = HomeInfo.Type;
            createHome.ZipCode = "test";
            createHome.Address = HomeInfo.Address;
            createHome.DistrictId = HomeInfo.DistrictId;
            createHome.Bathrooms = HomeInfo.Bathrooms;
            createHome.Bedrooms = HomeInfo.Bedrooms;
            createHome.Description = HomeInfo.Description;
            createHome.Space = HomeInfo.Space;
            createHome.Latitude = 0;
            createHome.Longitude = 0;
            return createHome;

        }

    }
}
