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