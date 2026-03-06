using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Mappers;
using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Autores
{

    public class AdicionarAutorUseCase : CadastroAutorUseCaseBase, IUseCase<CadastroAutorRequest, ResultDto>
    {
        public AdicionarAutorUseCase(IAutorRepository repository):base(repository)
        {
            
        }

        public async Task<ResultDto> ProcessarAsync(CadastroAutorRequest cadastro)
        {
            try
            {
                await ValidarDadosCadastro(cadastro);

                if (result.Notificacoes.Any())
                    return result;

                //salvar
                Autor autor = cadastro.ToEntity();
               
                await _repository.AdicionarAsync(autor);
                var registrosAfetados = await _repository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    AutorDto dto = autor;
                    result.AddData(dto);
                }
                else
                    result.AddNotificacao("Não foi possível salvar o cadastro do autor");

            }
            catch (Exception ex)
            {
                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao cadastrar novo autor");
                result.AddNotificacao(ex.Message);
            }


            return result;

        }

     
    }
}
