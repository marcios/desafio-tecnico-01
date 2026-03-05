using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly LivrosDbContext _context;
        public GeneroRepository(LivrosDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Genero>> ObterTodosAsync()
        {
            return await _context.Generos
                .AsNoTracking()
                .ToArrayAsync();
        }
    }
}
