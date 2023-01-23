namespace Omoqo.Challenge.Api.Core.Extensions;

public static class IServiceCollectionExtension
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddDbContext<OmoqoContext>(opt =>
            opt.UseInMemoryDatabase("OmoqoChallenge"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IShipRepository, ShipRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddTransient<IShipService, ShipService>();
        services.AddTransient<IUserService, UserService>();

        return services;
    }
}
