using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Authentication.Models
{
    public class Role : IdentityRole<Guid>
    { }
}
