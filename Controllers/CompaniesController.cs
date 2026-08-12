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

public class CompaniesController : ControllerBase
{
    private readonly JobTrackerContext _context;

    public CompaniesController(JobTrackerContext context)
    {
        _context = context;
    }

    [HttpGet("GetAllCompanies")]
    public async Task<ActionResult<List<Company>>> GetAllCompanies()
    {
        var allCompanies = await _context.Company
            .AsNoTracking()
            .ToListAsync();

        return allCompanies;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Company>> GetCompanyById(int id)
    {
        var company = await _context.Company.FindAsync(id);

        if (company == null)
        {
            return NotFound();
        }
        
        return company;
    }

    [HttpPost("PostCompany")]
    public async Task<ActionResult<Company>> PostCompany(CreateCompanyDto createCompany)
    {
        var newCompany = new Company();

        newCompany.CompanyName = createCompany.CompanyName;
        newCompany.RecruiterName = createCompany.RecruiterName;
        newCompany.RecruiterEmail = createCompany.RecruiterEmail;
        newCompany.RecruiterPhone = createCompany.RecruiterPhone;
        newCompany.ApplicationDeadline = createCompany.ApplicationDeadline;
        _context.Company.Add(newCompany);
        await _context.SaveChangesAsync();
        
        return CreatedAtAction(nameof(GetCompanyById), new { id = newCompany.CompanyId }, newCompany);
    }

}