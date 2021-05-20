using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Services;
using BusinessLogicLayer.Services.ResourceContainer;
using DataAccessLayer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
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
    public class ResourceController : ControllerBase
    {
        public readonly IResourceService _service;
        public ResourceController(IResourceService service)
        {
            _service = service;
        }


        [HttpGet("GetResources")]
        public async Task<IActionResult> GetResources(string resourceType)
        {
            var output = await _service.GetResourceListAsync(resourceType);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output.Result);
            }
        }

        [HttpGet("GetFeaturedResources")]
        public async Task<IActionResult> GetFeaturedResources(string resourceType)
        {
            var output = await _service.GetFeaturedResourcesAsync(resourceType);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output.Result);
            }
        }

        [HttpGet("GetResourcesForAdmin")]
        [Authorize]
        public async Task<IActionResult> GetResourcesForAdmin()
        {
            var output = await _service.GetResourceListForAdminAsync();
            if (output == null)
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
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet("ResourcesByCategory")]
        public async Task<IActionResult> ResourcesByCategory(int categoryId)
        {
            var output = await _service.GetFilteredResourceListAsync(categoryId);
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
        /// <param name="resourceId"></param>
        /// <returns></returns>
        [HttpGet("GetResourceDetails")]
        public async Task<IActionResult> GetResourceDetails(long resourceId)
        {
            var output = await _service.GetResource(resourceId);
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
        /// This API allows the creation of a resource record
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost("AddResource")]
        [Authorize]
        public async Task<IActionResult> AddResource(ResourceDTO resource)
        {
            var output = await _service.AddResourceAsync(resource);
            if (output.IsErrorOccured == true)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }
        }
        
        [HttpPost("EditResource")]
        [Authorize]
        public async Task<IActionResult> EditResource(ResourceDTO resource)
        {
            var output = await _service.EditResource(resource);
            if (output.IsErrorOccured == true)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }
        }

        [HttpDelete("DeleteResource")]
        [Authorize]
        public async Task<IActionResult> Delete(int resourceId)
        {
            var output = await _service.DeleteResource(resourceId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }

        }
    }
}
