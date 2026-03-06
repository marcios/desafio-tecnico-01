using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Contratcs.Repositories
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<bool> ExistePorNomeAsync(string nome, int? idIgnore = null);
    }
}
