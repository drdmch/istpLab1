using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class CoursePriceHistory : Entity
{
    public int? CourseId { get; set; }
    public decimal? OldPrice { get; set; }
    public decimal? NewPrice { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    public virtual Course? Course { get; set; }
}