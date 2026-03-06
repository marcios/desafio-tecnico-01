using LivrosWebApi.Core.Dtos.Requests;
using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Dtos.Mappers
{
    public static class AutorMapper
    {
        public static Autor ToEntity(this CadastroAutorRequest cadastro)
        {
            return new Autor(cadastro.Nome);
        }
    }
}
