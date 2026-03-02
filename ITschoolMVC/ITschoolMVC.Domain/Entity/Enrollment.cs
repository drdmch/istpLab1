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