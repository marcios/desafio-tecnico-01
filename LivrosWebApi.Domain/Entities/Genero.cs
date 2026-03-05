namespace LivrosWebApi.Core.Entities
{
    public class Genero
    {
        private Genero() { }
        public Genero(string nome)
        {
            Nome = nome;    
        }

        public int Id { get; private set; }
        public string Nome { get; private set; }

        public void AtualizarNome(string nome) => Nome = nome;
    }
}
