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

namespace BusinessLogicLayer.Services.QouteServiceContainer
{
    public class QouteService : IQouteService
    {
        private readonly GenericRepository<Qoute> _qouteRepository;
        private const string FolderName = "QoutesArtworks";
        public QouteService(GenericRepository<Qoute> qouteRepository)
        {
            _qouteRepository = qouteRepository;
        }

        public async Task<OutputHandler> CreateQoute(QouteDTO qouteDTO)
        {
            try
            {
                var outputhandler = await FileHandler.SaveFileFromByte(qouteDTO.ImgBytes, qouteDTO.FileName, FolderName);
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
                qouteDTO.QouteImg = outputhandler.ImageUrl;
                var mapped = new AutoMapper<QouteDTO, Qoute>().MapToObject(qouteDTO);

                _qouteRepository.CreateEntity(mapped);
                await _qouteRepository.SaveChangesAsync();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Qoute Created Successfully"
                };

            }
            catch (Exception ex)
            {

                var outputHandler = await FileHandler.DeleteFileFromFolder(qouteDTO.QouteImg, FolderName);

                return StandardMessages.getExceptionMessage(ex);


            }
        }

        public async Task<OutputHandler> DeleteQoute(int qouteId)
        {
            try
            {
                var qoute = await _qouteRepository.GetItemAsync(x => x.QouteId == qouteId);
                await _qouteRepository.DeleteAsync(qoute);
                var deletionresult = await FileHandler.DeleteFileFromFolder(qoute.QouteImg, FolderName);
                if (deletionresult.IsErrorOccured)
                {
                    return deletionresult;
                }
                await _qouteRepository.SaveChangesAsync();

                return new OutputHandler { IsErrorOccured = false, Message = "Qoute Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> GetAllQoutes(bool isFiltered)
        {
            try
            {
                if (isFiltered)
                {
                    var output = await _qouteRepository.GetListAsync(x => x.IsPublished);
                    var mapped = new AutoMapper<Qoute, QouteDTO>().MapToList(output);
                    foreach (var item in mapped)
                    {
                        item.ImgBytes = await FileHandler.ConvertFileToByte(item.QouteImg);
                    }
                    return new OutputHandler { Result = mapped, IsErrorOccured = false };
                }
                else
                {
                    var qoutes = new AutoMapper<Qoute, QouteDTO>().MapToList(await _qouteRepository.GetUnfilteredListAsync());
                    foreach (var item in qoutes)
                    {
                        var output = await FileHandler.GetFileSize(item.QouteImg);
                        if (output.IsErrorOccured)
                        {
                            item.StorageSize = "Could not retrieve size";
                        }
                        else
                        {
                            item.StorageSize = output.Result.ToString();
                        }
                    }
                    return new OutputHandler { Result = qoutes, IsErrorOccured = false };
                }
            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);
            }

        }
        public async Task<OutputHandler> GetAllQoutesForAdmin()
        {
            try
            {

                var qoutes = new AutoMapper<Qoute, QouteDTO>().MapToList(await _qouteRepository.GetUnfilteredListAsync());
                foreach (var item in qoutes)
                {
                    var output = await FileHandler.GetFileSize(item.QouteImg);
                    if (output.IsErrorOccured)
                    {
                        item.StorageSize = "Could not retrieve size";
                    }
                    else
                    {
                        item.StorageSize = output.Result.ToString();
                    }
                }
                return new OutputHandler { Result = qoutes, IsErrorOccured = false };
            }
            catch (Exception ex)
            {

                return StandardMessages.getExceptionMessage(ex);
            }


        }

        public async Task<OutputHandler> GetQoute(int qouteId)
        {
            var output = await _qouteRepository.GetItemAsync(x => x.QouteId == qouteId);
            var mapped = new AutoMapper<Qoute, QouteDTO>().MapToObject(output);
            mapped.ImgBytes = await FileHandler.ConvertFileToByte(mapped.QouteImg);

            return new OutputHandler { Result = mapped };
        }

        public async Task<OutputHandler> UpdateQoute(QouteDTO qouteDTO)
        {
            try
            {
                if (qouteDTO.ImgBytes == null)
                {
                    qouteDTO.QouteImg = qouteDTO.QouteImg;
                }
                else
                {
                    var outputhandler = await FileHandler.SaveFileFromByte(qouteDTO.ImgBytes, qouteDTO.FileName, FolderName);

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
                    qouteDTO.QouteImg = outputhandler.ImageUrl;
                }
                var mapped = new AutoMapper<QouteDTO, Qoute>().MapToObject(qouteDTO);
                await _qouteRepository.UpdateAsync(mapped);
                await _qouteRepository.SaveChangesAsync();

                if (qouteDTO.OldImageUrl == null)
                {

                }
                else
                {
                    if (qouteDTO.ImgBytes == null) //if Byte[] is null means image is not being updated 
                    {

                    }
                    else // only delete if artwork is not null meaning image is being updated 
                    //delete old file
                    {
                        var outputHandler = await FileHandler.DeleteFileFromFolder(qouteDTO.OldImageUrl, FolderName);
                        if (outputHandler.IsErrorOccured) //True means Delete was not successful for some reason
                        {
                            return new OutputHandler
                            {
                                IsErrorKnown = true,
                                IsErrorOccured = true,
                                Message = "Qoute Details updated successfully, but deleting of old file failed, please alert Techarch Team"
                            };
                        }
                    }
                }

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Qoute Updated Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);


            }

        }
    }
}
