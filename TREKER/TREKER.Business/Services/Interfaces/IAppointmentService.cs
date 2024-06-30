using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.ViewModels.AppointmentVMs;
using TREKER.Business.ViewModels.ContactVMs;
using TREKER.Core.Entities;

namespace TREKER.Business.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<IQueryable<Appointment>> GetAllAsync();
        Task<Appointment> GetByIdAsync(int id);
        Task CreateAsync(CreateAppointmentVM vm);
        Task UpdateAsync(UpdateAppointmentVM vm);
        Task DeleteAsync(int id);
        Task RecoverAsync(int id);
        Task RemoveAsync(int id);
        Task VerifyAppointment(int id);
        Task FinishAppointment(int id);
    }
}
