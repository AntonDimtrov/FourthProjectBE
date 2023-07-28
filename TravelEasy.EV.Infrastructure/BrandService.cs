using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Abstract;

namespace TravelEasy.EV.Infrastructure
{
    public class BrandService : IBrandService
    {
        private readonly ElectricVehiclesContext _EVContext;
        public BrandService(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }

        public void AddBrandToDB(Brand brand)
        {
            _EVContext.Brands.Add(brand);
            _EVContext.SaveChanges();
        }

        public void RemoveBrandFromDB(Brand brand)
        {
            _EVContext.Brands.Remove(brand);
            _EVContext.SaveChanges();
        }

        public Brand GetBrandById(int brandId)
        {
            return _EVContext.Brands.Where(b => b.Id == brandId).FirstOrDefault();
        }

        public Brand GetBrandByName(string name)
        {
            return _EVContext.Brands.Where(b => b.Name == name).FirstOrDefault();
        }

        public bool CheckIfBrandExists(int brandId)
        {
            return _EVContext.Brands.Any(b => b.Id == brandId);
        }
    }
}
