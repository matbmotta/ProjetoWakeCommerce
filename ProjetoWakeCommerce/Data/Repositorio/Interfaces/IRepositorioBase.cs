
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace ProjetoWakeCommerce.Repositorio.Interfaces
{
    public interface IRepositorioBase<T> where T : class
    {
        IQueryable<T> GetTodos();
        IQueryable<T> Get(Expression<Func<T, bool>> predicate);
        T Find(params object[] key);
        T First(Expression<Func<T, bool>> predicate);
        void Adicionar(T entity);
        void Atualizar(T entity);
        void Deletar(Func<T, bool> predicate);
        void Commit();
        void Dispose();
    }
}