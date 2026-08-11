namespace JobTracker.API.Dtos;

public class ProjectDto
{
    public string ProjectName { get; set; }
    public string Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}