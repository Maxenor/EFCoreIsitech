using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EFCoreIsitech.Data.Services;

/// <summary>
/// Service that provides information about the current user for auditing
/// </summary>
public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetCurrentUsername()
    {
        // In a real application with authentication, you would get the username from claims
        // return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name);
        
        // For demo purposes, we'll just return a fixed value if no authenticated user
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";
    }
}