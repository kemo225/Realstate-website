using System;
using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

   
    public bool IsDeleted { get; set; }
}
