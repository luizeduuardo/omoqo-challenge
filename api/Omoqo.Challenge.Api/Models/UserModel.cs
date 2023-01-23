namespace Omoqo.Challenge.Api.Models;

public class UserAuthenticateRequest
{
    public int Id { get; set; }
    public string ApiKey { get; set; } = string.Empty;
}

public class UserAuthenticateResponse
{
    public string Token { get; set; } = string.Empty;
}