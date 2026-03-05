using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests.Generos;

namespace LivrosWebApi.Core.Contratcs.Services
{
    public interface IGeneroService
    {
        Task<ResultDto> AdicionarAsync(CadastroGeneroRequest cadastroGenero);
        Task<ResultDto> AtualizarAsync(CadastroGeneroRequest cadastroGenero);
        Task<ResultDto> DeleteAsync(int generoId);

        Task<ResultDto> ObterGeneroPorId(int id);
        Task<ResultDto> ObterTodosAsync();
    }
}
