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
    }
    
    

}