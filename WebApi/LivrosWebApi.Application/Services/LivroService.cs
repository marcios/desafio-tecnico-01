using LivrosWebApi.Application.UseCases.Autores;
using LivrosWebApi.Application.UseCases.Generos;
using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.Services;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Livros;
using LivrosWebApi.Core.Dtos.Requests;
using Microsoft.Extensions.Logging;

namespace LivrosWebApi.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _repository;
        private readonly ILogger<LivroService> _logger;

        private readonly AdicionarLivroUseCase _adicionarLivroUseCase;
        private readonly AtualizarLivroUseCase _atualizarLivroUseCase;
        private readonly RemoverLivroUseCase _removerLivroUseCase;



        public LivroService(ILivroRepository repository, IGeneroRepository generoRepository, IAutorRepository autorRepository, ILogger<LivroService> logger)
        {
            _repository = repository;
            _adicionarLivroUseCase = new AdicionarLivroUseCase(repository, generoRepository, autorRepository);
            _atualizarLivroUseCase = new AtualizarLivroUseCase(repository, generoRepository, autorRepository);
            _logger = logger;
            _removerLivroUseCase = new RemoverLivroUseCase(repository);
        }

        public async Task<ResultDto> AdicionarAsync(CadastroLivroRequest cadastroLivro)
        {
            var result = await _adicionarLivroUseCase.ProcessarAsync(cadastroLivro);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de criar novo livro {messages}", result.Mensagem);

            return result;

        }

        public async Task<ResultDto> AtualizarAsync(CadastroLivroRequest cadastroLivro)
        {
            var result = await _atualizarLivroUseCase.ProcessarAsync(cadastroLivro);

            if (result.Notificacoes.Any())
                _logger.LogError("Erros encontrados no processo de atualizar o livro {messages}", result.Mensagem);

            return result;
        }

        public async Task<ResultDto> DeleteAsync(int livroId)
        {
            var result = await _removerLivroUseCase.ProcessarAsync(livroId);

            if (result.Notificacoes.Any())
            {
                _logger.LogError("Erros encontrados no processo de remover livro {messages}", result.Mensagem);
            }


            return result;
        }

        public async Task<ResultDto> ObterPorIdAsync(int livroId)
        {
            var result = new ResultDto();

            var livro = await _repository.ObterPorIdAsync(livroId);

            if (livro == null)
            {
                result.AddNotificacao($"Livro não localizado com Id: {livroId}");
                return result;
            }

            LivroDto dto = livro;
            result.AddData(dto);


            return result;
        }

        public async Task<ResultDto> ObterTodosAsync()
        {

            var result = new ResultDto();
            var livros = await _repository.ObterTodosAsync(
                livro => livro.Autor,
                livro => livro.Genero
                );

            if (livros == null || !livros.Any())
            {

                result.AddNotificacao("Nenhum registro localizado");
                return result;
            }

            var listGeneros = livros.Select(livro => new LivroDto(livro));

            result.AddData(listGeneros);

            return result;
        }
    }
}
