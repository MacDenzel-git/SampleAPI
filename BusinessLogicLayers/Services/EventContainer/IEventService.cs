using DataAccessLayer.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.EventContainer
{
   public interface IEventService
    {
        Task<OutputHandler> GetAllEvents();
        Task<OutputHandler> GetAllEventsForAdmin();
        Task<OutputHandler> CreateEvent(EventDTO eventsDTO);
        Task<OutputHandler> UpdateEvent(EventDTO eventDTO);
        Task<OutputHandler> DeleteEvent(int eventId);
        Task<OutputHandler> GetEvent(int eventId);
    }
}
