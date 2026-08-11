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

    [HttpPost("PostSkill")]
    public async Task<ActionResult<Skill>> PostSkill(SkillDto skillDto)
    {
        var userId = _service.GetLoggedInUserId();

        var newSkill = new Skill();

        newSkill.UserId = userId;
        newSkill.SkillName = skillDto.SkillName;

        _context.Skills.Add(newSkill);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSkillById), new{id = newSkill.SkillId}, newSkill);
    }

    [HttpPut("{id}")]

    public async Task<ActionResult<Skill>> UpdateSkill(int id, SkillDto skillDto)
    {
        var userId = _service.GetLoggedInUserId();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(j => j.UserId == userId && j.SkillId == id);

        if (skill == null)
        {
            return NotFound();
        }

        skill.SkillName = skillDto.SkillName;

        _context.Skills.Update(skill);
        await _context.SaveChangesAsync();

        return NoContent();

    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Skill>> DeleteSkill(int id)
    {
        var userId = _service.GetLoggedInUserId();

        var skill = await _context.Skills
            .FirstOrDefaultAsync(j => j.UserId == userId && j.SkillId == id);
        
        if (skill == null)
        {
            return NotFound();
        }

        _context.Skills.Remove(skill);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    
}