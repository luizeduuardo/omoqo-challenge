namespace Omoqo.Challenge.Api.Extensions;

public static class IApplicationBuilderExtension
{
    public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        using IServiceScope scope = app.ApplicationServices.CreateScope();

        OmoqoContext context = scope.ServiceProvider.GetRequiredService<OmoqoContext>();
        DbInitializer.Initialize(context);

        return app;
    }
}