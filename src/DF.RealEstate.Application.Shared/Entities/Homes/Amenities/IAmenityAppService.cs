using Abp.Application.Services.Dto;
using DF.RealEstate.Entities.Homes.Amenities.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Homes.Amenities
{
    public interface IAmenityAppService
    {
        Task<PagedResultDto<AmenityListDto>> GetAll(GetAllAminitiesInput input);
        Task CreateOrEdit(CreateOrEditAmenityDto input);
        Task Delete(EntityDto input);
        Task<GetForEditAmenityDto> GetForEdit(NullableIdDto input);
    }
}
