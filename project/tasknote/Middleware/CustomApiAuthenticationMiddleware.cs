using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class CustomApiAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomApiAuthenticationMiddleware> _logger;

    public CustomApiAuthenticationMiddleware(RequestDelegate next, ILogger<CustomApiAuthenticationMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            _logger.LogWarning("Authorization header missing or invalid.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized - Missing or invalid Authorization header.");
            return;
        }

        // Extract the token from the Authorization header
        var token = authHeader.Substring("Bearer ".Length).Trim();

        if (!IsValidToken(token))
        {
            _logger.LogWarning("Invalid token.");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized - Invalid Token.");
            return;
        }

        await _next(context);
    }

    private bool IsValidToken(string token)
    {
        return token == "testing-token";
    }
}