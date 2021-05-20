using BusinessLogicLayer.Services.UtilsContainer;
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
    public class UtilsController : ControllerBase
    {
        readonly IUtilService _utilService;
        public UtilsController(IUtilService service)
        {
            _utilService = service;
        }

        /// <summary>
        /// This API gets all branches available
        /// </summary>
        /// <returns></returns>
        [HttpGet("HomePageSetup")]
        public async Task<IActionResult> HomePageSetup()
        {
            try
            {
                var output = await _utilService.SetupHomePage();
                if (output == null)
                {
                    return BadRequest();
                }
                else
                {
                    return Ok(output);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        /// <summary>
        /// This API gets all branches available
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenuItems")]
        public async Task<IActionResult> GetMenuItems()
        {
            var output = await _utilService.GetMenuItems();
            if (output == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(output);
            }
        } 
        
        [HttpGet("GetDropdownItems")]
        public async Task<IActionResult> GetDropdownItems()
        {
            var output = await _utilService.DropDownItems();
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
