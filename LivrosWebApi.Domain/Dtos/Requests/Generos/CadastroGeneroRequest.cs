namespace LivrosWebApi.Core.Dtos.Requests.Generos
{
    public record CadastroGeneroRequest
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
    }

    
}
