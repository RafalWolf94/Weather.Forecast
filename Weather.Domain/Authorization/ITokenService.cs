using Core.Domain.Models.AppUsers;

namespace Core.Domain.Authorization;

public interface ITokenService
{
    string GetNewJwtTokenFor(AppUser appUser);
}