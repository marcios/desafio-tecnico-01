using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Dtos.Mappers
{
    public static class GeneroMapper
    {
        public static Genero ToEntity(this CadastroGeneroRequest cadastro)
        {
            return new Genero(cadastro.Nome);
        }
    }
}
