namespace LivrosWebApi.Core.Dtos.Requests
{
    public class CadastroAutorRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;
    }
}
