using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITschoolMVC.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public string Email { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public int? RoleId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }

    [NotMapped]
    public string FullName => $"{FirstName} {LastName}".Trim();

    public virtual Role? Role { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}