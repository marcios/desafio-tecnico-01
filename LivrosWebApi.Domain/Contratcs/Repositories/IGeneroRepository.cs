using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ObterTodosAsync();
    }
}
