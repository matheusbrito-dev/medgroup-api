using APIMED.Data.Data;
using APIMED.Data.Repository.Interfaces;
using APIMED.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace APIMED.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly ApiMedDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(ApiMedDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }
        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }
        public virtual async Task Atualizar(TEntity entity)
        {
            Db.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
            await SaveChanges();
        }
        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            try
            {
                return await Db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var x = e.Message;
                throw;
            }
        }
        public void Dispose()
        {
            Db?.DisposeAsync();
        }
    }
}