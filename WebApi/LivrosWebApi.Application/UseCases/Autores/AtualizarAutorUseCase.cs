using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Autores
{
    public class AtualizarAutorUseCase : CadastroAutorUseCaseBase, IUseCase<CadastroAutorRequest, ResultDto>
    {

        private Autor autor;

        public AtualizarAutorUseCase(IAutorRepository repository) : base(repository)
        {
        }

        protected override async Task ValidarDadosCadastro(CadastroAutorRequest cadastro)
        {
            await base.ValidarDadosCadastro(cadastro);

            if (result.Notificacoes.Any())
                return;

            if (!cadastro.Id.HasValue)
                result.AddNotificacao("Id do gênero não informado");

            autor = await _repository.ObterPorIdAsync(cadastro.Id.Value);

            if (autor == null)
                result.AddNotificacao($"Gênero não localizado com o Id ({cadastro.Id})");

        }

        public async Task<ResultDto> ProcessarAsync(CadastroAutorRequest cadastro)
        {
            try
            {
                await ValidarDadosCadastro(cadastro);

                if (result.Notificacoes.Any())
                    return result;


                autor.Nome = cadastro.Nome;
                autor.Ativo = cadastro.Ativo;

                _repository.Atualizar(autor);
                var registrosAfetados = await _repository.SaveChagesAsync();

                if (registrosAfetados > 0)
                {
                    AutorDto dto = autor;
                    result.AddData(dto);
                }
                else
                    result.AddNotificacao("Não foi possível atualizar o cadastro do autor");

            }
            catch (Exception ex)
            {
                result.Notificacoes.Clear();
                result.AddNotificacao("Falha ao atualizar autor");
                result.AddNotificacao(ex.Message);
            }


            return result;
        }
    }
}
