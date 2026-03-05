using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface IGeneroRepository : IRepository<Genero>
    {
        Task<bool> ExistePorNomeAsync(string nome);
    }
}
