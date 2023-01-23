namespace Omoqo.Challenge.Api.Core.Data;

public static class DbInitializer
{
    public static void Initialize(OmoqoContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
            return;

        context.Users.Add(new User
        {
            Name = "omoqo",
            ApiKey = "43b85c2554a456572d6c8d3314ad9ad65a633878e66ba4e9d521a7faad994d52"
        });

        context.SaveChanges();
    }
}