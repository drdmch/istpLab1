namespace ITschoolMVC.Domain.Entities;

public class UserProfile : Entity
{
    public int UserId { get; set; }
    public string FullName { get; set; }
    public string About { get; set; }
}