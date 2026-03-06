using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<bool> ExistePorNomeAsync(string nome, int? idIgnore = null);
    }
}
