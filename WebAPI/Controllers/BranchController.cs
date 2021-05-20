using BusinessLogicLayer.Services.BranchServiceContainer;
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
    public class BranchController : ControllerBase
    {
        readonly IBranchService _branchService;
        public BranchController(IBranchService service)
        {
            _branchService = service;
        }

        /// <summary>
        /// This API gets all branches available
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetBranches")]
        
        public async Task<IActionResult> GetAllBranches()
        {
            var output = await _branchService.GetAllBranches();
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
        [HttpGet("GetBranchDetailsByTeamMemberId")]
        public async Task<IActionResult> GetBranchDetailsByTeamMember(int teamMemberId)
        {
            var output = await _branchService.GetBranchDetailsByTeamMemberId(teamMemberId);
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
        /// <param name="branchId"></param>
        /// <returns></returns>
        [HttpGet("GetBranchDetailsByBranchId")]
        public async Task<IActionResult> GetBranchDetailsByBranchId(int branchId)
        {
            var output = await _branchService.GetBranchDetailsByBranchId(branchId);
            if (output == null)
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
