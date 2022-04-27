using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Addresses
{
    public class Country : FullAuditedEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Abbreviation { get; set; }
        public string Flag { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
