using LivrosWebApi.Core.Contratcs.Repositories;
using LivrosWebApi.Core.Entities;
using LivrosWebApi.Data.Contexts;
using Microsoft.EntityFrameworkCore;

namespace LivrosWebApi.Data.Repositories
{
    public class GeneroRepository : RepositoryBase<Genero>, IGeneroRepository
    {
        private readonly LivrosDbContext _context;
        public GeneroRepository(LivrosDbContext context):base(context) 
        {
            _context = context;
        }

        public async Task<bool> ExistePorNomeAsync(string nome)
        {
            return await _context.Generos.AnyAsync(x=>x.Nome.ToLower().Equals(nome.ToLower()));  
        }

     
    }
}
