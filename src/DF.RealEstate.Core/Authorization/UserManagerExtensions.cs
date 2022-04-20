using System.Threading.Tasks;
using Abp.Authorization.Users;
using DF.RealEstate.Authorization.Users;

namespace DF.RealEstate.Authorization
{
    public static class UserManagerExtensions
    {
        public static async Task<User> GetAdminAsync(this UserManager userManager)
        {
            return await userManager.FindByNameAsync(AbpUserBase.AdminUserName);
        }
    }
}
