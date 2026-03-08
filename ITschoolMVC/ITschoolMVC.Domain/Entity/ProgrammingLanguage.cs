using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; } = null!;
    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();
}