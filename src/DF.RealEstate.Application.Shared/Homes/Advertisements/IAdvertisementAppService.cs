using Abp.Application.Services.Dto;
using DF.RealEstate.Homes.Advertisements.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Homes.Advertisements
{
    public interface IAdvertisementAppService
    {
        Task<PagedResultDto<AdvertisementListDto>> GetAll(GetAllAdvertisementInput input);
        Task Delete(EntityDto input);
        Task CreateOrEdit(CreateOrEditAdvertisementDto input);
        Task<GetForEditAdvertisementDto> GetForEdit(NullableIdDto input);
    }
}
