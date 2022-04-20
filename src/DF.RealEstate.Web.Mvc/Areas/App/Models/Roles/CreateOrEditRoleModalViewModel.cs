using Abp.AutoMapper;
using DF.RealEstate.Authorization.Roles.Dto;
using DF.RealEstate.Web.Areas.App.Models.Common;

namespace DF.RealEstate.Web.Areas.App.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}