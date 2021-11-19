using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Entities;
using Data.Entities.Enums;
using System.IO;

namespace Services.Categories
{
    public interface ICategoryServices
    {
        Task<List<Category>> GetCategories(string parrentId = null);
        Task<Category> GetCategory(string code);
        Task Import(Stream stream);
        Task<Category> Add(Category category);
        Task<Category> Update(Category category);
        // Task<Groups> SpendingByCategory(string catcode, string startDate, string endDate, string direction);
    }
}