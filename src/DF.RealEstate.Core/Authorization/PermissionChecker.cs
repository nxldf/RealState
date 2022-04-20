using Abp.Authorization;
using DF.RealEstate.Authorization.Roles;
using DF.RealEstate.Authorization.Users;

namespace DF.RealEstate.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
