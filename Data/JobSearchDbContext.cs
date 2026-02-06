using System;
using jobsearch.Models;
using Microsoft.EntityFrameworkCore;

namespace jobsearch.Data;

public class JobSearchDbContext : DbContext
{
    public JobSearchDbContext(DbContextOptions<JobSearchDbContext> options)
        : base(options)
    {
    }

    public DbSet<ActivityLog> ActivityLogs => Set<ActivityLog>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ActivityLog>(e =>
        {
            e.ToTable("activity_log");

            e.HasKey(x => x.Id);

            e.Property<int>(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            e.Property<DateTime>(x => x.ActivityDate)
                .HasColumnName("activity_date")
                .HasColumnType("datetime2")
                .IsRequired();

            e.Property<string>(x => x.BusinessOrOrganization)
                .HasColumnName("business_or_organization")
                .IsRequired();

            e.Property<string>(x => x.ActivityPerformed)
                .HasColumnName("activity_performed")
                .IsRequired();

            e.Property<string>(x => x.TypeOfWork)
                .HasColumnName("type_of_work")
                .IsRequired();

            e.Property<string>(x => x.Results)
                .HasColumnName("results")
                .IsRequired();

            e.Property<string?>(x => x.ContactPerson)
                .HasColumnName("contact_person");

            e.Property<string?>(x => x.HowPerformed)
                .HasColumnName("how_performed");

            e.Property<string?>(x => x.Notes)
                .HasColumnName("notes");

            e.Property<bool>(x => x.ShouldBeOnDes)
                .HasColumnName("should_be_on_des")
                .HasDefaultValue(false);

            e.Property<bool>(x => x.IsOnDes)
                .HasColumnName("is_on_des")
                .HasDefaultValue(false);

            //e.Property<int>(x => x.ShouldBeOnDes)
            //    .HasColumnName("ShouldBeOnDes");

            //e.Property<int>(x => x.IsEnteredOnDes)
            //    .HasColumnName("IsEnteredOnDes");
        });
    }
}
