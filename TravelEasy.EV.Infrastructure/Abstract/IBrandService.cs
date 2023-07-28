using TravelEasy.EV.DB.Models.Diesel;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IBrandService
    {
        public Brand GetBrandById(int id);

        public Brand GetBrandByName(string name);

        public void AddBrandToDB(Brand brand);

        public void RemoveBrandFromDB(Brand brand);

        public bool CheckIfBrandExists(int brandId);
    }
}
