using JobTracker.API.Models;

namespace JobTracker.API.Dtos;

public class CreateJobApplicationDto
{
    public string JobTitle { get; set; }
    public string CompanyName { get; set; }
    public DateTime DateApplied { get; set; }
    public ApplicationStatus Status { get; set; }
}