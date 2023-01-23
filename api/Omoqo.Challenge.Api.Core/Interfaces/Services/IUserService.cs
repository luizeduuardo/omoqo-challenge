namespace Omoqo.Challenge.Api.Core.Interfaces.Services;

public interface IUserService
{
    Task<Result<User>> AuthenticateAsync(UserAuthenticateModel model);
}