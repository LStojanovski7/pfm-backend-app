using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Data.Repositories.Categories;
using Data.Entities;
using CsvHelper;
using System.Globalization;
using Services.CsvMaps;
using System.Linq;

namespace Services.Categories
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoriesRepository _repository;
        // private readonly IRepository<Category> _repository;
        public CategoryServices(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetCategories(string parrentId = null)
        {
            var category =  await _repository.GetCategory(parrentId);

            if(category == null)
            {
                return await _repository.Get(null); 
            }
            return await _repository.Get(parrentId);
        }

        public async Task<Category> GetCategory(string code)
        {
            return await _repository.GetCategory(code);
        }

        public async Task Import(Stream stream)
        {
            List<CategoriesCSV> result = new List<CategoriesCSV>();

            using(var reader = new StreamReader(stream))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<CategoryMap>();
                result = csvReader.GetRecords<CategoriesCSV>().ToList<CategoriesCSV>();
            }
            //NEED AUTOMAPER
            //TODO: validations
            foreach(var item in result)
            {
                Category category = new Category();

                category.Code = item.Code;
                
                if(string.IsNullOrEmpty(item.ParentCode))
                {
                    category.ParrentCode = null;
                }
                else
                {
                    category.ParrentCode = item.ParentCode;
                }

                category.Name = item.Name;

                await _repository.Add(category);
            }
        }

        public async Task<Category> Add(Category category)
        {

            await _repository.Add(category);

            return category;
        }

        public async Task<Category> Update(Category category)
        {
            var entity = await _repository.GetCategory(category.Code);

            if(entity == null)
            {
                throw new KeyNotFoundException("Provided category does not exist");
            }
            else
            {
                await _repository.Update(category);
            }

            return category;
        }
    }
}