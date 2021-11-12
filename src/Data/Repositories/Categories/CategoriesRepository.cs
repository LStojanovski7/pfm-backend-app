using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Categories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly AppDbContext _context;
        public CategoriesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Category>> Get(string parrentId = null)
        {
            var query = _context.Categories.AsQueryable<Category>();

            //TODO: filter (parrentId)

            return await query.ToListAsync<Category>();
        }

        public async Task Import(List<Category> mappedList)
        {
            //TODO: IF exists then update
            _context.Categories.AddRange(mappedList);
            await _context.SaveChangesAsync();
        }
    }
}