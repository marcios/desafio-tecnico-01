using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Repositories
{
    public class AutorRepository : RepositoryBase<Autor>, IAutorRepository
    {

        public AutorRepository(LivrosDbContext context):base(context) 
        {
           
        }

        public async Task<bool> ExistePorNomeAsync(string nome, int? idIgnore = null)
        {
            var query = _context.Autores
                 .Where(x => x.Nome.ToLower().Equals(nome.ToLower()));


            if (idIgnore.HasValue && idIgnore.Value > 0)
                query = query.Where(x => x.Id != idIgnore.Value);


            return await query.AnyAsync();
        }
    }
}
