using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TREKER.Business.Helpers;
using TREKER.Business.Services.Interfaces;
using TREKER.Business.ViewModels.TeamMemberVMs;
using TREKER.Core.Entities;
using TREKER.DAL.Repositories.Interfaces;

namespace TREKER.Business.Services.Abstractions
{
    public class TeamMemberService : ITeamMemberService
    {
        private readonly ITeamMemberRepository _teamMemberRepository;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public TeamMemberService(ITeamMemberRepository teamMemberRepository, IConfiguration configuration)
        {
            _teamMemberRepository = teamMemberRepository;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("AzureContainer");
        }

        public async Task CreateAsync(CreateTeamMemberVm vm)
        {
            TeamMember newTeamMember = new()
            {
                FullName = vm.FullName,
                ImageUrl = await vm.File.UploadFileAsync(_connectionString, "TeamMemberPictures/"),
                TeamMemberRoles = vm.MemberRole,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
            };

            await _teamMemberRepository.CreateAsync(newTeamMember);
            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(UpdateTeamMemberVm vm)
        {
            var oldTeamMember = await _teamMemberRepository.GetByIdAsync(vm.Id);

            oldTeamMember.FullName = vm.FullName ?? oldTeamMember.FullName;
            oldTeamMember.TeamMemberRoles = vm.MemberRole ?? oldTeamMember.TeamMemberRoles;

            if (vm.File is not null)
            {
                if (!string.IsNullOrEmpty(oldTeamMember.ImageUrl) && vm.File != null)
                {
                    Uri uri = new Uri(oldTeamMember.ImageUrl);
                    string blobName = uri.Segments.Last();
                    await FileManager.DeleteFileAsync(blobName, _connectionString, "TeamMemberPictures/");
                }

                oldTeamMember.ImageUrl = await vm.File.UploadFileAsync(_connectionString, "TeamMemberPictures/");
            }

            await _teamMemberRepository.UpdateAsync(oldTeamMember);
            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _teamMemberRepository.DeleteAsync(id);
            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task<IQueryable<TeamMember>> GetAllAsync()
        {
            return await _teamMemberRepository.GetAllAsync();
        }

        public async Task<TeamMember> GetByIdAsync(int id)
        {
            return await _teamMemberRepository.GetByIdAsync(id);
        }

        public async Task RecoverAsync(int id)
        {
            await _teamMemberRepository.RecoverAsync(id);
            await _teamMemberRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            _teamMemberRepository.Remove(id);
            await _teamMemberRepository.SaveChangesAsync();
        }

    }
}