using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace GroupSpace2023.Areas.Identity.Data;

// Add profile data for application users by adding properties to the GroupSpace2023User class
public class GroupSpace2023User : IdentityUser
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
}

