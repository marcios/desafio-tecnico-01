using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Autores
{
    public class RemoverAutorUseCase : IUseCase<int, ResultDto>
    {
        protected readonly IAutorRepository _repository;

        protected ResultDto result;

        Autor autor;
        public RemoverAutorUseCase(IAutorRepository repository)
        {
            _repository = repository;
            result = new ResultDto();
        }

        public async Task<ResultDto> ProcessarAsync(int generoId)
        {
            try
            {
                await ValidarExclusao(generoId);

                if (result.Notificacoes.Any())
                    return result;

                _repository.Delete(autor);

                var registrosAfetados = await _repository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    AutorDto dto = autor;
                    result.AddData(dto);
                }
                else
                    result.AddNotificacao("Não foi possível remover o cadastro do gênero");

            }
            catch (Exception ex)
            {

                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao remover gênero");
                result.AddNotificacao(ex.Message);
            }

            return result;

        }

        private async Task ValidarExclusao(int generoId)
        {
            //verificar se existe
            autor = await _repository.ObterPorIdAsync(generoId);

            if (autor == null)
                result.AddNotificacao($"Não existe um gênero com Id {generoId}");

            //verificar se tem Livros vinculados

        }
    }
}
