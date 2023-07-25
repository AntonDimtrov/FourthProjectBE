using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;

namespace TravelEasy.EV.Infrastructure
{
    public class ElectricVehicleService : IElectricVehicleService
    {
        private readonly ElectricVehiclesContext _EVContext;
        public ElectricVehicleService(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }
        public ElectricVehicle GetVehicleByID(int vehicleId)
        {
            return _EVContext.ElectricVehicles.Where(ev => ev.Id == vehicleId).FirstOrDefault();
        }
        public bool VehicleExists(int vehicleId)
        {
            return _EVContext.ElectricVehicles.Where(ev => ev.Id == vehicleId).Any();
        }
        public bool VehicleIsBooked(int vehicleId)
        {
            return _EVContext.Bookings.Where(ev => ev.Id == vehicleId).Any();
        }
    }
}
