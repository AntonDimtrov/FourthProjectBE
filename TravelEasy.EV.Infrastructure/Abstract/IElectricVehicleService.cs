using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.Infrastructure.Models.EVModels;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IElectricVehicleService
    {
        public bool CheckIfVehicleExists(int vehicleId);
        public bool VehicleIsBooked(int vehicleId);
        public ElectricVehicle GetVehicleByID(int vehicleId);
        public ICollection<ElectricVehicle> GetVehicles();
        public void AddVehicleToDB(ElectricVehicle vehicle);
        public void RemoveVehicleFromDB(ElectricVehicle vehicle);
        public AllEVResponseModel CreateAllEVResponseModel(int brandId, string model, decimal pricePerDay, string imageURL);
        public ICollection<AllEVResponseModel> CreateAllEVResponseModels(List<int> vehicleIds);
    }
}
