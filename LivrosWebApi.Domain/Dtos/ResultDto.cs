using System.Text.Json.Serialization;

namespace LivrosWebApi.Core.Dtos
{
    public class ResultDto
    {
        public dynamic? Data { get; private set; }
        public void AddData(dynamic data)
        {
            Data = data;
            
        }

        public ResultDto()
        {
            Notificacoes = new List<string>();
        }
        public List<string> Notificacoes { get; private set; }



        [JsonIgnore]
        public string Mensagem => ObterNotificacoes();


        private string ObterNotificacoes()
        {
            if (!Notificacoes.Any()) return string.Empty;

            return string.Join("\n", Notificacoes);
        }
        public void AddNotificacao(string notificacao)
        {
            if (Notificacoes == null)
                Notificacoes = new List<string>();

            Notificacoes.Add(notificacao);
        }
    }
  
}
