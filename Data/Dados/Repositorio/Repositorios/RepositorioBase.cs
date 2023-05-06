
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WakeCommerce.Database;

namespace ProjetoWakeCommerce.Repositorio.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T>, IDisposable where T : class
    {
        protected DataContext _ctx = new DataContext();

        public RepositorioBase(DataContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IEnumerable<T>> ObterTodos()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Where(predicate);
        }

        public async Task<T> ObterPorId(int entities)
        {
            return await _ctx.Set<T>().FindAsync(entities);
        }

        public void Adicionar(T entity)
        {
            _ctx.Set<T>().Add(entity);
            Commit();
        }

        public void Atualizar(T entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
            Commit();
        }

        public void Deletar(T entity)
        {
            _ctx.Remove(entity);
            Commit();
        }

        public void Commit()
        {
            _ctx.SaveChanges();
        }

        public void Dispose()
        {
            if (_ctx != null)
            {
                _ctx.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
