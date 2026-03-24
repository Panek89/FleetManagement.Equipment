using FleetManagement.Equipment.Domain.Entities;

namespace FleetManagement.Equipment.Domain.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
  Task<IEnumerable<T>> GetAllAsync(CancellationToken token);
  Task<T?> GetByIdAsync(Guid id, CancellationToken token);
  Task AddAsync(T entity, CancellationToken token);
  void Update(T entity);
  void Delete(T entity);
  Task SaveChangesAsync(CancellationToken token);
  Task<bool> ExistsByIdAsync(Guid id, CancellationToken token);
}
