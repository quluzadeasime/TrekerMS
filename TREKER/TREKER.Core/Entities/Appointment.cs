using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities.Commons;

namespace TREKER.Core.Entities
{
    public class Appointment : BaseAuditableEntity
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime Date { get; set; }
        public bool IsVerified { get; set; }
        public bool IsFinished { get; set; }
    }
}
