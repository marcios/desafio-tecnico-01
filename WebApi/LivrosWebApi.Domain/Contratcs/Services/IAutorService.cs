using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests;

namespace LivrosWebApi.Core.Contratcs.Services
{
    public interface IAutorService
    {
        Task<ResultDto> AdicionarAsync(CadastroAutorRequest cadastroGenero);
        Task<ResultDto> ObterTodosAsync();
    }
}
