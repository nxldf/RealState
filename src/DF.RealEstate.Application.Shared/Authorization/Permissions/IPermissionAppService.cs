using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DF.RealEstate.Authorization.Permissions.Dto;

namespace DF.RealEstate.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
