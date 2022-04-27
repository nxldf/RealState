using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Homes
{
    public class HomeAmenity : Entity
    {
        public virtual Home Home { get; set; }
        public virtual Amenity Amenity { get; set; }
        public int HomeId { get; set; }
        public int AmenityId { get; set; }
    }
}
