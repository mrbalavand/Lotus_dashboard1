using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Lotus_Dashboard1.Areas.Identity.Data;

// Add profile data for application users by adding properties to the Lotus_Dashboard1User class
public class Lotus_Dashboard1User : IdentityUser
{
    public string Fname1 { get; set; }

    public string Password1 { get; set; }


    public string NationalCode1 { get; set; }
}

