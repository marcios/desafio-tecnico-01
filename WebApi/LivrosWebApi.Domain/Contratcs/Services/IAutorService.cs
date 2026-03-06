using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests;

namespace LivrosWebApi.Core.Contratcs.Services
{
    public interface IAutorService
    {
        Task<ResultDto> AdicionarAsync(CadastroAutorRequest cadastroAturo);
        Task<ResultDto> AtualizarAsync(CadastroAutorRequest cadastroAutor);
        Task<ResultDto> ObterPorIdAsync(int id);
        Task<ResultDto> ObterTodosAsync();
        Task<ResultDto> DeleteAsync(int generoId);
    }
}
