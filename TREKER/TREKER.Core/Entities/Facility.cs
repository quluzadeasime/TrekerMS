using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Facility : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public ICollection<TrekkingFacility> Trekkings { get; set; }
    }
}
