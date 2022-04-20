using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DF.RealEstate.Authorization.Users.Dto;

namespace DF.RealEstate.Authorization.Users
{
    public interface IUserLoginAppService : IApplicationService
    {
        Task<PagedResultDto<UserLoginAttemptDto>> GetUserLoginAttempts(GetLoginAttemptsInput input);
    }
}
