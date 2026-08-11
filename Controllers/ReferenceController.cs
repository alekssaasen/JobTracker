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
}