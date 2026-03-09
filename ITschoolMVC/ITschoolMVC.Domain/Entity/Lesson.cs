// using System.ComponentModel.DataAnnotations.Schema;

// namespace ITschoolMVC.Domain.Entities;

// [Table("lesson")]
// public class Lesson : Entity
// {
//     [Column("courseid")] public int? CourseId { get; set; }
//     [Column("title")] public string Title { get; set; } = null!;
//     [Column("videourl")] public string? VideoUrl { get; set; }
//     [Column("ordernumber")] public int OrderNumber { get; set; }

//     public virtual Course? Course { get; set; }
// }

using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class Lesson : Entity
{
    public int? CourseId { get; set; }
    public string Title { get; set; } = null!;
    public string? VideoUrl { get; set; }
    public int OrderNumber { get; set; }

    public virtual Course? Course { get; set; }
}