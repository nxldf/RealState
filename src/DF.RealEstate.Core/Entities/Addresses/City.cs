using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Addresses
{
    public class City : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
