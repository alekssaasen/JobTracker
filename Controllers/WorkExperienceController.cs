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
    
    

}