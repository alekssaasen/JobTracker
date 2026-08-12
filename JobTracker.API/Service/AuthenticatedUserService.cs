using System.Security.Claims;
using JobTracker.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobTracker.API.Service;

public class AuthenticatedUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public int GetLoggedInUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return int.Parse(userId!);
    }

}

