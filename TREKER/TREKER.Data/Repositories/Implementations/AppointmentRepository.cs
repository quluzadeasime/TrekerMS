using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Core.Entities;
using TREKER.DAL.Context;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.DAL.Repositories.Implementations
{
    public class AppointmentRepository : Repository<Appointment>, IAppointmentRepository
    {
        public AppointmentRepository(AppDbContext context) : base(context)
        {
        }
    }
}
