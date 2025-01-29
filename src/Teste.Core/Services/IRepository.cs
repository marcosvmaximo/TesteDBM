namespace Teste.Core.Services;

public interface IRepository<TEntity>
{
    Task<IEnumerable<TEntity?>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id); 
    Task AddAsync(TEntity entity); 
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}