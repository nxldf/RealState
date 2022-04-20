using System.Collections.Generic;
using DF.RealEstate.Editions.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.Tenants
{
    public class TenantIndexViewModel
    {
        public List<SubscribableEditionComboboxItemDto> EditionItems { get; set; }
    }
}