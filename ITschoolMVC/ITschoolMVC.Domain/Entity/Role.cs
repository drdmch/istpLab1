// using System.ComponentModel.DataAnnotations.Schema;

// namespace ITschoolMVC.Domain.Entities;

// [Table("role")]
// public class Role : Entity
// {
//     [Column("name")] public string Name { get; set; } = null!;
//     [Column("permission")] public string? Permission { get; set; }
//     public virtual ICollection<User> Users { get; set; } = new List<User>();
// }

using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; } = null!;
    public string? Permission { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}