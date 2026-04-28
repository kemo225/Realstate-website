using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Common;
using RealEstate.Domain.Entities;
using RealEstate.Application.Common.Interfaces;

namespace RealEstate.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<UnitFacility> UnitFacilities => Set<UnitFacility>();
    public DbSet<UnitImage> UnitImages => Set<UnitImage>();
    public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<UnitDetail> UnitDetails => Set<UnitDetail>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<UnitService> UnitServices => Set<UnitService>();

    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<Deal> Deals => Set<Deal>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigureCreatedByRelationships(builder);

        builder.Entity<UnitFacility>()
            .HasKey(pf => new { pf.UnitId, pf.FacilityId });

        builder.Entity<Unit>()
            .Property(p => p.RowVersion)
            .IsRowVersion();

        builder.Entity<Deal>()
            .Property(d => d.RowVersion)
            .IsRowVersion();

    
        builder.Entity<ApplicationUser>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Unit>().Property(p => p.Price).HasPrecision(18, 2);
        builder.Entity<Location>().Property(l => l.Latitude).HasPrecision(10, 8);
        builder.Entity<Location>().Property(l => l.Longitude).HasPrecision(11, 8);
   

    }

    private static void ConfigureCreatedByRelationships(ModelBuilder builder)
    {
        var auditableEntityTypes = builder.Model
            .GetEntityTypes()
            .Where(t => typeof(IAuditableEntity).IsAssignableFrom(t.ClrType));

        foreach (var entityType in auditableEntityTypes)
        {
            builder.Entity(entityType.ClrType)
                .HasOne(typeof(ApplicationUser), nameof(BaseEntity.CreatedByUser))
                .WithMany()
                .HasForeignKey(nameof(IAuditableEntity.CreatedById))
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
