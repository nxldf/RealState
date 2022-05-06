using Abp.Application.Services.Dto;
using DF.RealEstate.Homes.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Homes
{
    public interface IHomeAppService
    {
        Task<PagedResultDto<HomeListDto>> GetAll(GetAllHomeInput input);
        Task DeleteCountry(EntityDto<long> input);
        Task<GetForEditHomeDto> GetHomeForEdit(NullableIdDto<long> input);
        Task CreateOrEditHome(CreateOrUpdateHomeDto input);
    }
}
