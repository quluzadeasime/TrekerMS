using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Destination : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        public int RegionId { get; set; }
        public Region Region { get; set; }
        public ICollection<Trekking> Trekkings { get; set; }
        public ICollection<Blog> Blogs { get; set; }
    }
}
