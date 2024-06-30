using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.AppointmentVMs;
using TREKER.Business.ViewModels.ContactVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Implementations;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task CreateAsync(CreateAppointmentVM vm)
        {
            var newAppointment = new Appointment()
            {
                Fullname = vm.Fullname,
                Email = vm.Email,
                Phone = vm.Phone,
                Date = vm.Date,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _appointmentRepository.CreateAsync(newAppointment);
            await _appointmentRepository.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
        {
            await _appointmentRepository.DeleteAsync(id);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<Appointment>> GetAllAsync()
        {
            return await _appointmentRepository.GetAllAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _appointmentRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _appointmentRepository.RecoverAsync(id);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _appointmentRepository.Remove(id);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateAppointmentVM vm)
        {
            var oldAppointment = await _appointmentRepository.GetByIdAsync(vm.Id);

            oldAppointment.Fullname = vm.Fullname ?? oldAppointment.Fullname;
            oldAppointment.Email = vm.Email ?? oldAppointment.Email;
            oldAppointment.Phone = vm.Phone ?? oldAppointment.Phone;
            oldAppointment.Date = vm.Date;
            oldAppointment.IsVerified = vm.IsVerified;
            oldAppointment.IsFinished = vm.IsFinished;

            await _appointmentRepository.UpdateAsync(oldAppointment);
            await _appointmentRepository.SaveChangesAsync();

        }

        public async Task VerifyAppointment(int id)
        {
            var oldAppointment = await _appointmentRepository.GetByIdAsync(id);

            oldAppointment.IsVerified = true;

            await _appointmentRepository.UpdateAsync(oldAppointment);
            await _appointmentRepository.SaveChangesAsync();
        }

        public async Task FinishAppointment(int id)
        {
            var oldAppointment = await _appointmentRepository.GetByIdAsync(id);

            oldAppointment.IsFinished = true;

            await _appointmentRepository.UpdateAsync(oldAppointment);
            await _appointmentRepository.SaveChangesAsync();

        }
    }
}
