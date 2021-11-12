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
        public CategoryServices(ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Category>> GetCategories(string parrentId = null)
        {
            return await _repository.Get(parrentId);
        }

        public async Task ImportCategories(Stream stream)
        {
            //TODO: read data
            List<CategoriesCSV> result = new List<CategoriesCSV>();

            using(var reader = new StreamReader(stream))
            using(var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csvReader.Context.RegisterClassMap<CategoryMap>();
                result = csvReader.GetRecords<CategoriesCSV>().ToList<CategoriesCSV>();
            }

            //NEED AUTOMAPER

            List<Category> categoriesList = new List<Category>();

            foreach(var item in result)
            {
                Category category = new Category();

                category.Code = item.Code;
                category.ParrentCode = item.ParentCode;
                category.Name = item.Name;

                categoriesList.Add(category);
            }

            //TODO: validations

           await _repository.Import(categoriesList);
        }
    }
}