using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Entities;

namespace Data.Repositories.Categories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> Get(string parrentId = null);
        Task Import(List<Category> mappedList);
        Task<Category> Update(Category category);
        Task<Category> Add(Category category);
        Task<Category> GetCategory(string code, string parrentId = null);
    }
}