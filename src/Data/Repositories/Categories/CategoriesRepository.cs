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
            IQueryable<Category> query;

            if(string.IsNullOrEmpty(parrentId))
            {
                query = _context.Categories.AsQueryable<Category>();
            }
            else
            {
                query = _context.Categories.Where(c => c.ParrentCode == parrentId).AsQueryable<Category>();
            }

            return await query.ToListAsync<Category>();
        }

        public async Task<Category> GetCategory(string code, string parrentId = null) => await _context.Categories.FindAsync(code);

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
            var item = _context.Categories.Find(category.Code);

            if(item != null)
            {
                _context.Entry<Category>(item).State = EntityState.Detached;
                await Update(category);
                
                return category;
            }
            
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}