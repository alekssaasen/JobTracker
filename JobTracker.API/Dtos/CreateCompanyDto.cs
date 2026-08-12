namespace JobTracker.API.Dtos;

public class CreateCompanyDto
{
    public string CompanyName { get; set; }
    public string RecruiterName { get; set; }
    public string RecruiterEmail { get; set; }
    public string RecruiterPhone { get; set; }
    public DateTime? ApplicationDeadline { get; set; }
}