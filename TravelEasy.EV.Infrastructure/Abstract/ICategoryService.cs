using TravelEasy.EV.DB.Models.Diesel;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface ICategoryService
    {
        public Category? GetCategoryById(int categoryId);
        public Category? GetCategoryByName(string categoryName);
        public Category CreateCategory(string categoryName);
        public void AddCategory(Category category);
        public void RemoveCategory(Category category);
    }
}
