using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using RealEstate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<Request> Requests { get; }
    public DbSet<UnitSoldout> UnitSoldouts { get; }

    DbSet<Unit> Units { get; }
    DbSet<Project> Projects { get; }
    DbSet<Location> Locations { get; }
    DbSet<Facility> Facilities { get; }
    DbSet<UnitFacility> UnitFacilities { get; }
    DbSet<UnitImage> UnitImages { get; }
    DbSet<ProjectImage> ProjectImages { get; }
    DbSet<Aplicant> Applicants { get; }
    DbSet<Lead> Leads { get; }
    DbSet<Deal> Deals { get; }
    DbSet<PaymentPlan> PaymentPlans { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<ApplicationUser> Users { get; }
    DbSet<IdentityRole> Roles { get; }
    DbSet<IdentityUserRole<string>> UserRoles { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
