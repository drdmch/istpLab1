namespace ITschoolMVC.Domain.Entities;

public class Enrollment : Entity
{
    public int UserId { get; set; }
    public int CourseId { get; set; }
    public int Progress { get; set; }
    public DateTime EnrolledAt { get; set; }
}