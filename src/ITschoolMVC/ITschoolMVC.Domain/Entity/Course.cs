namespace ITschoolMVC.Domain.Entities;

public class Course : Entity, IAggregateRoot
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int LevelId { get; set; }
    public int LanguageId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}