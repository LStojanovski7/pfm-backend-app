using System.Threading.Tasks;
using System.Collections.Generic;
using Data.Entities;
using System.Linq;
using System.IO;

namespace Data.Repositories.Categories
{
    public interface ICategoriesRepository
    {
        Task<List<Category>> Get(string parrentId = null);
        Task Import(List<Category> mappedList);
    }
}