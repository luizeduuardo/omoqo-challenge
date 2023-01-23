namespace Omoqo.Challenge.Api.Core.Data;

public class OmoqoContext : DbContext
{
    public OmoqoContext(DbContextOptions<OmoqoContext> options) : base(options)
    {
    }

    public DbSet<Ship> Ships { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}