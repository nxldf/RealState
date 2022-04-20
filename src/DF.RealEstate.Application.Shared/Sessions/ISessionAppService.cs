using System.Threading.Tasks;
using Abp.Application.Services;
using DF.RealEstate.Sessions.Dto;

namespace DF.RealEstate.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
