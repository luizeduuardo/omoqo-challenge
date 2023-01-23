namespace Omoqo.Challenge.Api.Core.Interfaces.Data;

public interface IUnitOfWork : IDisposable
{
    int SaveChanges();
}