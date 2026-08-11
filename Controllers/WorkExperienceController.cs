using JobTracker.API.Data;
using JobTracker.API.Dtos;
using JobTracker.API.Models;
using JobTracker.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class WorkExperienceController : ControllerBase
{
    private readonly JobTrackerContext _context;
    private readonly AuthenticatedUserService _service;
    
    public WorkExperienceController(JobTrackerContext context, AuthenticatedUserService service)
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

    [HttpPut("UpdateWorkExperience")]
    public async Task<ActionResult<WorkExperience>> UpdateWorkExperience(int id, WorkExperienceDto workExperienceDto)
    {
        var userId = _service.GetLoggedInUserId();
        
        var workExperience = await _context.WorkExperiences
            .FirstOrDefaultAsync(j => j.WorkExperienceId == id && j.UserId == userId);

        if (workExperience == null)
        {
            return NotFound();
        }
        
        workExperience.CompanyName = workExperienceDto.CompanyName;
        workExperience.JobTitle = workExperienceDto.JobTitle;
        workExperience.Description = workExperienceDto.Description;
        workExperience.Start = workExperienceDto.Start;
        workExperience.End = workExperienceDto.End;

        _context.WorkExperiences.Update(workExperience);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<WorkExperience>> DeleteWorkExperience(int id)
    {
        var userId = _service.GetLoggedInUserId();

        var deleteWorkExperience = await _context.WorkExperiences
            .FirstOrDefaultAsync(j => j.WorkExperienceId == id && j.UserId == userId);

        if (deleteWorkExperience == null)
        {
            return NotFound();
        }
        
        _context.WorkExperiences.Remove(deleteWorkExperience);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}