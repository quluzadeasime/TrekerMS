using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Setting : BaseAuditableEntity
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Facebook { get; set; }
        public string Instagram { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }

    }
}
