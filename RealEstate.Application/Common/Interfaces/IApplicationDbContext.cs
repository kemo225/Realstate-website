using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstate.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Unit> Units { get; }
    DbSet<Project> Projects { get; }
    DbSet<Location> Locations { get; }
    DbSet<Facility> Facilities { get; }
    DbSet<UnitFacility> UnitFacilities { get; }
    DbSet<UnitImage> UnitImages { get; }
    DbSet<ProjectImage> ProjectImages { get; }
    DbSet<Owner> Owners { get; }
    DbSet<Lead> Leads { get; }
    DbSet<Deal> Deals { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
