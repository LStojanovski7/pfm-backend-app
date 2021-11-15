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

        public async Task<Category> GetCategory(string code) => await _context.Categories.FindAsync(code);

        public async Task Import(List<Category> mappedList)
        {
            foreach(var item in mappedList)
            {
                var entity =  await _context.Categories.FindAsync(item.Code);

                if(entity != null)
                {
                    await Update(item);
                }
                else
                {
                    await Add(item);
                }
            }
        }

        public async Task<Category> Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return category;
        }

        public async Task<Category> Add(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}