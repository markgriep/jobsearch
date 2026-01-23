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
                .HasColumnName("id");

            e.Property<string>(x => x.ActivityDate)
                .HasColumnName("activity_date")
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
        });
    }
}
