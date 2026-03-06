using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Mappers;
using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Generos
{

    public class AdicionarGeneroUseCase : CadastroGeneroUseCaseBase, IUseCase<CadastroGeneroRequest, ResultDto>
    {
        public AdicionarGeneroUseCase(IGeneroRepository generoRepository):base(generoRepository)
        {
            
        }

        public async Task<ResultDto> ProcessarAsync(CadastroGeneroRequest cadastroGenero)
        {
            try
            {
                await ValidarDadosCadastro(cadastroGenero);

                if (result.Notificacoes.Any())
                    return result;

                //salvar
                Genero genero = cadastroGenero.ToEntity();
               
                await _generoRepository.AdicionarAsync(genero);
                var registrosAfetados = await _generoRepository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    GeneroDto dto = genero;
                    result.AddData(dto);
                }
                else
                    result.AddNotificacao("Não foi possível salvar o cadastro do gênero");

            }
            catch (Exception ex)
            {
                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao cadastrar novo gênero");
                result.AddNotificacao(ex.Message);
            }


            return result;

        }

     
    }
}
