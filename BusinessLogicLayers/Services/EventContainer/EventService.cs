using DataAccessLayer.DataTransferObjects;
using DataAccessLayer.GenericRepoSettings;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;
using TechArchDataHandler.AutoMapper;
using BusinessLogicLayer.BLLResources;
using BusinessLogicLayer.Resources;

namespace BusinessLogicLayer.Services.EventContainer
{
    public class EventService : IEventService
    {
        private readonly GenericRepository<Event> _eventRepository;
        private const string FolderName = "EventArtworks";
        public EventService(GenericRepository<Event> eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public async Task<OutputHandler> CreateEvent(EventDTO eventsDTO)
        {
            try
            {
              
                var outputhandler = await FileHandler.SaveFileFromByte(eventsDTO.ImgBytes, eventsDTO.FileName, FolderName);
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

                var mapped = new AutoMapper<EventDTO, Event>().MapToObject(eventsDTO);
                mapped.ImageUrl = outputhandler.ImageUrl;
                _eventRepository.CreateEntity(mapped);
                await _eventRepository.SaveChangesAsync();

                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "Event Created Successfully"
                };

            }
            catch (Exception ex)
            {
                var outputHandler = await FileHandler.DeleteFileFromFolder(eventsDTO.ImageUrl, FolderName);

                return StandardMessages.getExceptionMessage(ex);

            }
        }

        public async Task<OutputHandler> DeleteEvent(int eventId)
        {
            try
            {
                var output = await _eventRepository.GetItemAsync(x => x.EventId == eventId);
                await _eventRepository.DeleteAsync(output);
                await _eventRepository.SaveChangesAsync();
                var outputHandler = await FileHandler.DeleteFileFromFolder(output.ImageUrl, FolderName);
                if (outputHandler.IsErrorOccured) // FILE Deletion failed but updated RECORD deleted
                {
                    return new OutputHandler
                    {
                        IsErrorKnown = true,
                        IsErrorOccured = true,
                        Message = "resource deleted successfully, but deleting of old file failed, please alert Techarch Team"
                    };
                }
                return new OutputHandler { IsErrorOccured = false, Message = "Event Deleted Successfully" };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);
            }
        }

        
        public async Task<OutputHandler> GetAllEvents()
        {
            var output = await _eventRepository.GetUnfilteredListAsync();
            var mapped = new AutoMapper<Event,EventDTO >().MapToList(output);
            foreach (var item in mapped)
            {
                item.ImgBytes = await FileHandler.ConvertFileToByte(item.ImageUrl); //convert for each record to send back to client for display
            }
            return new OutputHandler { Result = mapped, IsErrorOccured = false };
        }
        
        public async Task<OutputHandler> GetAllEventsForAdmin()
        {
            var events = await _eventRepository.GetUnfilteredListAsync();
            var mapped = new AutoMapper<Event,EventDTO >().MapToList(events);
            foreach (var item in mapped)
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
            return new OutputHandler { Result = mapped, IsErrorOccured = false };
        }

        public async Task<OutputHandler> GetEvent(int eventId)
        {
            var output = await _eventRepository.GetItemAsync(x => x.EventId == eventId);
            var mapped = new AutoMapper<Event, EventDTO>().MapToObject(output);
              mapped.ImgBytes = await FileHandler.ConvertFileToByte(output.ImageUrl); //convert for each record to send back to client for display
           
            return new OutputHandler { Result = mapped, IsErrorOccured = false };

        }

        public async Task<OutputHandler> UpdateEvent(EventDTO eventDTO)
        {
            
            try
            {
                if (eventDTO.ImgBytes == null)
                { 
                    eventDTO.ImageUrl = eventDTO.ImageUrl;
                }
                else
                {
                    var outputhandler = await FileHandler.SaveFileFromByte(eventDTO.ImgBytes, eventDTO.FileName, FolderName);

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
                    eventDTO.ImageUrl = outputhandler.ImageUrl;
                }
                var mapped = new AutoMapper<EventDTO, Event>().MapToObject(eventDTO);
                await _eventRepository.UpdateAsync(mapped);
                await _eventRepository.SaveChangesAsync();

                if (eventDTO.OldImageUrl == null)
                {

                }
                else
                {
                    if (eventDTO.ImgBytes == null) //if Byte[] is null means image is not being updated 
                    {

                    }
                    else // only delete if artwork is not null meaning image is being updated 
                    //delete old file
                    {
                        var outputHandler = await FileHandler.DeleteFileFromFolder(eventDTO.OldImageUrl, FolderName);
                        if (outputHandler.IsErrorOccured) //True means Delete was not successful for some reason
                        {
                            return new OutputHandler
                            {
                                IsErrorKnown = true,
                                IsErrorOccured = true,
                                Message = "Event Details updated successfully, but deleting of old file failed, please alert Techarch Team"
                            };
                        }
                    }
                }
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = "event Updated Successfully"
                };

            }
            catch (Exception ex)
            {
                return StandardMessages.getExceptionMessage(ex);

            }

        }
    }
}
