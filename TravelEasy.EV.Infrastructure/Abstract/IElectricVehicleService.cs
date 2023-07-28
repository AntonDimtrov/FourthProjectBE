using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.Infrastructure.Models.EVModels;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IElectricVehicleService
    {
        public bool CheckIfVehicleExists(int vehicleId);

        public ElectricVehicle? GetVehicleByID(int vehicleId);

        public ICollection<ElectricVehicle> GetVehicles();

        public void AddVehicleToDB(ElectricVehicle vehicle);

        public void RemoveVehicleFromDB(ElectricVehicle vehicle);

        public AllEVResponseModel CreateAllEVResponseModel(int brandId, string model, decimal pricePerDay, string imageURL);

        public ICollection<AllEVResponseModel> CreateAllEVResponseModels(List<int> vehicleIds);

        EVResponseModel CreateEVResponseModel(int brandId, string model, int horsePower, int Range, decimal pricePerDay, string imageURL, int categoryId);
        
        ICollection<EVResponseModel> CreateEVResponseModels(List<int> vehicleIds);

        ICollection<ElectricVehicle> GetByBrandIds(ICollection<string> brandNames);

        ICollection<ElectricVehicle> GetByCategories(ICollection<string> selectedCategories);

        ICollection<ElectricVehicle> GetByBrandsIds(ICollection<string> brandNames);

        ICollection<ElectricVehicle> GetByBrands(ICollection<string> selectedBrandNames);
        bool ExistingVehiclesInDB();
        EVResponseModel CreateEVResponseModel(ElectricVehicle ev);
    }
}
