namespace LivrosWebApi.Core.Entities
{
    public class Genero : EntidadeBase
    {
        private Genero() { }
        public Genero(string nome)
        {
            Nome = nome;
        }

        
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;

        public ICollection<Livro> Livros { get; set; }


    }
}
