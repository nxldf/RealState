using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Homes
{
    public class Amenity : FullAuditedEntity, IMultiLingualEntity<AmenityTranslation>
    {
        public string AdminName { get; set; }
        public virtual ICollection<HomeAmenity> Homes { get; set; }
        public virtual ICollection<AmenityTranslation> Translations { get; set; }
    }

    public class AmenityTranslation : Entity, IEntityTranslation<Amenity>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public Amenity Core { get; set; }

        public int CoreId { get; set; }

        public string Language { get; set; }
    }
}
