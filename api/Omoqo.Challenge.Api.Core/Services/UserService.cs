namespace Omoqo.Challenge.Api.Core.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> AuthenticateAsync(UserAuthenticateModel model)
    {
        try
        {
            if (model.Id == default)
                return new Result<User>("User Id is required");

            if (string.IsNullOrEmpty(model.ApiKey))
                return new Result<User>("API key is required");

            Result<User?> userResult = await _userRepository.SingleOrDefaultAsync(model.Id);

            if (!userResult.Success)
                return userResult!;

            if (userResult.Value == null)
                return new Result<User>("User not found");

            if (!userResult.Value.ApiKey.Equals(model.ApiKey))
                return new Result<User>("Invalid API key");

            return userResult!;
        }
        catch (Exception ex)
        {
            return new Result<User>(ex);
        }
    }
}
