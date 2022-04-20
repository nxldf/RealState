using System.Collections.Generic;
using DF.RealEstate.Authorization.Permissions.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}