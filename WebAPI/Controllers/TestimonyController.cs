using BusinessLogicLayer.Services.TestimonyServiceContainer;
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
    public class TestimonyController : ControllerBase
    {
        private readonly ITestimonyService _testimonyService;
        public TestimonyController(ITestimonyService testimonyService)
        {
            _testimonyService = testimonyService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="testimonyDTO"></param>
        /// <returns></returns>
        [HttpPost("CreateTestimony")]
        [Authorize]
        public async Task<IActionResult> Create(TestimonyDTO testimonyDTO)
        {
            var output = await _testimonyService.CreateTestimony(testimonyDTO);
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
        /// <param name="Qoute"></param>
        /// <returns></returns>
        [HttpPut("UpdateTestimony")]
        [Authorize]
        public async Task<IActionResult> Update(TestimonyDTO testimony)
        {
            var output = await _testimonyService.UpdateTestimony(testimony);
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
        /// <param name="sermonSeriesId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTestimony")]
        [Authorize]
        public async Task<IActionResult> Delete(int testimonyId)
        {
            var output = await _testimonyService.DeleteTestimony(testimonyId);
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
     /// <param name="isFiltered"></param>
     /// <returns></returns>
        [HttpGet("GetAllTestimonies")]
        public async Task<IActionResult> GetAllTestimonies(bool isFiltered)
        {
            var output = await _testimonyService.GetAllTestimonies(isFiltered);
            if (output.IsErrorOccured)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output.Result);
            }

        }

        [HttpGet("GetTestimony")]
        public async Task<IActionResult> GetTestimony(int testimonyId)
        {
            var output = await _testimonyService.GetTestimony(testimonyId);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output.Result);
            }

        }
    }
}
