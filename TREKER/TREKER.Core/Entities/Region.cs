using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Region : BaseAuditableEntity
    {
        public string Name { get; set; }
        public IQueryable<Destination> Destinations { get; set; }
    }
}
