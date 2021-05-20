using BusinessLogicLayer.Services.PositionsContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        readonly IPositionService _service;
        public PositionController(IPositionService service)
        {
            _service = service;
        }


        /// <summary>
        /// This api is used to get positions
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllPositions")]
        public async Task<IActionResult> GetAllPositions()
        {
            var output = await _service.GetAllPosition();
            if (output == null)
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
        /// <param name="sermonCategory"></param>
        /// <returns></returns>
        [HttpPost("CreatePosition")]
        [Authorize]
        public async Task<IActionResult> Create(PositionDTO sermonCategory)
        {
            var output = await _service.CreatePosition(sermonCategory);
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
        /// <param name="positionDTO"></param>
        /// <returns></returns>
        [HttpPut("UpdatePosition")]
        [Authorize]
        public async Task<IActionResult> Update(PositionDTO positionDTO)
        {
            var output = await _service.UpdatePosition(positionDTO);
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
        /// <param name="positionId"></param>
        /// <returns></returns>
        
        [HttpDelete("DeletePosition")]
        [Authorize]
        public async Task<IActionResult> Delete(int positionId)
        {
            var output = await _service.DeletePosition(positionId);
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output);
            }

        }

        [HttpGet("GetPosition")]
        [Authorize]
        public async Task<IActionResult> GetPosition(int positionId)
        {
            var output = await _service.GetPosition(positionId);
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output);
            }

        }
    }
}
