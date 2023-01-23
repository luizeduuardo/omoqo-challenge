namespace Omoqo.Challenge.Api.Commons;

public class ApiAppSettings
{
    public int JwtExpirationSeconds { get; set; }
    public string JwtSecretKey { get; set; } = string.Empty;
}