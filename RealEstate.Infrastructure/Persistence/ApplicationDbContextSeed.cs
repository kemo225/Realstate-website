using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;

namespace RealEstate.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedDefaultUserAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        var adminRole = new IdentityRole("Admin");

        if (roleManager.Roles.All(r => r.Name != adminRole.Name))
        {
            await roleManager.CreateAsync(adminRole);
        }


        const string adminEmail = "admin@realestate.com";
        var administrator = await userManager.FindByEmailAsync(adminEmail);

        if (administrator == null)
        {
            administrator = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FirstName = "System",
                LastName = "Admin"            };

            await userManager.CreateAsync(administrator, "Admin123!");
        }

        if (!await userManager.IsInRoleAsync(administrator, adminRole.Name!))
        {
            await userManager.AddToRoleAsync(administrator, adminRole.Name!);
        }

    }

    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        var createdById = await context.Users
            .Where(u => u.Email == "admin@realestate.com")
            .Select(u => u.Id)
            .FirstOrDefaultAsync();

        if (!context.Locations.Any())
        {
            context.Locations.AddRange(new List<Location>
            {
                new Location { City = "Cairo", District = "New Cairo", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Location { City = "Cairo", District = "Maadi", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Location { City = "Giza", District = "Sheikh Zayed", CreatedAt = DateTime.UtcNow, CreatedById = createdById }
            });
            await context.SaveChangesAsync();
        }

        if (!context.Facilities.Any())
        {
            context.Facilities.AddRange(new List<Facility>
            {
                new Facility { Name = "Air Conditioning", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Facility { Name = "Swimming Pool", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Facility { Name = "Gym", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Facility { Name = "Parking", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Facility { Name = "Security", CreatedAt = DateTime.UtcNow, CreatedById = createdById }
            });

            await context.SaveChangesAsync();
        }

        if (!context.Owners.Any())
        {
            context.Owners.AddRange(new List<Owner>
            {
                new Owner { FullName = "John Doe", Phone = "0123456789", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Owner { FullName = "Jane Smith", Phone = "0987654321", CreatedAt = DateTime.UtcNow, CreatedById = createdById }
            });
            await context.SaveChangesAsync();
        }

        if (!context.Projects.Any())
        {
            context.Projects.AddRange(new List<Project>
            {
                new Project { Name = "Palm Hills", Description = "Luxury complex", CreatedAt = DateTime.UtcNow, CreatedById = createdById },
                new Project { Name = "Mountain View", Description = "Modern living", CreatedAt = DateTime.UtcNow, CreatedById = createdById }
            });
            await context.SaveChangesAsync();
        }
    }
}
