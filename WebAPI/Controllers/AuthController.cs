using BusinessLogicLayer.Services.AuthenticationService;
using DataAccessLayer.Authentication.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace projectWebAPI.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticateUserService _authenticationService;
        public AuthController(IAuthenticateUserService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(UserSignUpResource userSignUpResource)
        {
            var output = await _authenticationService.SignUp(userSignUpResource);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }
        }


        [HttpPost("signIn")]
        public async Task<IActionResult> SignIn(UserLoginResource userLoginResource)
        {
            var output = await _authenticationService.SignIn(userLoginResource);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }
        }


        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var output = await _authenticationService.CreateRole(roleName);
            if (output.IsErrorOccured)
            {
                return BadRequest(output);
            }
            else
            {
                return Ok(output);
            }
        }

        [HttpPost("User/{userEmail}/Role")]
        public async Task<IActionResult> AddUserToRole(string userEmail, [FromBody] string roleName)
        {
            var output = await _authenticationService.AddUserToRole(userEmail,roleName);
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
