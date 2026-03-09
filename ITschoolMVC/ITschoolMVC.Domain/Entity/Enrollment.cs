// using System.ComponentModel.DataAnnotations.Schema;

// namespace ITschoolMVC.Domain.Entities;

// [Table("enrollments")]
// public class Enrollment : Entity
// {
//     [Column("userid")] public int? UserId { get; set; }
//     [Column("courseid")] public int? CourseId { get; set; }
//     [Column("progress")] public int Progress { get; set; } = 0;
//     [Column("enrolledat")] public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

//     public virtual User? User { get; set; }
//     public virtual Course? Course { get; set; }
// }

using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class Enrollment : Entity
{
    public int? UserId { get; set; }
    public int? CourseId { get; set; }
    public int Progress { get; set; } = 0;
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    public virtual User? User { get; set; }
    public virtual Course? Course { get; set; }
}