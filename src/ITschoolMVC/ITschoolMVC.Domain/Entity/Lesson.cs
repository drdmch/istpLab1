namespace ITschoolMVC.Domain.Entities;

public class Lesson : Entity
{
    public int CourseId { get; set; }
    public string Title { get; set; }
    public string VideoUrl { get; set; }
    public int OrderNumber { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}