using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly LivrosDbContext _context;

        public RepositoryBase(LivrosDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(T entity)
        {
           await _context.AddAsync(entity);  
        }

        public async Task<IEnumerable<T>> ObterTodosAsync()
        {
            return await _context.Set<T>().AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveChagesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
