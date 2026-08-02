using System.Text.RegularExpressions;
using JobTracker.API.Data;
using JobTracker.API.Dtos;
using JobTracker.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("controller")]

public class AuthController : ControllerBase
{
    public readonly JobTrackerContext _context;

    public AuthController(JobTrackerContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<string>> Register(RegisterDto register)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(j => j.Email == register.Email);
        
        if (existingUser != null)
        {
            return Forbid();
        }
        
        var user = new User();

        var hasher = new PasswordHasher<User>();
        string hashedPassword = hasher.HashPassword(user, register.Password);
        
        user.FirstName = register.FirstName;
        user.LastName = register.LastName;
        user.Email = register.Email;
        user.PasswordHash = hashedPassword;
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered.");
    }
}