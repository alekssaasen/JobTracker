namespace JobTracker.API.Models;

public class Education
{
    public int EducationId { get; set; }
    public string SchoolName { get; set; } 
    public string? Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}