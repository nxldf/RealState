using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DF.RealEstate.Authorization.Permissions.Dto;
using DF.RealEstate.Web.Areas.App.Models.Common;

namespace DF.RealEstate.Web.Areas.App.Models.Roles
{
    public class RoleListViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}