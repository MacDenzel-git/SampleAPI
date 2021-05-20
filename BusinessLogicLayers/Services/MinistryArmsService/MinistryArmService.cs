using BusinessLogicLayer.BLLResources;
using BusinessLogicLayer.Resources;
using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.projectArmsService
{
    public class projectArmService : IprojectArmService
    {
        private readonly GenericRepository<projectArm> _projectArmRepository;
        private const string FolderName = "projectArmsArtworks";
        public projectArmService(GenericRepository<projectArm> projectArmRepository)
        {
            _projectArmRepository = projectArmRepository;
        }

        public async Task<OutputHandler> CreateprojectArm(projectArmDTO projectArmDTO)
        {
            
            try
            {
                var outputhandler = await FileHandler.SaveFileFromByte(projectArmDTO.ImgBytes, projectArmDTO.Filename, FolderName);
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
                projectArmDTO.Artwork = outputhandler.ImageUrl; //saving the bytes as string 
                var mapped = new AutoMapper<projectArmDTO, projectArm>().MapToObject(projectArmDTO);
                _projectArmRepository.CreateEntity(mapped);
                await _projectArmRepository.SaveChangesAsync();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "project Arm Created Successfully"
                };

            }
            catch (Exception ex)
            {
                var outputHandler = await FileHandler.DeleteFileFromFolder(projectArmDTO.Artwork, FolderName);
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> DeleteprojectArm(int projectArmId)
        {
            try
            {
                var projectArm = await _projectArmRepository.GetItemAsync(x => x.projectArmId == projectArmId);
                await _projectArmRepository.DeleteAsync(projectArm);
                await _projectArmRepository.SaveChangesAsync();

                var outputHandler = await FileHandler.DeleteFileFromFolder(projectArm.Artwork, FolderName);
                if (outputHandler.IsErrorOccured) //Deletion failed but updated saved
                {
                    return new OutputHandler
                    {
                        IsErrorKnown = true,
                        IsErrorOccured = true,
                        Message = "Resource deleted successfully, but deleting of old file failed, please alert Techarch Team"
                    };
                }
                return new OutputHandler { IsErrorOccured = false, Message = "project Arm Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> GetAllprojectArms(bool isAdminRequest)
        {
            var projectArms = await _projectArmRepository.GetUnfilteredListAsync();
            var projectArmsDTO = new AutoMapper<projectArm, projectArmDTO>().MapToList(projectArms);
            if (!isAdminRequest)
            {

                foreach (var item in projectArmsDTO)
                {
                    item.ImgBytes = await FileHandler.ConvertFileToByte(item.Artwork); //convert for each record to send back to client for display
                }
            }
            else
            {
                foreach (var item in projectArmsDTO)
                {
                    var output = await FileHandler.GetFileSize(item.Artwork);
                    if (output.IsErrorOccured)
                    {
                        item.StorageSize = "Could not retrieve size";
                    }
                    else
                    {
                        item.StorageSize = output.Result.ToString();
                    }
                }
            }
            return new OutputHandler { Result = projectArmsDTO, IsErrorOccured = false };
        }

        public async Task<OutputHandler> GetprojectArm(int projectArmId)
        {
            var output = await _projectArmRepository.GetItemAsync(x => x.projectArmId == projectArmId);
            var projectArm = new AutoMapper<projectArm, projectArmDTO>().MapToObject(output);
            projectArm.ImgBytes = await FileHandler.ConvertFileToByte(projectArm.Artwork);

            return new OutputHandler { Result = projectArm };
        }

        public async Task<OutputHandler> UpdateprojectArm(projectArmDTO projectArmDTO)
        {
            try
            {
                if (projectArmDTO.ImgBytes == null)
                {
                    projectArmDTO.Artwork = projectArmDTO.OldImageUrl;
                }
                else
                {
                    var outputhandler = await FileHandler.SaveFileFromByte(projectArmDTO.ImgBytes, projectArmDTO.Filename, FolderName);

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
                    projectArmDTO.Artwork = outputhandler.ImageUrl;
                }
                var mapped = new AutoMapper<projectArmDTO, projectArm>().MapToObject(projectArmDTO);
                await _projectArmRepository.UpdateAsync(mapped);
                await _projectArmRepository.SaveChangesAsync();
                if (projectArmDTO.ImgBytes != null)
                {

                    var outputHandler = await FileHandler.DeleteFileFromFolder(projectArmDTO.OldImageUrl, FolderName);
                    if (outputHandler.IsErrorOccured) //Deletion failed but updated saved
                    {
                        return new OutputHandler
                        {
                            IsErrorKnown = true,
                            IsErrorOccured = true,
                            Message = "resource Details updated successfully, but deleting of old file failed, please alert Techarch Team"
                        };
                    }
                }
                

                    
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "project Arm Updated Successfully"
                };

            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);


            }

        }
    }
}
