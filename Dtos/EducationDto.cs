namespace JobTracker.API.Dtos;

public class EducationDto
{
    public string SchoolName { get; set; } 
    public string? Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime End { get; set; }
}