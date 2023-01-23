namespace Omoqo.Challenge.Api.Helpers;

public interface IJwtHelper
{
    string GenerateAccessToken(User user);
}

public class JwtHelper : IJwtHelper
{
    private readonly ApiAppSettings _apiAppSettings;

    public JwtHelper(IOptions<ApiAppSettings> apiAppSettings)
    {
        _apiAppSettings = apiAppSettings.Value;
    }

    public string GenerateAccessToken(User user)
    {
        List<Claim> claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name)
        };

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(_apiAppSettings.JwtExpirationSeconds),
            SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiAppSettings.JwtSecretKey)),
                        SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler tokenHandler = new();

        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }
}