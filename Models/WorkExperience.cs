using System.ComponentModel.DataAnnotations;

namespace JobTracker.API.Models;

public class WorkExperience
{
    public int WorkExperienceId { get; set; }
    [Required]
    public string CompanyName { get; set; }
    [Required]
    public string JobTitle { get; set; }
    public string? Description { get; set; }
    [Required]
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}
