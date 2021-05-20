using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.TeamServiceContainer
{
    public interface ITeamService
    {
        Task<OutputHandler> AddTeamMemberAsync(TeamMembersDTO TeamMember);
        Task<IEnumerable<TeamMembersDTO>> GetTeamMembersListAsync();
        Task<IEnumerable<TeamMembersDTO>> GetFilteredTeamMembersListAsync(int categoryId);
        Task<IEnumerable<TeamMembersDTO>> GetTeamMembersListForAdminAsync();
        Task<TeamMembersDTO> GetTeamMember(long TeamMemberId);
        Task<OutputHandler> EditTeamMember(TeamMembersDTO TeamMember);
        Task<OutputHandler> DeleteTeamMember(int teamMemberId);
    }
}
