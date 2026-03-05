namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface IRepository<T> where T : class
    {   
        Task<IEnumerable<T>> ObterTodosAsync();
        Task AdicionarAsync(T entity);   
        Task<int> SaveChagesAsync();
    }
}
