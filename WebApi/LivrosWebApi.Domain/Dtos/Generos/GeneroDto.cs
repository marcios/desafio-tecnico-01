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
            Ativo = genero.Ativo;

            if(genero.Livros!=null)
                TotalLivros = genero.Livros.Count;  

            
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get;  set; }

        public int? TotalLivros { get; set; } = 0;

        public static implicit operator GeneroDto(Genero genero)
        {
            if (genero == null) return null;
            return new GeneroDto(genero);
           
        }
    }
}
