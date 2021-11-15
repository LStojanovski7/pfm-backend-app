using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Entities;
using System.IO;

namespace Services.Categories
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetCategories(string parrentId = null);
        Task ImportCategories(Stream stream);
        Task<Category> Add(Category category);
        Task<Category> Update(Category category);
    }
}