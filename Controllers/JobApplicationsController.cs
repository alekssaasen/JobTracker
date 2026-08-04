using JobTracker.API.Data;
using JobTracker.API.Dtos;
using JobTracker.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class JobApplicationsController : ControllerBase
{
    private readonly JobTrackerContext _context;
    
    public JobApplicationsController(JobTrackerContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<JobApplication>>> GetAllJobApplications()
    {
        var allJobApplications = await _context.JobApplications
            .Include(j => j.Company)
            .ToListAsync();
        
        return allJobApplications;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<JobApplication>> GetJobApplication(int id)
    {
        var jobApplication = await _context.JobApplications
            .Include(j => j.Company)
            .FirstOrDefaultAsync(i => i.JobApplicationId == id);

        if (jobApplication == null)
        {
            return NotFound();
        }

        return jobApplication;
    }

    [HttpPost]
    public async Task<ActionResult<JobApplication>> PostJobApplication(CreateJobApplicationDto createJobApplication)
    {
        var newJobApplication = new JobApplication();

        newJobApplication.JobTitle = createJobApplication.JobTitle;
        newJobApplication.CompanyId = createJobApplication.CompanyId;
        newJobApplication.DateApplied = createJobApplication.DateApplied;
        newJobApplication.Status = createJobApplication.Status;

        _context.JobApplications.Add(newJobApplication);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetJobApplication), new {id = newJobApplication.JobApplicationId}, newJobApplication);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<JobApplication>> UpdateJobApplication(int id, UpdateJobApplicationDto updateJobApplication)
    {
        var existingJobApplication = await _context.JobApplications.FindAsync(id);
        
        if (existingJobApplication == null)
        {
            return NotFound();
        }
        existingJobApplication.JobTitle = updateJobApplication.JobTitle;
        existingJobApplication.CompanyId = updateJobApplication.CompanyId;
        existingJobApplication.DateApplied = updateJobApplication.DateApplied ;
        existingJobApplication.DateInterview =  updateJobApplication.DateInterview;
        existingJobApplication.Status = updateJobApplication.Status;
        _context.JobApplications.Update(existingJobApplication);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<JobApplication>> DeleteJobApplication(int id)
    {
        var deleteJobApplication = await _context.JobApplications.FindAsync(id);
        if (deleteJobApplication == null)
        {
            return NotFound();
        }

        _context.JobApplications.Remove(deleteJobApplication);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

