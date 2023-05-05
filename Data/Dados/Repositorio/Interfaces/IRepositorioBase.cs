
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ProjetoWakeCommerce.Repositorio.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        Task<IEnumerable<T>> ObterTodos();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        Task<T> ObterPorId(int entities);
        Task<T> Find(params object[] key);
        T First(Expression<Func<T, bool>> predicate);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(T entity);
        void Commit();
        void Dispose();
    }
}