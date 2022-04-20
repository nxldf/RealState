using System.Collections.Generic;
using DF.RealEstate.Caching.Dto;

namespace DF.RealEstate.Web.Areas.App.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}