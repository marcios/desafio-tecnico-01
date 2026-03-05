using LivrosWebApi.Application.UseCases.Generos;
using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.Services;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Requests.Generos;
using Microsoft.Extensions.Logging;

namespace LivrosWebApi.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _repository;
        private readonly ILogger<GeneroService> _logger;

        private readonly AdicionarGeneroUseCase _adicionarGeneroUseCase;

        public GeneroService(IGeneroRepository repository, ILogger<GeneroService> logger)
        {
            _repository = repository;
            _logger = logger;
            _adicionarGeneroUseCase = new AdicionarGeneroUseCase(repository);
        }

        public async Task<ResultDto> AdicionarAsync(CadastroGeneroRequest cadastroGenero)
        {
            var result = await _adicionarGeneroUseCase.ProcessarAsync(cadastroGenero);

            if (result.Notificacoes.Any())
                _logger.LogInformation("Erros encontrados no processo de criar novo gêncero {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> ObterTodosAsync()
        {
            var result = new ResultDto();
            var generos = await _repository.ObterTodosAsync();
            if (generos == null || !generos.Any()){

                result.AddNotificacao("Nenhum registro localizado");
                return result;
            }

            var listGeneros = generos.Select(genero => new GeneroDto(genero));

            result.AddData(listGeneros);

            return result;
        }
    }
}
