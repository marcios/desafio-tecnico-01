using LivrosWebApi.Application.UseCases.Autores;
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
        private readonly AtualizarAutorUseCase _atualizarAutorUseCase;
        private readonly AdicionarAutorUseCase _adicionarAutorUseCase;
        private readonly RemoverAutorUseCase _removerAutorUseCase;

        public AutorService(IAutorRepository repository, ILogger<AutorService> logger)
        {
            _repository = repository;
            _logger = logger;
            _adicionarAutorUseCase = new AdicionarAutorUseCase(repository);
            _atualizarAutorUseCase = new AtualizarAutorUseCase(repository);
            _removerAutorUseCase = new RemoverAutorUseCase(repository);
        }

        public async Task<ResultDto> AdicionarAsync(CadastroAutorRequest cadastroGenero)
        {
            var result = await _adicionarAutorUseCase.ProcessarAsync(cadastroGenero);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de criar novo gênero {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> AtualizarAsync(CadastroAutorRequest cadastroAutor)
        {
            var result = await _atualizarAutorUseCase.ProcessarAsync(cadastroAutor);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de atualizar o autor {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> DeleteAsync(int generoId)
        {
            var result = await _removerAutorUseCase.ProcessarAsync(generoId);

            if (result.Notificacoes.Any())
            {
                _logger.LogError("Erros encontrados no processo de remover autor {messages}", result.Mensagem);
            }


            return result;
        }

        public async Task<ResultDto> ObterPorIdAsync(int id)
        {
            var result = new ResultDto();

            var autor = await _repository.ObterPorIdAsync(id);

            if (autor == null)
            {
                result.AddNotificacao($"Gênero não localizado com Id: {id}");
                return result;
            }

            AutorDto dto = autor;
            result.AddData(dto);
            return result;
        }

        public async Task<ResultDto> ObterTodosAsync()
        {
            var result = new ResultDto();
            var autores = await _repository.ObterTodosAsync(x=>x.Livros);
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
