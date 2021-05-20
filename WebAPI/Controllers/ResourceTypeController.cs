using BusinessLogicLayer.Services.ResourceTypeContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResourceTypeController : ControllerBase
    {
        private readonly IResourceTypeService _sermonTypeService;
        public ResourceTypeController(IResourceTypeService sermonTypeService)
        {
            _sermonTypeService = sermonTypeService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sermonResourceType"></param>
        /// <returns></returns>
        [HttpPost("CreateResourceType")]
        public async Task<IActionResult> Create(ResourceTypeDTO  ResourceType)
        {
            var output = await _sermonTypeService.CreateResourceType(ResourceType);
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
        /// <param name="sermonResourceType"></param>
        /// <returns></returns>
        [HttpPut("UpdateResourceType")]
        public async Task<IActionResult> Update(ResourceTypeDTO  ResourceType)
        {
            var output = await _sermonTypeService.UpdateResourceType(ResourceType);
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
        /// <param name="resourceTypeId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteResourceType")]
        public async Task<IActionResult> Delete(int resourceTypeId)
        {
            var output = await _sermonTypeService.DeleteResourceType(resourceTypeId);
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
        [HttpGet("GetAllResourceTypes")]
        public async Task<IActionResult> GetSermonResourceType()
        {
            var output = await _sermonTypeService.GetAllResourceTypes();
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResourceTypeId"></param>
        /// <returns></returns>
        [HttpGet("GetResourceType")]
        public async Task<IActionResult> GetSermonResourceType(int ResourceTypeId)
        {
            var output = await _sermonTypeService.GetResourceType(ResourceTypeId);
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
