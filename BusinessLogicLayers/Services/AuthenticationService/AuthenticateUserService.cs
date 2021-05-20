using DataAccessLayer.Authentication.JSONWebToken;
using DataAccessLayer.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.AutoMapper;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.AuthenticationService
{
    public class AuthenticateUserService : IAuthenticateUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly JwtSettings _jwtSettings;
        public AuthenticateUserService(UserManager<User> userManager,
            RoleManager<Role> roleManage,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _roleManager = roleManage;
            _userManager = userManager;
        }
        public async Task<OutputHandler> SignUp(UserSignUpResource userSignUpResource)
        {
            var user = new AutoMapper<UserSignUpResource, User>().MapToObject(userSignUpResource);
            userSignUpResource.Email = user.Email;
            var userCreateResult = await _userManager.CreateAsync(user, userSignUpResource.Password);

            if (userCreateResult.Succeeded)
            {
                return new OutputHandler
                {
                    IsErrorOccured = false
                };

            }

            return new OutputHandler { IsErrorOccured = true, Message = userCreateResult.Errors.First().Description };

            // Problem(userCreateResult.Errors.First().Description, null, 500);
        }

        public async Task<OutputHandler> SignIn(UserLoginResource userLoginResource)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userLoginResource.Email);
            if (user is null)
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    Message = "Email or password incorrect.Please Check and try again"
                };
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, userLoginResource.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = $"{user.Email}",
                    Result = GenerateJwt(user, roles)
                };
            }

            return new OutputHandler
            { 
                IsErrorOccured = true, 
                Message = "Email or password incorrect.Please Check and try again"
            };
        }

        public async Task<OutputHandler> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return new OutputHandler
                {
                    IsErrorOccured = true,
                    IsErrorKnown = true,
                    Message = "Role name should be provided."
                };

            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    IsErrorKnown = true,
                    Message = "New role has been successfully created"
                };
            }

            return new OutputHandler
            {
                IsErrorOccured = true,
                IsErrorKnown = true,
                Message = roleResult.Errors.First().Description
            };

        }


        public async Task<OutputHandler> AddUserToRole(string userEmail, string roleName)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == userEmail);

            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (result.Succeeded)
            {
                return new OutputHandler
                {
                    IsErrorOccured = false,
                    Message = $"{user.FirstName} {user.LastName} has been added to {roleName}"
                };

            }

            return new OutputHandler
            {
                IsErrorOccured = true,
                IsErrorKnown = true,
                Message = result.Errors.First().Description
            };


        }

        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
