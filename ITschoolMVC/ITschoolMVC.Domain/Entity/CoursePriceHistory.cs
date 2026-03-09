// using System.ComponentModel.DataAnnotations.Schema;

// namespace ITschoolMVC.Domain.Entities;

// [Table("coursepricehistory")]
// public class CoursePriceHistory : Entity
// {
//     [Column("courseid")] public int? CourseId { get; set; }
//     [Column("oldprice")] public decimal? OldPrice { get; set; }
//     [Column("newprice")] public decimal? NewPrice { get; set; }
//     [Column("changedat")] public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

//     public virtual Course? Course { get; set; }
// }

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