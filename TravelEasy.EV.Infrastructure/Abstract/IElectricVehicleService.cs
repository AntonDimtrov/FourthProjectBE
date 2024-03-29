﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEasy.ElectricVehicles.DB.Models;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IElectricVehicleService
    {
        public bool VehicleExists(int vehicleId);
        public bool VehicleIsBooked(int vehicleId);
        public ElectricVehicle GetVehicleByID(int vehicleId);
        public ICollection<ElectricVehicle> GetVehicles();
        public void AddVehicle(ElectricVehicle vehicle);
        public void RemoveVehicle(ElectricVehicle vehicle);
    }
}
