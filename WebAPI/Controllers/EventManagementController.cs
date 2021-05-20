using BusinessLogicLayer.Services.EventContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace projectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventManagementController : Controller
    {
        private readonly IEventService _eventService;
        public EventManagementController(IEventService eventsService)
        {
            _eventService = eventsService; 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventManagementDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateSingleEvent")]
        [Authorize]
        public async Task<IActionResult> CreateSingleEvent(EventDTO eventManagementDTO)
        {
            var output = await _eventService.CreateEvent(eventManagementDTO);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        [HttpPut("UpdateEvent")]
        [Authorize]
        public async Task<IActionResult> Update(EventDTO events)
        {
            var output = await _eventService.UpdateEvent(events);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteEvent")]
        [Authorize]
        public async Task<IActionResult> Delete(int eventId)
        {
            var output = await _eventService.DeleteEvent(eventId);
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetEvents")]
        public async Task<IActionResult> GetEvents()
        {
            var output = await _eventService.GetAllEvents();
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }
        
        [HttpGet("GetAllEventsForAdmin")]
        public async Task<IActionResult> GetAllEventsForAdmin()
        {
            var output = await _eventService.GetAllEventsForAdmin();
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }

        [HttpGet("GetEvent")]
        public async Task<IActionResult> GetEvent(int eventId)
        {
            var output = await _eventService.GetEvent(eventId);
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }
    }
}
