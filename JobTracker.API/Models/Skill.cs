namespace JobTracker.API.Models;

public class Skill
{
    public int SkillId { get; set; }
    public string SkillName { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = null!;
}