namespace LivrosWebApi.Core.Dtos.Requests
{
    public class CadastroLivroRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public int GeneroId { get; set; }
        public int AutorId { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
