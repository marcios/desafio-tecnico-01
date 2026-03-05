namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface IRepository<T> where T : class
    {   
        Task<IEnumerable<T>> ObterTodosAsync();
        Task AdicionarAsync(T entity);
        void Atualizar(T entity);
        Task<int> SaveChagesAsync();

        void Delete(T entity);

        Task<T>ObterPorIdAsync(int  id);    
    }
}
