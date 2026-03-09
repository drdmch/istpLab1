// using System.ComponentModel.DataAnnotations.Schema;

// namespace ITschoolMVC.Domain.Entities;

// [Table("course")]
// public class Course : Entity, IAggregateRoot
// {
//     [Column("title")] public string Title { get; set; } = null!;
//     [Column("description")] public string? Description { get; set; }
//     [Column("price")] public decimal Price { get; set; }
//     [Column("levelid")] public int? LevelId { get; set; }
//     [Column("languageid")] public int? LanguageId { get; set; }
//     [Column("createdat")] public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
//     [Column("updatedat")] public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

//     public virtual CourseLevel? Level { get; set; }
//     public virtual ProgrammingLanguage? Language { get; set; }
//     public virtual ICollection<Lesson> Lessons { get; set; } = new List<Lesson>();
//     public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
//     public virtual ICollection<CoursePriceHistory> PriceHistories { get; set; } = new List<CoursePriceHistory>();
// }
using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class Course : Entity, IAggregateRoot
{
    public string Title { get; set; } = null!;
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