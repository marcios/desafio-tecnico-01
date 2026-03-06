using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests;

namespace LivrosWebApi.Application.UseCases.Generos
{
    public class CadastroGeneroUseCaseBase
    {

        const int TAMANHO_MAXINO_NOME = 100;
        protected readonly IGeneroRepository _generoRepository;

        protected ResultDto result;

        public CadastroGeneroUseCaseBase(IGeneroRepository generoRepository)
        {
            result = new ResultDto();
            _generoRepository = generoRepository;
        }

        protected async virtual Task ValidarDadosCadastro(CadastroGeneroRequest cadastroGenero)
        {
            //Nome está vazio

            if (string.IsNullOrWhiteSpace(cadastroGenero.Nome))
                result.AddNotificacao("Nome do gênero deve ser informado");

            else if (cadastroGenero.Nome.Length > TAMANHO_MAXINO_NOME)
                result.AddNotificacao($"O Tamanho do nome excede máximo permitido que é de {TAMANHO_MAXINO_NOME} caracters");
            else if (await _generoRepository.ExistePorNomeAsync(cadastroGenero.Nome, cadastroGenero.Id))
                result.AddNotificacao($"Já existe um gênero cadastrado com o nome {cadastroGenero.Nome}");

        }
    }
}
