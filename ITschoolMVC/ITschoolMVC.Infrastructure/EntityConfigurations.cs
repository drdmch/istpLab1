using ITschoolMVC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ITschoolMVC.Infrastructure.EntityConfigurations;

internal class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Role");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired().HasMaxLength(50);
        builder.Property(r => r.Permission).HasMaxLength(255);
    }
}
internal class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(255);
        builder.HasIndex(u => u.Email).IsUnique();
        builder.Property(u => u.FirstName).HasMaxLength(255);
        builder.Property(u => u.LastName).HasMaxLength(255);
        builder.Property(u => u.About).HasMaxLength(1000);

        builder.HasOne(u => u.Role)
               .WithMany(r => r.Users)
               .HasForeignKey(u => u.RoleId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
internal class CourseLevelEntityTypeConfiguration : IEntityTypeConfiguration<CourseLevel>
{
    public void Configure(EntityTypeBuilder<CourseLevel> builder)
    {
        builder.ToTable("CourseLevels");
        builder.HasKey(cl => cl.Id);
        builder.Property(cl => cl.Name).IsRequired().HasMaxLength(100);
    }
}

internal class ProgrammingLanguageEntityTypeConfiguration : IEntityTypeConfiguration<ProgrammingLanguage>
{
    public void Configure(EntityTypeBuilder<ProgrammingLanguage> builder)
    {
        builder.ToTable("ProgrammingLanguage");
        builder.HasKey(pl => pl.Id);
        builder.Property(pl => pl.Name).IsRequired().HasMaxLength(100);
    }
}

internal class CourseEntityTypeConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Course");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Title).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Description).HasMaxLength(1000);
        builder.Property(c => c.Price).HasPrecision(18, 2);

        builder.HasOne(c => c.Level)
               .WithMany(l => l.Courses)
               .HasForeignKey(c => c.LevelId);

        builder.HasOne(c => c.Language)
               .WithMany(lang => lang.Courses)
               .HasForeignKey(c => c.LanguageId);
    }
}

internal class LessonEntityTypeConfiguration : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lesson");
        builder.HasKey(l => l.Id);
        builder.Property(l => l.Title).IsRequired().HasMaxLength(255);
        builder.Property(l => l.VideoUrl).HasMaxLength(1000);

        builder.HasOne(l => l.Course)
               .WithMany(c => c.Lessons)
               .HasForeignKey(l => l.CourseId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}


internal class EnrollmentEntityTypeConfiguration : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");
        builder.HasKey(e => e.Id);

        builder.HasOne(e => e.User)
               .WithMany(u => u.Enrollments)
               .HasForeignKey(e => e.UserId);

        builder.HasOne(e => e.Course)
               .WithMany(c => c.Enrollments)
               .HasForeignKey(e => e.CourseId);
    }
}

internal class CoursePriceHistoryEntityTypeConfiguration : IEntityTypeConfiguration<CoursePriceHistory>
{
    public void Configure(EntityTypeBuilder<CoursePriceHistory> builder)
    {
        builder.ToTable("CoursePriceHistory");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.OldPrice).HasPrecision(18, 2);
        builder.Property(h => h.NewPrice).HasPrecision(18, 2);

        builder.HasOne(h => h.Course)
               .WithMany(c => c.PriceHistories)
               .HasForeignKey(h => h.CourseId);
    }
}