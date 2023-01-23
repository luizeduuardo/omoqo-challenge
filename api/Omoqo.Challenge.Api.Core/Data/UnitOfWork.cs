namespace Omoqo.Challenge.Api.Core.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly OmoqoContext _context;

    public UnitOfWork(OmoqoContext context) => _context = context;

    public int SaveChanges() => _context.SaveChanges();

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing) => _context.Dispose();
}
