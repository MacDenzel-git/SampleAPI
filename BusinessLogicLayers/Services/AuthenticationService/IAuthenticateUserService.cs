using DataAccessLayer.Authentication.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TechArchDataHandler.General;

namespace BusinessLogicLayer.Services.AuthenticationService
{
    public interface IAuthenticateUserService
    {
        Task<OutputHandler> SignUp(UserSignUpResource userSignUpResource);
        Task<OutputHandler> SignIn(UserLoginResource userLoginResource);
        Task<OutputHandler> CreateRole(string roleName);
        Task<OutputHandler> AddUserToRole(string userEmail, string roleName);
    }
}
