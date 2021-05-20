using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Authentication.Models
{
    public class UserLoginResource
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
