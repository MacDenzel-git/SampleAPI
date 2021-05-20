using BusinessLogicLayer.Services.SermonCategoryContainer;
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
    public class ResourceCategoryController : Controller
    {
        private readonly IResourceCategoryService _sermonCategoryService;
        public ResourceCategoryController(IResourceCategoryService sermonCategoryService)
        {
            _sermonCategoryService = sermonCategoryService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sermonCategory"></param>
        /// <returns></returns>
        [HttpPost("CreateCategory")]
        [Authorize]
        public async Task<IActionResult> Create(ResourceCategoryDTO sermonCategory)
        {
            var output = await _sermonCategoryService.CreateResourceCategory(sermonCategory);
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
        /// <param name="sermonCategory"></param>
        /// <returns></returns>
        [HttpPut("UpdateCategory")]
        [Authorize]
        public async Task<IActionResult> Update(ResourceCategoryDTO sermonCategory)
        {
            var output = await _sermonCategoryService.UpdateResourceCategory(sermonCategory);
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
        /// <param name="sermonSeriesId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteCategory")]
        [Authorize]
        public async Task<IActionResult> Delete(int sermonSeriesId)
        {
            var output = await _sermonCategoryService.DeleteResourceCategory(sermonSeriesId);
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
        [HttpGet("GetResourceCategories")]
        public async Task<IActionResult> GetResourceCategories()
        {
            var output = await _sermonCategoryService.GetAllResourceCategory();
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }

        [HttpGet("GetResourceCategory")]
        public async Task<IActionResult> GetResourceCategory(int CategoryId)
        {
            var output = await _sermonCategoryService.GetResourceCategory(CategoryId);
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
