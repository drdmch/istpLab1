using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class Role : Entity
{
    public string Name { get; set; }
    public string? Permission { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}