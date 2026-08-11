namespace JobTracker.API.Models;

public class Project
{
    public int ProjectId { get; set; }
    public string ProjectName { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}