using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Contratcs.UseCases;
using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Generos;
using LivrosWebApi.Core.Dtos.Mappers;
using LivrosWebApi.Core.Dtos.Requests.Generos;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Application.UseCases.Generos
{

    public class AdicionarGeneroUseCase : IUseCase<CadastroGeneroRequest, ResultDto>
    {
        const int TAMANHO_MAXINO_NOME = 100;
        private readonly IGeneroRepository _generoRepository;
        private ResultDto result;

        public AdicionarGeneroUseCase(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
            result = new ResultDto();
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
                    result.AddData(genero);
                else
                    result.AddNotificacao("Não foi possível salvar o cadastro do gênero");

            }
            catch (Exception ex)
            {
                result.AddNotificacao("Falha ao cadastrar novo gênero");
                result.AddNotificacao(ex.Message);
            }


            return result;

        }

        private async Task ValidarDadosCadastro(CadastroGeneroRequest cadastroGenero)
        {
            //Nome está vazio

            if (string.IsNullOrWhiteSpace(cadastroGenero.Nome))
                result.AddNotificacao("Nome do gênero deve ser informado");

            else if (cadastroGenero.Nome.Length > TAMANHO_MAXINO_NOME)
                result.AddNotificacao($"O Tamanho do nome excede´máximo permitido que é de {TAMANHO_MAXINO_NOME} caracters");
            else if (await _generoRepository.ExistePorNomeAsync(cadastroGenero.Nome))
                result.AddNotificacao($"Já existe um gênero cadastrado com o nome {cadastroGenero.Nome}");

        }
    }
}
