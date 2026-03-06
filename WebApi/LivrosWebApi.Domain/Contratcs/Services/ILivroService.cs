using LivrosWebApi.Core.Dtos;
using LivrosWebApi.Core.Dtos.Requests;

namespace LivrosWebApi.Core.Contratcs.Services
{
    public interface ILivroService
    {
        Task<ResultDto> AdicionarAsync(CadastroLivroRequest cadastroLivro);
        Task<ResultDto> AtualizarAsync(CadastroLivroRequest cadastroLivro);
        Task<ResultDto> DeleteAsync(int livroId);
        Task<ResultDto> ObterPorIdAsync(int livroId);
        Task<ResultDto> ObterTodosAsync();
    }

}
