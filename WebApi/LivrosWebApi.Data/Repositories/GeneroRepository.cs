using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Repositories
{
    public class GeneroRepository : RepositoryBase<Genero>, IGeneroRepository
    {
        private readonly LivrosDbContext _context;
        public GeneroRepository(LivrosDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistePorNomeAsync(string nome, int? idIgnore)
        {
            var query = _context.Generos
                .Where(x => x.Nome.ToLower().Equals(nome.ToLower()));


            if (idIgnore.HasValue && idIgnore.Value > 0)
                query = query.Where(x => x.Id != idIgnore.Value);


            return await query.AnyAsync();

        }


    }
}
