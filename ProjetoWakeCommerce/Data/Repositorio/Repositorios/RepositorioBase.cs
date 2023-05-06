
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjetoWakeCommerce.Repositorio.Interfaces;
using System.Linq.Expressions;
using static ProjetoWakeCommerce.Data.ConexaoDataBase;
namespace ProjetoWakeCommerce.Repositorio.Repositorios
{
    public class RepositorioBase<T> : IRepositorioBase<T>, IDisposable where T : class
    {
        protected DataContext _ctx = new DataContext();
        public RepositorioBase(DataContext ctx)
        {
            _ctx = ctx;
        }

        protected RepositorioBase()
        {
            DbContextOptionsBuilder options = new DbContextOptionsBuilder();
            var connection = Connection();
            options.UseSqlServer(connection);
            _ctx = new DataContext(options.Options);
        }

        public IQueryable<T> GetTodos()
        {
            return _ctx.Set<T>();
        }

        public IQueryable<T> Get(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Where(predicate);
        }

        public T Find(params object[] key)
        {
            return _ctx.Set<T>().Find(key);
        }

        public T First(Expression<Func<T, bool>> predicate)
        {
            return _ctx.Set<T>().Where(predicate).FirstOrDefault();
        }

        public void Adicionar(T entity)
        {
            _ctx.Set<T>().Add(entity);
        }

        public void Atualizar(T entity)
        {
            _ctx.Entry(entity).State = EntityState.Modified;
        }

        public void Deletar(Func<T, bool> predicate)
        {
            _ctx.Set<T>()
           .Where(predicate).ToList()
           .ForEach(del => _ctx.Set<T>().Remove(del));
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
