using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Livros;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Autores
{
    public class RemoverLivroUseCase : IUseCase<int, ResultDto>
    {
        protected readonly ILivroRepository _repository;

        protected ResultDto result;

        Livro livro;
        public RemoverLivroUseCase(ILivroRepository repository)
        {
            _repository = repository;
            result = new ResultDto();
        }

        public async Task<ResultDto> ProcessarAsync(int livroId)
        {
            try
            {
                await ValidarExclusao(livroId);

                if (result.Notificacoes.Any())
                    return result;

                _repository.Delete(livro);

                var registrosAfetados = await _repository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    LivroDto dto = livro;
                    result.AddData(dto);
                }
                else
                    result.AddNotificacao("Não foi possível remover o cadastro do livro");

            }
            catch (Exception ex)
            {

                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao remover livro");
                result.AddNotificacao(ex.Message);
            }

            return result;

        }

        private async Task ValidarExclusao(int autorId)
        {
            //verificar se existe
            livro = await _repository.ObterPorIdAsync(autorId);

            if (livro == null)
                result.AddNotificacao($"Não existe um gênero com Id {autorId}");

        }
    }
}
