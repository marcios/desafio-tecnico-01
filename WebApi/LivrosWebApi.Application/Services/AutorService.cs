using LivrosWebApi.Application.UseCases.Generos;
using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.Services;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Requests;
using Microsoft.Extensions.Logging;

namespace LivrosWebApi.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _repository;
        private readonly ILogger<AutorService> _logger;
        private readonly AdicionarAutoroUseCase _adicionarAutorUseCase;

        public AutorService(IAutorRepository repository, ILogger<AutorService> logger)
        {
            _repository = repository;
            _logger = logger;
            _adicionarAutorUseCase = new AdicionarAutoroUseCase(repository);
        }

        public async Task<ResultDto> AdicionarAsync(CadastroAutorRequest cadastroGenero)
        {
            var result = await _adicionarAutorUseCase.ProcessarAsync(cadastroGenero);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de criar novo gênero {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> ObterTodosAsync()
        {
            var result = new ResultDto();
            var autores = await _repository.ObterTodosAsync();
            if (autores == null || !autores.Any())
            {

                result.AddNotificacao("Nenhum registro localizado");
                return result;
            }

            var listGeneros = autores.Select(autor => new AutorDto(autor));

            result.AddData(listGeneros);

            return result;
        }
    }
}
