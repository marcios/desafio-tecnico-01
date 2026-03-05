using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests.Generos;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Generos
{
    public class AtualizarGeneroUseCase :CadastroGeneroUseCaseBase , IUseCase<CadastroGeneroRequest, ResultDto>
    {

        private Genero genero;

        public AtualizarGeneroUseCase(IGeneroRepository generoRepository):base(generoRepository)
        {   
        }

        protected override async Task ValidarDadosCadastro(CadastroGeneroRequest cadastroGenero)
        {
            await base.ValidarDadosCadastro(cadastroGenero);

            if (result.Notificacoes.Any())
                return ;

            if (!cadastroGenero.Id.HasValue)
                result.AddNotificacao("Id do gênero não informado");

            genero = await _generoRepository.ObterPorIdAsync(cadastroGenero.Id.Value);

            if (genero == null)            
                result.AddNotificacao($"Gênero não localizado com o Id ({cadastroGenero.Id})");

        }

        public async  Task<ResultDto> ProcessarAsync(CadastroGeneroRequest cadastroRequest)
        {
            try
            {
                await ValidarDadosCadastro(cadastroRequest);

                if (result.Notificacoes.Any())
                    return result;


                genero.AtualizarNome(cadastroRequest.Nome);
                _generoRepository.Atualizar(genero);
                var registrosAfetados = await _generoRepository.SaveChagesAsync();

                if (registrosAfetados > 0)
                    result.AddData(genero);
                else
                    result.AddNotificacao("Não foi possível atualizar o cadastro do gênero");

            }
            catch (Exception ex)
            {
                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao atualizar gênero");
                result.AddNotificacao(ex.Message);
            }


            return result;
        }
    }
}
