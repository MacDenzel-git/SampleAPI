using BusinessLogicLayer.BLLResources;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;

namespace BusinessLogicLayer.Services.BranchServiceContainer
{
    public class BranchService : IBranchService
    {
        readonly GenericRepository<Branch> _branchRepository;
        readonly GenericRepository<TeamMember> _teamMemberRepository;
        readonly GenericRepository<ProjectPartnershipPlatform> _givingPlatformRepository;
        public BranchService(GenericRepository<Branch> branchRepository,
            GenericRepository<TeamMember> teamMemberRepository,
            GenericRepository<ProjectPartnershipPlatform> givingPlatformRepository)
        {
            _givingPlatformRepository = givingPlatformRepository;
            _branchRepository = branchRepository;
            _teamMemberRepository = teamMemberRepository;
        }

        //should always return resultHandler

        public async Task<IEnumerable<BranchDTO>> GetAllBranches()
        {
            return new AutoMapper<Branch, BranchDTO>().MapToList(await _branchRepository.GetUnfilteredListAsync());
        }

        public async Task<BranchDTO> GetBranchDetailsByTeamMemberId(int teamMemberId)
        {

            try
            {
                var teamMember = await _teamMemberRepository.GetItemAsync(x => x.TeamMemberId == teamMemberId);
                var branch = new AutoMapper<Branch, BranchDTO>().MapToObject(await _branchRepository.GetItemAsync(x => x.BranchId == teamMember.BranchId));
                var givingPlatforms = new AutoMapper<ProjectPartnershipPlatform, ProjectPartnershipPlatformsDTO>().MapToList(await _givingPlatformRepository.GetListAsync(x => x.BranchId == teamMember.BranchId));
                branch.BranchLeaderName = teamMember.Name;
                branch.ImgBytes = await FileHandler.ConvertFileToByte(branch.ImageUrl);
                branch.GivingPlatforms = givingPlatforms;
                return branch;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        
        public async Task<BranchDTO> GetBranchDetailsByBranchId(int branchId)
        {

            try
            {
                var teamMember = await _teamMemberRepository.GetItemAsync(x => x.BranchId == branchId);
                var branch = new AutoMapper<Branch, BranchDTO>().MapToObject(await _branchRepository.GetItemAsync(x => x.BranchId ==  branchId));
                var givingPlatforms = new AutoMapper<ProjectPartnershipPlatform, ProjectPartnershipPlatformsDTO>().MapToList(await _givingPlatformRepository.GetListAsync(x => x.BranchId == branchId));

                if (teamMember == null)
                {
                    branch.BranchLeaderName = "";
                }
                else
                {
                    branch.BranchLeaderName = teamMember.Name;
                }

                branch.ImgBytes = await FileHandler.ConvertFileToByte(branch.ImageUrl);
                branch.GivingPlatforms = givingPlatforms;
                return branch;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
