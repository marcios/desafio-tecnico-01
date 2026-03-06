using System.Linq.Expressions;

namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface IRepository<T> where T : class
    {   
        Task<IEnumerable<T>> ObterTodosAsync(params Expression<Func<T, object>>[] includes);
        Task AdicionarAsync(T entity);
        void Atualizar(T entity);
        Task<int> SaveChagesAsync();
        void Delete(T entity);
        Task<T>ObterPorIdAsync(int  id);
    }
}
