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
        private readonly AtualizarGeneroUseCase _atualizarGeneroUseCase;
        private readonly RemoverGeneroUseCase _removeGeneroUseCase;

        public GeneroService(IGeneroRepository repository, ILogger<GeneroService> logger)
        {
            _repository = repository;
            _logger = logger;
            _adicionarGeneroUseCase = new AdicionarGeneroUseCase(repository);
            _atualizarGeneroUseCase = new AtualizarGeneroUseCase(repository);
            _removeGeneroUseCase = new RemoverGeneroUseCase(repository);
        }

        public async Task<ResultDto> AdicionarAsync(CadastroGeneroRequest cadastroGenero)
        {
            var result = await _adicionarGeneroUseCase.ProcessarAsync(cadastroGenero);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de criar novo gênero {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> AtualizarAsync(CadastroGeneroRequest cadastroGenero)
        {
            var result = await _atualizarGeneroUseCase.ProcessarAsync(cadastroGenero);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de atualizar o gênero {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> DeleteAsync(int generoId)
        {
            var result = await _removeGeneroUseCase.ProcessarAsync(generoId);

            if (result.Notificacoes.Any())
            {
                _logger.LogError("Erros encontrados no processo de remover gênero {messages}", result.Mensagem);
            }


            return result;
        }

        public async Task<ResultDto> ObterGeneroPorId(int id)
        {
            var result = new ResultDto();

            var genero = await _repository.ObterPorIdAsync(id);

            if(genero == null)
            {
                result.AddNotificacao($"Gênero não localizado com Id: {id}");
                return result;
            }

            GeneroDto dto = genero;
            result.AddData(dto);


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
