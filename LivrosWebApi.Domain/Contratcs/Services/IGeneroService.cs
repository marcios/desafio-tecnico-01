using LivrosWebApi.Core.Dtos.Generos;

namespace LivrosWebApi.Core.Contratcs.Services
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> ObterTodosAsync();
    }
}
