using Services.Interfaces;
using Data.Repositories.Categories;
using Data.Entities;

namespace Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoriesRepository _repository;
        public CategoryServices(ICategoriesRepository repository)
        {
            _repository = repository;
        }
    }
}