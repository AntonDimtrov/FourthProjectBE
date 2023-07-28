using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Abstract;

namespace TravelEasy.EV.Infrastructure
{
    public class CategoryService : ICategoryService
    {
        private readonly ElectricVehiclesContext _EVContext;
        public CategoryService(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }

        public Category? GetCategoryById(int categoryId)
        {
            return _EVContext.Categories.FirstOrDefault(c => c.Id == categoryId);
        }

        public Category? GetCategoryByName(string categoryName)
        {
            return _EVContext.Categories.FirstOrDefault(c => c.Name == categoryName);
        }

        public Category CreateCategory(string categoryName)
        {
            return new Category { Name = categoryName };
        }

        public void AddCategoryToDB(Category category)
        {
            _EVContext.Add(category);
            _EVContext.SaveChanges();
        }

        public void RemoveCategoryFromDB(Category category)
        {
            _EVContext.Remove(category);
            _EVContext.SaveChanges();
        }

        public bool CheckIfCategoryExists(int categoryId)
        {
            return _EVContext.Categories.Any(c => c.Id == categoryId);
        }
    }
}
