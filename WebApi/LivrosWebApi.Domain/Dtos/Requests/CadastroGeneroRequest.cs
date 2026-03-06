namespace LivrosWebApi.Core.Dtos.Requests
{
    public record CadastroGeneroRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }

        public bool Ativo { get; set; } = true;
    }

    
}
