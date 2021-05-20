using BusinessLogicLayer.BLLResources;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;
using System.Linq;
using System.IO;
using BusinessLogicLayer.Resources;

namespace BusinessLogicLayer.Services.TeamServiceContainer
{
    public class TeamService : ITeamService
    {
        private const string FolderName = "TeamMembersArtworks";

        private readonly GenericRepository<TeamMember> _teamMemberRepository;
        private ProjectWebsiteDBContext _dbContext;
        public TeamService(GenericRepository<TeamMember> teamRepository, ProjectWebsiteDBContext context)
        {
            _dbContext = context;
            _teamMemberRepository = teamRepository;
        }
        public async Task<OutputHandler> AddTeamMemberAsync(TeamMembersDTO teamMember)
        {
            
            var outputhandler = await FileHandler.SaveFileFromByte(teamMember.Artwork, teamMember.Filename, FolderName);
            if (outputhandler.IsErrorOccured)
            {
                if (outputhandler.IsErrorKnown)
                {
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = outputhandler.Message
                    };
                }
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Something went wrong while the system tried to upload the file"
                };
            }
            teamMember.ImageUrl = outputhandler.ImageUrl;


            try
            {

                var MappedTeamMember = new AutoMapper<TeamMembersDTO, TeamMember>().MapToObject(teamMember);
                _teamMemberRepository.CreateEntity(MappedTeamMember);
                await _teamMemberRepository.SaveChangesAsync();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "TeamMember has been created Successsdfully "

                };
            }
            catch (Exception ex)
            {
                var outputHandler = await FileHandler.DeleteFileFromFolder(teamMember.ImageUrl, FolderName);
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> EditTeamMember(TeamMembersDTO teamMember)
        {

            if (teamMember.Artwork == null)
            {
                teamMember.ImageUrl = teamMember.OldImageUrl;
            }
            else
            {
                var outputhandler = await FileHandler.SaveFileFromByte(teamMember.Artwork, teamMember.Filename, FolderName);
                if (outputhandler.IsErrorOccured)
                {
                    if (outputhandler.IsErrorKnown)
                    {
                        return new OutputHandler
                        {
                            IsErrorOccured = true,
                            Message = outputhandler.Message
                        };
                    }
                    return new OutputHandler
                    {
                        IsErrorOccured = true,
                        Message = "Something went wrong while the system tried to upload the file"
                    };
                }


                teamMember.ImageUrl = outputhandler.ImageUrl;

            }
            try
            {
                var MappedTeamMember = new AutoMapper<TeamMembersDTO, TeamMember>().MapToObject(teamMember);
                await _teamMemberRepository.UpdateAsync(MappedTeamMember);
                await _teamMemberRepository.SaveChangesAsync();

                if (teamMember.OldImageUrl == null)
                {

                }
                else
                {
                    if (teamMember.Artwork == null) //if Byte[] is null means image is not being updated 
                    {

                    }
                    else // only delete if artwork is not null meaning image is being updated 
                    //delete old file
                    {
                        var outputHandler = await FileHandler.DeleteFileFromFolder(teamMember.OldImageUrl, FolderName);
                        if (outputHandler.IsErrorOccured) //True means Delete was not successful for some reason
                        {
                            return new OutputHandler
                            {
                                IsErrorKnown = true,
                                IsErrorOccured = true,
                                Message = "Team Member Details updated successfully, but deleting of old file failed, please alert Techarch Team"
                            };
                        }
                    }
                }
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Team Member has been edited Successsdfully "

                };
            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<IEnumerable<TeamMembersDTO>> GetFilteredTeamMembersListAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamMembersDTO> GetTeamMember(long TeamMemberId)
        {
            var TeamMembers = await _teamMemberRepository.GetItemAsync(x => x.TeamMemberId == TeamMemberId);
            var TeamMemberDTO = new AutoMapper<TeamMember, TeamMembersDTO>().MapToObject(TeamMembers);
            if (TeamMemberDTO.ImageUrl == null)
            {

            }
            else
            {
                TeamMemberDTO.Artwork = await FileHandler.ConvertFileToByte(TeamMembers.ImageUrl);
            }
            return TeamMemberDTO;
        }

        public async Task<IEnumerable<TeamMembersDTO>> GetTeamMembersListAsync()
        {
            try
            {

                var TeamMembers = (from root in _dbContext.TeamMembers.Include(x => x.Branch)
                                   where root.IsPublished
                                   select new TeamMembersDTO
                                   {
                                       BranchId = root.BranchId,
                                       projectArmId = root.projectArmId,
                                       PositionId = root.PositionId,
                                       TeamMemberId = root.TeamMemberId,
                                       Name = root.Name,
                                       ImageUrl = root.ImageUrl,
                                       BranchName = root.Branch.BranchName,
                                       IsPublished = root.IsPublished,
                                       IsTopLeadership = root.IsTopLeadership,
                                       IsDirector = root.IsDirector,
                                       IsStaffMember = root.IsStaffMember,
                                       IsBranchLeader = root.IsBranchLeader,
                                       IsECMember = root.IsEcmember
                                   }).ToList();

                
                foreach (var item in TeamMembers)
                {
                    if (item.ImageUrl == null)
                    {

                    }
                    else
                    {
                        item.Artwork = await FileHandler.ConvertFileToByte(item.ImageUrl);

                    }
                }

                return TeamMembers;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TeamMembersDTO>> GetTeamMembersListForAdminAsync()
        {
            var teamMember = new AutoMapper<TeamMember, TeamMembersDTO>().MapToList(await _teamMemberRepository.GetUnfilteredListAsync());

            foreach (var item in teamMember)
            {
                var output = await FileHandler.GetFileSize(item.ImageUrl);
                if (output.IsErrorOccured)
                {
                    item.StorageSize = "Could not retrieve size";
                }
                else
                {
                    item.StorageSize = output.Result.ToString();
                }
                 
            }
            return teamMember;
        }

        public async Task<OutputHandler> DeleteTeamMember(int teamMemberId)
        {
            try
            {
                var teamMember = await _teamMemberRepository.GetItemAsync(x => x.TeamMemberId == teamMemberId);
                await _teamMemberRepository.DeleteAsync(teamMember);
                await _teamMemberRepository.SaveChangesAsync(); ;
                var outputHandler = await FileHandler.DeleteFileFromFolder(teamMember.ImageUrl, FolderName);
                if (outputHandler.IsErrorOccured) // FILE Deletion failed but updated RECORD deleted
                {
                    return new OutputHandler
                    {
                        IsErrorKnown = true,
                        IsErrorOccured = true,
                        Message = "resource deleted successfully, but deleting of old file failed, please alert Techarch Team"
                    };
                }
                return new OutputHandler { IsErrorOccured = false, Message = "Team Member Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }
    }
}
