namespace Omoqo.Challenge.Api.Core.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity  : class
{
    protected readonly OmoqoContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(OmoqoContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<Result<int>> CountAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return new Result<int>(await query.CountAsync());
        }
        catch (Exception ex)
        {
            return new Result<int>(ex);
        }
    }

    public async Task<Result<IEnumerable<TEntity>>> ListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return new Result<IEnumerable<TEntity>>(await query.ToListAsync());
        }
        catch (Exception ex)
        {
            return new Result<IEnumerable<TEntity>>(ex);
        }
    }

    public async Task<Result<IEnumerable<TEntity>>> ListPartialAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take)
    {
        try
        {
            IQueryable<TEntity> query = _dbSet;

            if (predicate != null)
                query = query.Where(predicate);

            return new Result<IEnumerable<TEntity>>(await query.Skip(skip).Take(take).ToListAsync());
        }
        catch (Exception ex)
        {
            return new Result<IEnumerable<TEntity>>(ex);
        }
    }

    public async Task<Result<TEntity?>> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        try
        {
            return new Result<TEntity?>(await _dbSet.SingleOrDefaultAsync(predicate));
        }
        catch (Exception ex)
        {
            return new Result<TEntity?>(ex);
        }
    }

    public async Task<Result<TEntity?>> SingleOrDefaultAsync(int id)
    {
        try
        {
            return new Result<TEntity?>(await _dbSet.FindAsync(id));
        }
        catch (Exception ex)
        {
            return new Result<TEntity?>(ex);
        }
    }

    public Result Add(TEntity entity)
    {
        try
        {
            _dbSet.Add(entity);

            return new Result();
        }
        catch (Exception ex)
        {
            return new Result<object>(ex);
        }
    }

    public Result Remove(TEntity entity)
    {
        try
        {
            _dbSet.Remove(entity);

            return new Result();
        }
        catch (Exception ex)
        {
            return new Result(ex);
        }
    }

    public Result Update(TEntity entity)
    {
        try
        {
            _dbSet.Update(entity);

            return new Result();
        }
        catch (Exception ex)
        {
            return new Result(ex);
        }
    }
}