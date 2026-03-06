using LivrosWebApi.Core.Entities;

namespace LivrosWebApi.Core.Dtos.Livros
{
    public class LivroDto
    {
        public LivroDto()
        {

        }

        public LivroDto(Livro livro)
        {
            Id = livro.Id;
            Nome = livro.Nome;
            Ativo = livro.Ativo;

            if (livro.Autor != null)
                Autor = livro.Autor.Nome;

            AutorId = livro.AutorId;

            if (livro.Genero != null)
                Genero = livro.Genero.Nome;

            GeneroId = livro.GeneroId;


        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public int AutorId { get; set; }
        public string Autor { get; set; }
        public string Genero { get; set; }
        public int GeneroId { get; set; }



        public static implicit operator LivroDto(Livro genero)
        {
            if (genero == null) return null;
            return new LivroDto(genero);

        }
    }
}
