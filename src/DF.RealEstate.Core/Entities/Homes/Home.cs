using Abp.Domain.Entities.Auditing;
using DF.RealEstate.Entities.Addresses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Homes
{
    public class Home : FullAuditedEntity<long>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PropertyType Type { get; set; }

        public string Address { get; set; }
        public string ZipCode { get; set; }
        public virtual District District { get; set; }
        public int DistrictId { get; set; }

        public long Space { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public int Bedrooms { get; set; }
        public int Bathrooms { get; set; }

        public virtual ICollection<HomeAmenity> Amenities { get; set; }
        public virtual ICollection<HomePhoto> Photos { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }
    }

}
