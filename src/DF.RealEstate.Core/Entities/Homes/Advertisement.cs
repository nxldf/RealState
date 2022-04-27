using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Homes
{
    public class Advertisement : FullAuditedEntity
    {
        public DateTime? AvailableDate { get; set; }
        public AdvertisementType Type { get; set; }
        public decimal NetPrice { get; set; }
        public bool HideAddress { get; set; }
        public bool HidePreciseLocation { get; set; }
        public bool ContactByPhone { get; set; }
        public bool ContactByEmail { get; set; }
        public bool ContactSiteMessage { get; set; }
    }
}
