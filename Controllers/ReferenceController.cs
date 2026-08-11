using JobTracker.API.Data;
using JobTracker.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ReferenceController
{
    private readonly JobTrackerContext _context;
    private readonly AuthenticatedUserService _service;

    public ReferenceController(JobTrackerContext context, AuthenticatedUserService service)
    {
        _context = context;
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Reference>> GetReferenceById(int id)
    {
        var userId = _service.GetLoggedInUserId();

        var reference = await _context.References
            .FirstOrDefaultAsync(j => j.ReferenceId == id && j.UserId == userId);

        if (reference == null)
        {
            return NotFound();
        }

        return reference;
    }

    [HttpPost("PostReference")]
    public async Task<ActionResult<Reference>> PostNewReference(ReferenceDto referenceDto)
    {
        var userId = _service.GetLoggedInUserId();

        var reference = new Reference();

        reference.UserId = userId;
        reference.FirstName = referenceDto.FirstName;
        reference.LastName = referenceDto.LastName;
        reference.JobTitle = referenceDto.JobTitle;
        reference.Email = referenceDto.Email;
        reference.PhoneNumber = referenceDto.PhoneNumber;

        _context.References.Add(reference);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetReferenceById), new { id = reference.ReferenceId }, reference);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Reference>> UpdateReference(int id, ReferenceDto referenceDto)
    {
        var userId = _service.GetLoggedInUserId();
        
        var reference = await _context.References
            .FirstOrDefaultAsync(j => j.ReferenceId == id && j.UserId == userId);

        if (reference == null)
        {
            return NotFound();
        }
        
        reference.FirstName = referenceDto.FirstName;
        reference.LastName = referenceDto.LastName;
        reference.JobTitle = referenceDto.JobTitle;
        reference.Email = referenceDto.Email;
        reference.PhoneNumber = referenceDto.PhoneNumber;

        _context.References.Update(reference);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}