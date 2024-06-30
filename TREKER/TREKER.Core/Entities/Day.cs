using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Day : BaseAuditableEntity
    {
        public string Description { get; set; }
        public int TrekkingId { get; set; }
        public Trekking Trekking { get; set; }
    }
}
