using JobTracker.API.Data;
using JobTracker.API.Models;
using JobTracker.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]

public class SkillController : ControllerBase
{
    public readonly JobTrackerContext _context;
    public readonly AuthenticatedUserService _service;
    public SkillController(JobTrackerContext context, AuthenticatedUserService service)
    {
        _context = context;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Skill>> GetSkillById(int id)
    {
        var userId = _service.GetLoggedInUserId();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(j => j.UserId == userId && j.SkillId == id);

        if (skill == null)
        {
            return NotFound();
        }

        return skill;
    }
}