namespace JobTracker.API.Models;

public class Reference
{
    public int ReferenceId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string JobTitle { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}