using BusinessLogicLayer.Services.projectArmsService;
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
    public class ProjectArmController : ControllerBase
    {
        private readonly IprojectArmService _projectArmService;
        public ProjectArmController(IprojectArmService projectArmService)
        {
            _projectArmService = projectArmService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="events"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("CreateprojectArm")]
        public async Task<IActionResult> Create(projectArmDTO projectArmDTO)
        {
            var output = await _projectArmService.CreateprojectArm(projectArmDTO);
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
        /// <param name="projectArms"></param>
        /// <returns></returns>
        [HttpPut("UpdateprojectArm")]
        [Authorize]
        public async Task<IActionResult> Update(projectArmDTO projectArms)
        {
            var output = await _projectArmService.UpdateprojectArm(projectArms);
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
        /// <param name="projectArmId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteprojectArm")]
        [Authorize]
        public async Task<IActionResult> Delete(int projectArmId)
        {
            var output = await _projectArmService.DeleteprojectArm(projectArmId);
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
        /// <returns></returns>
        [HttpGet("GetprojectArms")]
        public async Task<IActionResult> GetprojectArms(bool isAdminRequest)
        {
            var output = await _projectArmService.GetAllprojectArms(isAdminRequest);
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
        /// <param name="projectArmId"></param>
        /// <returns></returns>
        [HttpGet("GetprojectArm")]
        public async Task<IActionResult> GetprojectArm(int projectArmId)
        {
            var output = await _projectArmService.GetprojectArm(projectArmId);
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
