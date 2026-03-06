namespace LivrosWebApi.Core.Entities
{
    public class Livro : EntidadeBase
    {
        private Livro()
        {

        }
        public Livro(string nome, int autorId, int generoId)
        {
            Nome = nome;
            AutorId = autorId;
            GeneroId = generoId;    
        }
        
        public string Nome { get; set; }
        public int AutorId { get; set; }
        public int GeneroId { get; set; }
        public bool Ativo { get; set; } = true;
        public Autor Autor { get; set; }
        public Genero Genero { get; set; }

    }
}
