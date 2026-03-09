// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// namespace ITschoolMVC.Domain.Entities;

// public abstract class Entity 
// {
//     [Key]
//     [Column("id")] 
//     public int Id { get; set; } 
// }

using System;
using System.Collections.Generic;

namespace ITschoolMVC.Domain.Entities;
public abstract class Entity {
    public int Id { get; set; } 
}