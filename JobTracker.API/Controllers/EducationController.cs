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
public class EducationController : ControllerBase
{
    private readonly JobTrackerContext _context;
    private readonly AuthenticatedUserService _service;

    public EducationController(JobTrackerContext context, AuthenticatedUserService service)
    {
        _context = context;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Education>> GetEducationById(int id)
    {
        var userId = _service.GetLoggedInUserId();

        var education = await _context.Educations
            .FirstOrDefaultAsync(j => j.UserId == userId && j.EducationId == id);

        if (education == null)
        {
            return NotFound();
        }
        
        return education;
    }

    [HttpPost("PostEducation")]
    public async Task<ActionResult<Education>> PostEducation(EducationDto educationDto)
    {
        var userId = _service.GetLoggedInUserId();

        var newEducation = new Education();

        newEducation.UserId = userId;
        newEducation.SchoolName = educationDto.SchoolName;
        newEducation.Description = educationDto.Description;
        newEducation.Start = educationDto.Start;
        newEducation.End = educationDto.End;

        _context.Educations.Add(newEducation);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetEducationById), new{id = newEducation.EducationId}, newEducation);
    }
    
    [HttpPut("{id}")]

    public async Task<ActionResult<Education>> UpdateEducation(int id, EducationDto educationDto)
    {
        var userId = _service.GetLoggedInUserId();

        var education = await _context.Educations
            .FirstOrDefaultAsync(j => j.UserId == userId && j.EducationId == id);

        if (education == null)
        {
            return NotFound();
        }

        education.SchoolName = educationDto.SchoolName;
        education.Description = educationDto.Description;
        education.Start = educationDto.Start;
        education.End = educationDto.End;

        _context.Educations.Update(education);
        await _context.SaveChangesAsync();

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<Education>> DeleteEducation(int id)
    {
        var userId = _service.GetLoggedInUserId();

        var education = await _context.Educations
            .FirstOrDefaultAsync(j => j.UserId == userId && j.EducationId == id);
        
        if (education == null)
        {
            return NotFound();
        }

        _context.Educations.Remove(education);
        await _context.SaveChangesAsync();

        return NoContent();
    }
 }