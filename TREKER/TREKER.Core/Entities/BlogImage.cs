using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class BlogImage : BaseAuditableEntity
    {
        public string ImageUrl { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
