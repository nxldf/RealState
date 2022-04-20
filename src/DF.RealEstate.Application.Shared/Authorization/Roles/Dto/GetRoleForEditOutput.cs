using System.Collections.Generic;
using DF.RealEstate.Authorization.Permissions.Dto;

namespace DF.RealEstate.Authorization.Roles.Dto
{
    public class GetRoleForEditOutput
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}