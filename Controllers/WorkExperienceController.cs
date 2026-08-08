using JobTracker.API.Data;
using JobTracker.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WorkExperienceController : ControllerBase
{
    private readonly JobTrackerContext _context;
    private readonly AuthenticatedUserService _service;
    
    public WorkExperienceController(JobTrackerContext context)
    {
        _context = context;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<WorkExperience>> GetWorkExperienceById(int id)
    {
        var userid = _service.GetLoggedInUserId();
        
        var workExperience = await _context.WorkExperiences
            .FirstOrDefaultAsync(j => j.UserId == userid && j.WorkExperienceId == id);

        if (workExperience == null)
        {
            return NotFound();
        }

        return workExperience;

    }
    
    [HttpPost("PostWorkExperience")]
    public async Task<ActionResult<WorkExperience>> PostWorkExperience(WorkExperienceDto workExperienceDto)
    {
        var userId = _service.GetLoggedInUserId();
        var workExperience = new WorkExperience();
        
        workExperience.UserId = userId;
        workExperience.CompanyName = workExperienceDto.CompanyName;
        workExperience.JobTitle = workExperienceDto.JobTitle;
        workExperience.Description = workExperienceDto.Description;
        workExperience.Start = workExperienceDto.Start;
        workExperience.End = workExperienceDto.End;

        _context.WorkExperiences.Add(workExperience);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetWorkExperienceById), new{id = workExperience.WorkExperienceId}, workExperience);
    }

}