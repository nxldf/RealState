using Abp.AspNetCore.Mvc.Authorization;
using DF.RealEstate.Authorization;
using DF.RealEstate.Storage;
using Abp.BackgroundJobs;

namespace DF.RealEstate.Web.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Administration_Users)]
    public class UsersController : UsersControllerBase
    {
        public UsersController(IBinaryObjectManager binaryObjectManager, IBackgroundJobManager backgroundJobManager)
            : base(binaryObjectManager, backgroundJobManager)
        {
        }
    }
}