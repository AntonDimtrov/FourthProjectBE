using System.Collections.Generic;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.DB.Models.Diesel;
using TravelEasy.EV.Infrastructure.Abstract;
using TravelEasy.EV.Infrastructure.Models.EVModels;

namespace TravelEasy.EV.Infrastructure
{
    public class ElectricVehicleService : IElectricVehicleService
    {
        private readonly ElectricVehiclesContext _EVContext;
        private readonly IBrandService _brandService;
        private readonly ICategoryService _categoryService;
        public ElectricVehicleService(
            ElectricVehiclesContext EVContext,
            IBrandService brandService,
            ICategoryService categoryService)
        {
            _EVContext = EVContext;
            _brandService = brandService;
            _categoryService = categoryService;
        }
        public bool ExistingVehiclesInDB()
        {
            return _EVContext.ElectricVehicles.Any();
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
                ElectricVehicle vehicle = GetVehicleByID(vehicleId);

                AllEVResponseModel newModel = CreateAllEVResponseModel(vehicle.BrandId, vehicle.Model, vehicle.PricePerDay, vehicle.ImageURL);

                models.Add(newModel);
            }

            return models;
        }

        public EVResponseModel CreateEVResponseModel(int brandId, string model, int horsePower, int Range, decimal pricePerDay, string imageURL, int categoryId)
        {
            EVResponseModel newModel = new()
            {
                BrandId = brandId,
                Model = model,
                HorsePowers = horsePower,
                Range = Range,
                PricePerDay = pricePerDay,
                ImageURL = imageURL,
                CategoryId = categoryId
            };

            return newModel;
        }

        public EVResponseModel CreateEVResponseModel(ElectricVehicle ev)
        {
            EVResponseModel newModel = new()
            {
                BrandId = ev.BrandId,
                Model = ev.Model,
                HorsePowers = ev.HorsePowers,
                Range = ev.Range,
                PricePerDay = ev.PricePerDay,
                ImageURL = ev.ImageURL,
                CategoryId = ev.CategoryId
            };

            return newModel;
        }

        public ICollection<EVResponseModel> CreateEVResponseModels(List<int> vehicleIds)
        {
            ICollection<EVResponseModel> models = new List<EVResponseModel>();

            foreach (var vehicleId in vehicleIds)
            {
                EVResponseModel newModel = CreateEVResponseModel(GetVehicleByID(vehicleId));

                models.Add(newModel);
            }

            return models;
        }
    }
}
