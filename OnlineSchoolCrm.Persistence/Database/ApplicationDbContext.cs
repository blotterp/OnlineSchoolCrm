using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using OnlineSchoolCrm.Domain.Crm;
using OnlineSchoolCrm.Domain.Tenant;

namespace OnlineSchoolCrm.Persistence.Database;
public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }

    public DbSet<Tenant> Tenants => Set<Tenant>();

    public DbSet<Lead> Leads => Set<Lead>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureTenant(modelBuilder);

        ConfigureLead(modelBuilder);
    }

    private static void ConfigureTenant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(builder =>
        {
            builder.ToTable("tenants");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
            .HasColumnName("id");
            builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired();

            builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

            builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");
        });
    }

    private static void ConfigureLead(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lead>(builder =>
        {
            builder.ToTable("leads");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
            .HasColumnName("id");

            builder.Property(x => x.ParentName)
            .HasColumnName("parent_name")
            .HasMaxLength(200)
            .IsRequired();

            builder.Property(x => x.Phone)
            .HasColumnName("phone")
            .HasMaxLength(20)
            .IsRequired();
            builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired();

            builder.Property(x => x.ChildName)
            .HasColumnName("child_name")
            .HasMaxLength(200);

            builder.Property(x => x.ChildAge)
            .HasColumnName("child_age");

            builder.Property(x => x.CourseInterest)
            .HasColumnName("course_interest")
            .HasMaxLength(200);

            builder.Property(x => x.Status)
            .HasColumnName("status")
            .HasConversion<int>()
            .IsRequired();

            builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

            builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");

            builder.HasIndex(x => x.TenantId);
            builder.HasIndex(x => x.Phone);
            builder.HasIndex(x => x.Status);


        });

    }
}