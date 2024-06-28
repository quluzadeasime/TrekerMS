using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Blog : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string ByUsername { get; set; }
        public string Description { get; set; }
        public IQueryable<Blog> Blogs { get; set; }
        public int DestinationId { get; set; }
        public Destination Destination { get; set; }
    }
}
