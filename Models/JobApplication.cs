using System.ComponentModel.DataAnnotations;

namespace JobTracker.API.Models;

public class JobApplication
{
    public int JobApplicationId { get; set; }
    [Required]
    public string JobTitle { get; set; }
    [Required]
    public string CompanyName { get; set; }
    public DateTime DateApplied { get; set; }
    public DateTime? DateInterview { get; set; }
    public ApplicationStatus Status { get; set; }
}