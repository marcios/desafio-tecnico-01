using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Dtos.Mappers
{
    public static class LivroMapper
    {
        public static Livro ToEntity(this CadastroLivroRequest cadastro)
        {
            return new Livro(cadastro.Nome, cadastro.AutorId, cadastro.GeneroId);
        }
    }
}
