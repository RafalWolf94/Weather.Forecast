using System.Security.Claims;
using Core.Domain.Authorization;

namespace Weather.Forecast.TechnicalStuff.Authorization;

public class AuthenticatedUserService : IAuthenticatedUserService
{
    public Guid UserId { get; }

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        if (Guid.TryParse(httpContextAccessor.HttpContext?.User.FindFirstValue("uid"), out Guid userId))
        {
            UserId = userId;
        }
    }
}