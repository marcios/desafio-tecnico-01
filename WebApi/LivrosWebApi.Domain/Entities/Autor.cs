namespace LivrosWebApi.Core.Entities
{
    public class Autor : EntidadeBase
    {
        private Autor() { }
        public Autor(string nome)
        {
            Nome = nome;
        }
        
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;

        public ICollection<Livro> Livros { get; set; }


    }
}
