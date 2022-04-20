using System.Collections.Generic;
using DF.RealEstate.Authorization.Delegation;
using DF.RealEstate.Authorization.Users.Delegation.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.Layout
{
    public class ActiveUserDelegationsComboboxViewModel
    {
        public IUserDelegationConfiguration UserDelegationConfiguration { get; set; }
        
        public List<UserDelegationDto> UserDelegations { get; set; }
    }
}
