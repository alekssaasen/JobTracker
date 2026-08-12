using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JobTracker.API.Data;
using JobTracker.API.Dtos;
using JobTracker.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace JobTracker.API.Controllers;

[ApiController]
[Route("[controller]")]

public class AuthController : ControllerBase
{
    private readonly JobTrackerContext _context;
    private readonly IConfiguration _config;

    public AuthController(JobTrackerContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<string>> Register(RegisterDto register)
    {
        var existingUser = await _context.Users
            .FirstOrDefaultAsync(j => j.Email == register.Email);
        
        if (existingUser != null)
        {
            return BadRequest("Email already in use.");
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

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login(LoginDto login)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(j => j.Email == login.Email);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        var hasher = new PasswordHasher<User>();
        var verifyPassword = hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);

        if (verifyPassword == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Wrong password.");
        }
        
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials
            );

        var tokenHandler = new JwtSecurityTokenHandler();
        
        return tokenHandler.WriteToken(token);
    }
}