using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Difficulty : BaseAuditableEntity
    {
        public string Name { get; set; }
        public ICollection<Trekking> Trekkings { get; set; }
    }
}
