using System;
using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Common;

namespace RealEstate.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

   
    public bool IsDeleted { get; set; }
}
