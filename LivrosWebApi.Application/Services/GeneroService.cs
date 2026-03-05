using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.Services;
using LivrosWebApi.Core.Dtos.Generos;

namespace LivrosWebApi.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _repository;

        public GeneroService(IGeneroRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GeneroDto>> ObterTodosAsync()
        {
            var generos = await _repository.ObterTodosAsync();
            if (generos == null) return null;

            return generos.Select(genero => new GeneroDto(genero));
        }
    }
}
