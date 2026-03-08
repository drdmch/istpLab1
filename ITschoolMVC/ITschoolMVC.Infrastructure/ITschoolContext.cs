using ITschoolMVC.Domain.Entities; 
using Microsoft.EntityFrameworkCore;

namespace ITschoolMVC.Infrastructure;

public class ITschoolContext : DbContext
{
    public ITschoolContext(DbContextOptions<ITschoolContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<CourseLevel> CourseLevels { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<CoursePriceHistory> PriceHistories { get; set; }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=psp;Username=postgres;Password=postgres");
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ITschoolContext).Assembly);
    }
}