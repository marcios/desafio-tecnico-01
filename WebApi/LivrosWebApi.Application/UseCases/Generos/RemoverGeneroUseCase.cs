using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Generos
{
    public class RemoverGeneroUseCase : IUseCase<int, ResultDto>
    {
        protected readonly IGeneroRepository _generoRepository;

        protected ResultDto result;

        Genero genero;
        public RemoverGeneroUseCase(IGeneroRepository repository)
        {
            _generoRepository = repository;
            result = new ResultDto();
        }

        public async Task<ResultDto> ProcessarAsync(int generoId)
        {
            try
            {
                await ValidarExclusao(generoId);

                if (result.Notificacoes.Any())
                    return result;

                _generoRepository.Delete(genero);

                var registrosAfetados = await _generoRepository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    GeneroDto dto = genero;
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
            genero = await _generoRepository.ObterPorIdAsync(generoId);

            if (genero == null)
                result.AddNotificacao($"Não existe um gênero com Id {generoId}");

            //verificar se tem Livros vinculados

        }
    }
}
