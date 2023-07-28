using TravelEasy.EV.DB.Models.Diesel;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface ICategoryService
    {
        public Category? GetCategoryById(int categoryId);

        public Category? GetCategoryByName(string categoryName);

        public Category CreateCategory(string categoryName);

        public void AddCategoryToDB(Category category);

        public void RemoveCategoryFromDB(Category category);

        public bool CheckIfCategoryExists(int categoryId);
    }
}
