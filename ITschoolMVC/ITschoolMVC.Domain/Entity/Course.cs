using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class Course : Entity, IAggregateRoot
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int? LevelId { get; set; }
    public int? LanguageId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual CourseLevel? Level { get; set; }
    public virtual ProgrammingLanguage? Language { get; set; }
    public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
    public virtual ICollection<CoursePriceHistory> PriceHistories { get; set; } = new List<CoursePriceHistory>();
}