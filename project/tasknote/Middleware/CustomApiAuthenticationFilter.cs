using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

public class CustomApiAuthenticationFilter : IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Check for Authorization header
        var authHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            context.Result = new UnauthorizedResult(); // Return 401 Unauthorized
            return;
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();

        // Validate the token (custom validation logic)
        if (token != "testing") // Example validation
        {
            context.Result = new UnauthorizedResult(); // Return 401 Unauthorized
            return;
        }
    }
}