namespace LivrosWebApi.Core.Entities
{
    public class Autor
    {
        private Autor() { }
        public Autor(string nome)
        {
            Nome = nome;
        }
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; } = true;


    }
}
