namespace LivrosWebApi.Core.Contratcs.UseCases
{
    public interface IUseCase<TEntrada, TSaida> where TEntrada : class where TSaida : class
    {
        Task<TSaida> ProcessarAsync(TEntrada entrada);
    }
}
