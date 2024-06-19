using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;
using TREKER.Core.Enums;

namespace TREKER.Core.Entities
{
    public class TeamMember:BaseAuditableEntity
    {
        public string FullName { get; set; }
        public string ImageUrl { get; set; }
        public TeamMemberRoles TeamMemberRoles { get; set; }
    }
}
