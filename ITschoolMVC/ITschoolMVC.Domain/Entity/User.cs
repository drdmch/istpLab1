using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class User : Entity, IAggregateRoot
{
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int? RoleId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }

    public virtual Role? Role { get; set; }
    public virtual ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}