using System.ComponentModel.DataAnnotations;

namespace JobTracker.API.Models;

public class JobApplication
{
    public int JobApplicationId { get; set; }
    [Required]
    public string JobTitle { get; set; }
    [Required]
    public int CompanyId { get; set; }
    public Company Company { get; set; } = null!;
    public DateTime DateApplied { get; set; }
    public DateTime? DateInterview { get; set; }
    public ApplicationStatus Status { get; set; }
}