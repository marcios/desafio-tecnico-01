using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Livros;
using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Generos
{
    public class AtualizarLivroUseCase : CadastroLivroUseCaseBase, IUseCase<CadastroLivroRequest, ResultDto>
    {

        private Livro livro;

        public AtualizarLivroUseCase(ILivroRepository repository, IGeneroRepository generoRepository, IAutorRepository autorRepository) : base(repository, generoRepository, autorRepository)
        {
        }

        protected override async Task ValidarDadosCadastro(CadastroLivroRequest cadastroLivro)
        {
            await base.ValidarDadosCadastro(cadastroLivro);

            if (result.Notificacoes.Any())
                return;

            if (!cadastroLivro.Id.HasValue)
                result.AddNotificacao("Id do livro não informado");

            livro = await _livroRepository.ObterPorIdAsync(cadastroLivro.Id.Value);

            if (livro == null)
                result.AddNotificacao($"Gênero não localizado com o Id ({cadastroLivro.Id})");

        }

        public async Task<ResultDto> ProcessarAsync(CadastroLivroRequest cadastroRequest)
        {
            try
            {
                await ValidarDadosCadastro(cadastroRequest);

                if (result.Notificacoes.Any())
                    return result;


                livro.Nome = cadastroRequest.Nome;
                livro.Ativo = cadastroRequest.Ativo;
                livro.GeneroId = cadastroRequest.GeneroId;
                livro.AutorId = cadastroRequest.AutorId;

                _livroRepository.Atualizar(livro);
                var registrosAfetados = await _generoRepository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    LivroDto dto = livro;
                    result.AddData(dto);
                }
                else
                    result.AddNotificacao("Não foi possível atualizar o cadastro do livro");

            }
            catch (Exception ex)
            {
                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao atualizar livro");
                result.AddNotificacao(ex.Message);
            }


            return result;
        }
    }
}
