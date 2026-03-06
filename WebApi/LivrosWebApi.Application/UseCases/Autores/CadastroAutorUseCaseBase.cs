using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests;

namespace LivrosWebApi.Application.UseCases.Generos
{
    public class CadastroAutorUseCaseBase
    {

        const int TAMANHO_MAXINO_NOME = 100;
        protected readonly IAutorRepository _repository;

        protected ResultDto result;

        public CadastroAutorUseCaseBase(IAutorRepository repository)
        {
            result = new ResultDto();
            _repository = repository;
        }

        protected async virtual Task ValidarDadosCadastro(CadastroAutorRequest cadastro)
        {
            //Nome está vazio

            if (string.IsNullOrWhiteSpace(cadastro.Nome))
                result.AddNotificacao("Nome do gênero deve ser informado");

            else if (cadastro.Nome.Length > TAMANHO_MAXINO_NOME)
                result.AddNotificacao($"O Tamanho do nome excede máximo permitido que é de {TAMANHO_MAXINO_NOME} caracters");
            else if (await _repository.ExistePorNomeAsync(cadastro.Nome, cadastro.Id))
                result.AddNotificacao($"Já existe um autor cadastrado com o nome {cadastro.Nome}");

        }
    }
}
