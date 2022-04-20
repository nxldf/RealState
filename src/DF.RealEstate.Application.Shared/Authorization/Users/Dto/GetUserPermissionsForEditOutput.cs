using System.Collections.Generic;
using DF.RealEstate.Authorization.Permissions.Dto;

namespace DF.RealEstate.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}