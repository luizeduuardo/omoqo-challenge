namespace Omoqo.Challenge.Api.Core.Interfaces.Repositories;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task<Result<int>> CountAsync(Expression<Func<TEntity, bool>> predicate);
    Task<Result<IEnumerable<TEntity>>> ListAsync(Expression<Func<TEntity, bool>> predicate);
    Task<Result<IEnumerable<TEntity>>> ListPartialAsync(Expression<Func<TEntity, bool>> predicate, int skip, int take);
    Task<Result<TEntity?>> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
    Task<Result<TEntity?>> SingleOrDefaultAsync(int id);
    Result Add(TEntity entity);
    Result Remove(TEntity entity);
    Result Update(TEntity entity);
}
