using System.Collections.Generic;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.Infrastructure.Abstract;
using TravelEasy.EV.Infrastructure.Models.EVModels;

namespace TravelEasy.EV.Infrastructure
{
    public class ElectricVehicleService : IElectricVehicleService
    {
        private readonly ElectricVehiclesContext _EVContext;
        private readonly IBrandService _brandService;
        private readonly IElectricVehicleService _vehicleService;
        private readonly ICategoryService _categoryService;
        public ElectricVehicleService(
            ElectricVehiclesContext EVContext,
            IBrandService brandService,
            IElectricVehicleService vehicleService,
            ICategoryService categoryService)
        {
            _EVContext = EVContext;
            _brandService = brandService;
            _vehicleService = vehicleService;
            _categoryService = categoryService;
        }

        public void AddVehicleToDB(ElectricVehicle vehicle)
        {
            _EVContext.ElectricVehicles.Add(vehicle);
            _EVContext.SaveChanges();
        }

        public void RemoveVehicleFromDB(ElectricVehicle vehicle)
        {
            _EVContext.ElectricVehicles.Remove(vehicle);
            _EVContext.SaveChanges();
        }

        public bool CheckIfVehicleExists(int vehicleId)
        {
            return _EVContext.ElectricVehicles.Any(ev => ev.Id == vehicleId);
        }

        public bool VehicleIsBooked(int vehicleId)
        {
            return _EVContext.Bookings.Where(ev => ev.Id == vehicleId).Any();
        }

        public ICollection<ElectricVehicle> GetVehicles()
        {
            return _EVContext.ElectricVehicles.ToList();
        }

        public ElectricVehicle? GetVehicleByID(int vehicleId)
        {
            ElectricVehicle? vehicle = _EVContext.ElectricVehicles
                .Where(ev => ev.Id == vehicleId)
                .FirstOrDefault();

            return vehicle;
        }

        public ICollection<ElectricVehicle> GetByBrands(ICollection<string> selectedBrandNames)
        {
            List<int> brandIds = selectedBrandNames
                .Select(bn => _brandService.GetBrandByName(bn).Id)
                .ToList();

            ICollection<ElectricVehicle> vehicles = GetVehicles()
                .Where(ev => brandIds.Contains(ev.BrandId))
                .ToList();

            return vehicles;
        }

        public ICollection<ElectricVehicle> GetByBrandIds(ICollection<string> brandNames)
        {
            List<int> brandIds = brandNames
                .Select(bn => _brandService
                .GetBrandByName(bn).Id)
                .ToList();

            return GetVehicles().Where(ev => brandIds.Contains(ev.BrandId)).ToList();
        }

        public ICollection<ElectricVehicle> GetByCategories(ICollection<string> selectedCategories)
        {
            List<int> categoryIds = selectedCategories
                  .Select(c => _categoryService.GetCategoryByName(c).Id)
                  .ToList();

            ICollection<ElectricVehicle> vehicles = GetVehicles()
                .Where(ev => categoryIds.Contains(ev.CategoryId))
                .ToList();

            return vehicles;
        }

        public ICollection<ElectricVehicle> GetByBrandsIds(ICollection<string> brandNames)
        {
            List<int> brandIds = brandNames
                .Select(bn => _brandService
                .GetBrandByName(bn).Id)
                .ToList();

            List<ElectricVehicle> vehicles = GetVehicles()
                .Where(ev => brandIds.Contains(ev.BrandId))
                .ToList();

            return vehicles;
        }

        public AllEVResponseModel CreateAllEVResponseModel(int brandId, string model, decimal pricePerDay, string imageURL)
        {
            AllEVResponseModel newModel = new()
            {
                BrandId = brandId,
                Model = model,
                PricePerDay = pricePerDay,
                ImageURL = imageURL
            };

            return newModel;
        }
        public ICollection<AllEVResponseModel> CreateAllEVResponseModels(List<int> vehicleIds)
        {
            ICollection<AllEVResponseModel> models = new List<AllEVResponseModel>();

            foreach (var vehicleId in vehicleIds)
            {
                ElectricVehicle vehicle = _vehicleService.GetVehicleByID(vehicleId);

                AllEVResponseModel newModel = new()
                {
                    BrandId = vehicle.BrandId,
                    Model = vehicle.Model,
                    PricePerDay = vehicle.PricePerDay,
                    ImageURL = vehicle.ImageURL
                };

                models.Add(newModel);
            }

            return models;
        }
    }
}
