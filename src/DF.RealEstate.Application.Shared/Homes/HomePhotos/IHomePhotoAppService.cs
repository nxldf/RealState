using Abp.Application.Services.Dto;
using DF.RealEstate.Homes.HomePhotos.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Homes.HomePhotos
{
    public interface IHomePhotoAppService
    {
        Task<PagedResultDto<HomePhotoDto>> GetAll(GetHomePhotosInput input);
        Task Delete(EntityDto<int> input);
        Task CreateOrEdit(CreateOrEditHomePhotoDto input);
        Task<HomePhotoDto> GetForEdit(NullableIdDto<int> input);
    }
}
