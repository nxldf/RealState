using System.Collections.Generic;
using DF.RealEstate.DashboardCustomization.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
