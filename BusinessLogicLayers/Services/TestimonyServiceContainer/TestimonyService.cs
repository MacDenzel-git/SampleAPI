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

namespace BusinessLogicLayer.Services.TestimonyServiceContainer
{
    public class TestimonyService : ITestimonyService
    {
        private readonly GenericRepository<Testimony> _testimonyRepository;
        private const string FolderName = "TestimonyImages";
        public TestimonyService(GenericRepository<Testimony> testimonyRepository)
        {
            _testimonyRepository = testimonyRepository;
        }

        public async Task<OutputHandler> CreateTestimony(TestimonyDTO testimonyDTO)
        {
            try
            {
                var outputhandler = await FileHandler.SaveFileFromByte(testimonyDTO.ImgBytes, testimonyDTO.FileName, FolderName);
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
                testimonyDTO.ImageUrl = outputhandler.ImageUrl;
                var mapped = new AutoMapper<TestimonyDTO, Testimony>().MapToObject(testimonyDTO);

                _testimonyRepository.CreateEntity(mapped);
                await _testimonyRepository.SaveChangesAsync();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "testimony Created Successfully"
                };

            }
            catch (Exception ex)
            {
                await FileHandler.DeleteFileFromFolder(testimonyDTO.ImageUrl, FolderName);
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> DeleteTestimony(int testimonyId)
        {
            try
            {
                var testimony = await _testimonyRepository.GetItemAsync(x => x.TestimonyId == testimonyId);
                await _testimonyRepository.DeleteAsync(testimony);
                var deletionresult = await FileHandler.DeleteFileFromFolder(testimony.ImageUrl, FolderName);
                if (deletionresult.IsErrorOccured)
                {
                    return deletionresult;
                }
                await _testimonyRepository.SaveChangesAsync();

                return new OutputHandler { IsErrorOccured = false, Message = "Testimony Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        public async Task<OutputHandler> GetAllTestimonies(bool isfiltered)
        {

            if (isfiltered)
            {
                var output = await _testimonyRepository.GetListAsync(x => x.IsPublished);
                var mapped = new AutoMapper<Testimony, TestimonyDTO>().MapToList(output);
                foreach (var item in mapped)
                {
                    item.ImgBytes = await FileHandler.ConvertFileToByte(item.ImageUrl);

                }

                return new OutputHandler { Result = mapped, IsErrorOccured = false };
            }
            else
            {
                var testimonies = new AutoMapper<Testimony,TestimonyDTO>().MapToList(await _testimonyRepository.GetUnfilteredListAsync());
                foreach (var item in testimonies)
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
                return new OutputHandler { Result = testimonies, IsErrorOccured = false };
            }

        }

        public async Task<OutputHandler> GetTestimony(int testimonyId)
        {
            var output = await _testimonyRepository.GetItemAsync(x => x.TestimonyId == testimonyId);
            var mapped = new AutoMapper<DataAccessLayer.Models.Testimony, TestimonyDTO>().MapToObject(output);
            mapped.ImgBytes = await FileHandler.ConvertFileToByte(output.ImageUrl);
            return new OutputHandler { Result = mapped };
        }

        public async Task<OutputHandler> UpdateTestimony(TestimonyDTO testimonyDTO)
        {
            try
            {
                if (testimonyDTO.ImgBytes == null)
                {
                    testimonyDTO.ImageUrl = testimonyDTO.ImageUrl;
                }
                else
                {
                    var outputhandler = await FileHandler.SaveFileFromByte(testimonyDTO.ImgBytes, testimonyDTO.FileName, FolderName);

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
                    testimonyDTO.ImageUrl = outputhandler.ImageUrl;
                }
                var mapped = new AutoMapper<TestimonyDTO, Testimony>().MapToObject(testimonyDTO);
                await _testimonyRepository.UpdateAsync(mapped);
                await _testimonyRepository.SaveChangesAsync();
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Testimony Updated Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }

        }
    }
}
