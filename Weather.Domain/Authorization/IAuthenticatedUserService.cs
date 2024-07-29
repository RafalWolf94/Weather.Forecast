namespace Core.Domain.Authorization;

public interface IAuthenticatedUserService
{
    public Guid UserId { get; }
}