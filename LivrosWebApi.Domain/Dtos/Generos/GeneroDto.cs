using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Dtos.Generos
{
    public class GeneroDto
    {
        public GeneroDto()
        {

        }

        public GeneroDto(Genero genero)
        {
            Id = genero.Id;
            Nome =genero.Nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }

        public static implicit operator GeneroDto(Genero genero)
        {
            if (genero == null) return null;
            return new GeneroDto(genero);
           
        }
    }
}
