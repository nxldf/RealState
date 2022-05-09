using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DF.RealEstate.Entities.Homes
{
    public class HomePhoto : FullAuditedEntity
    {
        public string ContainerName { get; set; }
        public string OrginalAddress { get; set; }
        public string ThumbnailAddress { get; set; }
        public virtual Home Home { get; set; }
        public long HomeId { get; set; }
    }
}
