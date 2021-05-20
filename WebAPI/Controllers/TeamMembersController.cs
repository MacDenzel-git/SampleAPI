using BusinessLogicLayer.Services.TeamServiceContainer;
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
    public class TeamMembersController : Controller
    {
        public readonly ITeamService _service;
        public TeamMembersController(ITeamService service)
        {
            _service = service;
        }
        [HttpGet("GetTeamMembers")]
        public async Task<IActionResult> GetTeamMembers()
        {
            var output = await _service.GetTeamMembersListAsync();
            if (output == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output);
            }
        }

        [HttpGet("GetTeamMembersForAdmin")]
        public async Task<IActionResult> GetTeamMembersForAdmin()
        {
            var output = await _service.GetTeamMembersListForAdminAsync();
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
        [HttpGet("TeamMembersByCategory")]
        public async Task<IActionResult> TeamMembersByCategory(int categoryId)
        {
            var output = await _service.GetFilteredTeamMembersListAsync(categoryId);
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
        /// <param name="teamMemberId"></param>
        /// <returns></returns>
        [HttpGet("GetTeamMemberDetails")]
        public async Task<IActionResult> GetTeamMemberDetails(long teamMemberId)
        {
            var output = await _service.GetTeamMember(teamMemberId);
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
        /// This API allows the creation of a teamMember record
        /// </summary>
        /// <param name="teamMember"></param>
        /// <returns></returns>
        [HttpPost("AddTeamMember")]
        [Authorize]
        public async Task<IActionResult> AddTeamMember(TeamMembersDTO teamMember)
        {
            var output = await _service.AddTeamMemberAsync(teamMember);
            if (output.IsErrorOccured == true)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }
        }

        [HttpPost("EditTeamMember")]
        [Authorize]
        public async Task<IActionResult> EditTeamMember(TeamMembersDTO teamMember)
        {
            var output = await _service.EditTeamMember(teamMember);
            if (output.IsErrorOccured == true)
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
        /// <param name="teamMemberId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteTeamMember")]
        [Authorize]
        public async Task<IActionResult> Delete(int teamMemberId)
        {
            var output = await _service.DeleteTeamMember(teamMemberId);
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
