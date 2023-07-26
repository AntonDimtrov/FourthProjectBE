using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.Infrastructure.Abstract;

namespace TravelEasy.EV.Infrastructure
{
    public class ElectricVehicleService : IElectricVehicleService
    {
        private readonly ElectricVehiclesContext _EVContext;
        public ElectricVehicleService(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }

        public void AddVehicle(ElectricVehicle vehicle)
        {
            _EVContext.ElectricVehicles.Add(vehicle);
            _EVContext.SaveChanges();
        }

        public ElectricVehicle GetVehicleByID(int vehicleId)
        {
            return _EVContext.ElectricVehicles.Where(ev => ev.Id == vehicleId).FirstOrDefault();
        }

        public ICollection<ElectricVehicle> GetVehicles()
        {
            return _EVContext.ElectricVehicles.ToList();
        }

        public void RemoveVehicle(ElectricVehicle vehicle)
        {
            _EVContext.ElectricVehicles.Remove(vehicle);
            _EVContext.SaveChanges();
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
