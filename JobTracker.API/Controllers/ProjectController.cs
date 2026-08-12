using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobTracker.API.Data;
using JobTracker.API.Dtos;
using JobTracker.API.Models;
using JobTracker.API.Service;
using Microsoft.AspNetCore.Authorization;

namespace JobTracker.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly JobTrackerContext _context;
        private readonly AuthenticatedUserService _service;

        public ProjectController(JobTrackerContext context, AuthenticatedUserService service)
        {
            _context = context;
            _service = service;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectById(int id)
        {
            var userId = _service.GetLoggedInUserId();
            
            var project = await _context.Projects.FirstOrDefaultAsync(j => j.UserId == userId && j.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }

            return project;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProject(int id, ProjectDto projectDto)
        {
            var userId = _service.GetLoggedInUserId();

            var project = await _context.Projects.FirstOrDefaultAsync(j => j.UserId == userId && j.ProjectId == id);

            if (project == null)
            {
                return NotFound();
            }
            
            project.ProjectName = projectDto.ProjectName;
            project.Description = projectDto.Description;
            project.Start = projectDto.Start;
            project.End = projectDto.End;

            _context.Projects.Update(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<Project>> PostProject(ProjectDto projectDto)
        {
            var userId = _service.GetLoggedInUserId();

            var project = new Project();

            project.UserId = userId;
            project.ProjectName = projectDto.ProjectName;
            project.Description = projectDto.Description;
            project.Start = projectDto.Start;
            project.End = projectDto.End;
            
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.ProjectId }, project);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var userId = _service.GetLoggedInUserId();
            
            var project = await _context.Projects.FirstOrDefaultAsync(j => j.UserId == userId && j.ProjectId == id);
            
            if (project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
