using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests;

namespace LivrosWebApi.Application.UseCases.Generos
{
    public class CadastroLivroUseCaseBase
    {

        const int TAMANHO_MAXINO_NOME = 255;
        protected readonly ILivroRepository _livroRepository;
        protected readonly IGeneroRepository _generoRepository;
        protected readonly IAutorRepository _autorRepository;

        protected ResultDto result;

        public CadastroLivroUseCaseBase(ILivroRepository repository, IGeneroRepository generoRepository, IAutorRepository autorRepository   )
        {
            result = new ResultDto();
            _livroRepository = repository;
            _generoRepository = generoRepository;
            _autorRepository = autorRepository;
        }

        protected async virtual Task ValidarDadosCadastro(CadastroLivroRequest cadastroLivro)
        {   

            if (string.IsNullOrWhiteSpace(cadastroLivro.Nome))
                result.AddNotificacao("Nome do livro deve ser informado");

            else if (cadastroLivro.Nome.Length > TAMANHO_MAXINO_NOME)
                result.AddNotificacao($"O Tamanho do nome excede máximo permitido que é de {TAMANHO_MAXINO_NOME} caracters");
            else if (await _livroRepository.ExistePorNomeAsync(cadastroLivro.Nome, cadastroLivro.Id))
                result.AddNotificacao($"Já existe um livro cadastrado com o nome {cadastroLivro.Nome}");

            if(!await _autorRepository.ExistePorId(cadastroLivro.AutorId))
                result.AddNotificacao($"Autor informado não existe cadastrado");

            if (!await _generoRepository.ExistePorId(cadastroLivro.GeneroId))
                result.AddNotificacao($"Gênero informado não existe cadastrado");

        }
    }
}
