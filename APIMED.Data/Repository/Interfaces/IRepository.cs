using APIMED.Domain.Entities;
using System.Linq.Expressions;

namespace APIMED.Data.Repository.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        Task Adicionar(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<List<TEntity>> ObterTodos();
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
        Task<int> SaveChanges();
    }
}