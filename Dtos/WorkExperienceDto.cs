namespace JobTracker.API.Dtos;

public class WorkExperienceDto
{
    public string CompanyName { get; set; }
    public string JobTitle { get; set; }
    public string? Description { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
}