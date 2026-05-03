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
    public DbSet<Request> Requests => Set<Request>();
    public DbSet<UnitSoldout> UnitSoldouts => Set<UnitSoldout>();

    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Location> Locations => Set<Location>();
    public DbSet<Facility> Facilities => Set<Facility>();
    public DbSet<UnitFacility> UnitFacilities => Set<UnitFacility>();
    public DbSet<UnitImage> UnitImages => Set<UnitImage>();
    public DbSet<ProjectImage> ProjectImages => Set<ProjectImage>();
    public DbSet<Aplicant> Applicants => Set<Aplicant>();
    public DbSet<PaymentPlan> PaymentPlans => Set<PaymentPlan>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<UnitService> UnitServices => Set<UnitService>();

    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<Deal> Deals => Set<Deal>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DbSet<Developer> Developers => Set<Developer>();
    public DbSet<DeveloperGallery> DeveloperGalleries => Set<DeveloperGallery>();

    // Translation system
    public DbSet<EntityTranslation> Translations => Set<EntityTranslation>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigureAuditRelationships(builder);

        builder.Entity<Developer>().HasQueryFilter(d => !d.IsDeleted);

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
        builder.Entity<PaymentPlan>().Property(p => p.CommissionRate).HasPrecision(4, 2);
        builder.Entity<PaymentPlan>().Property(p => p.InstallmentDownPayment).HasPrecision(4, 2);


        builder.Entity<DeveloperGallery>()
    .HasOne(g => g.Developer)
    .WithMany(d => d.Gallery)
    .HasForeignKey(g => g.DeveloperId)
    .IsRequired(false);

        builder.Entity<Request>()
            .HasOne(u => u.ApprovedByUser)
            .WithMany()
            .HasForeignKey(u => u.ApprovedById)
            .OnDelete(DeleteBehavior.Restrict);

        // EntityTranslation: unique constraint prevents duplicate translations
        builder.Entity<EntityTranslation>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.ToTable("EntityTranslations");

            entity.HasIndex(t => new { t.EntityType, t.EntityId, t.FieldName, t.Language })
                  .IsUnique()
                  .HasDatabaseName("UQ_Translations_EntityType_EntityId_FieldName_Language");

            // Covering index for the most common lookup pattern
            entity.HasIndex(t => new { t.EntityType, t.EntityId, t.Language })
                  .HasDatabaseName("IX_Translations_Lookup");

            entity.Property(t => t.FieldName).HasMaxLength(64).IsRequired();
            entity.Property(t => t.Language).HasMaxLength(8).IsRequired();
            entity.Property(t => t.Value).IsRequired();
        });

    }

    private static void ConfigureAuditRelationships(ModelBuilder builder)
    {
        var auditableEntityTypes = builder.Model
            .GetEntityTypes()
            .Where(t => typeof(IAuditableEntity).IsAssignableFrom(t.ClrType));

        foreach (var entityType in auditableEntityTypes)
        {
            builder.Entity(entityType.ClrType)
                .HasOne(typeof(ApplicationUser), nameof(IAuditableEntity.CreatedByUser))
                .WithMany()
                .HasForeignKey(nameof(IAuditableEntity.CreatedById))
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.Entity(entityType.ClrType)
                .HasOne(typeof(ApplicationUser), nameof(IAuditableEntity.UpdatedByUser))
                .WithMany()
                .HasForeignKey(nameof(IAuditableEntity.UpdatedById))
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
