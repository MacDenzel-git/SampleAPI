using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.BranchServiceContainer
{
    public interface IBranchService
    {
        Task<IEnumerable<BranchDTO>> GetAllBranches();
        Task<BranchDTO> GetBranchDetailsByTeamMemberId(int teamMemberId);
        Task<BranchDTO> GetBranchDetailsByBranchId(int branchId);
    }
}
