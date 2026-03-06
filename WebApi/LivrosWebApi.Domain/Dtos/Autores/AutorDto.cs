using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Dtos.Generos
{
    public class AutorDto
    {
        public AutorDto()
        {

        }

        public AutorDto(Autor autor)
        {
            Id = autor.Id;
            Nome = autor.Nome;
            Ativo = autor.Ativo;

            if(autor.Livros!=null)
                TotalLivros=autor.Livros.Count; 


        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public int? TotalLivros { get; set; } = 0;

        public static implicit operator AutorDto(Autor autor)
        {
            if (autor == null) return null;
            return new AutorDto(autor);

        }
    }
}
