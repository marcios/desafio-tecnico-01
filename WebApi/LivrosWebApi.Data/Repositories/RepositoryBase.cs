using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LivrosWebApi.Data.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : EntidadeBase
    {
        protected readonly LivrosDbContext _context;

        public RepositoryBase(LivrosDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(T entity)
        {
            await _context.AddAsync(entity);
        }

        public void Atualizar(T entity)
        {
            _context.Set<T>().Update(entity);

        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<bool> ExistePorId(int id)
        {
            return await _context.Set<T>().AnyAsync(x => x.Id == id);
        }

        public async Task<T> ObterPorIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var query =  _context.Set<T>().AsQueryable();

            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }


            return await query.FirstOrDefaultAsync(x => x.Id == id);


        }

        public async Task<IEnumerable<T>> ObterTodosAsync(params Expression<Func<T, object>>[] includes)
        {
            var query = _context.Set<T>().AsNoTracking();

            if (includes != null && includes.Any())
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            return await query.ToListAsync();
        }

        public async Task<int> SaveChagesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
