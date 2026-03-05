namespace LivrosWebApi.Core.Contratcs.UseCases
{
    public interface IUseCase<TEntrada, TSaida>  where TSaida : class
    {
        Task<TSaida> ProcessarAsync(TEntrada entrada);
    }
}
