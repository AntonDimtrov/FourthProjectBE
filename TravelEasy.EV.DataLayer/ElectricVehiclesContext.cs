using Microsoft.EntityFrameworkCore;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DB.Models.Diesel;

namespace TravelEasy.EV.DataLayer
{
    public class ElectricVehiclesContext : DbContext
    {
        public DbSet<ElectricVehicle> ElectricVehicles { get; set;}
        public DbSet<User> Users { get; set;}

        public DbSet<Booking> Bookings { get; set;}

        public ElectricVehiclesContext()
        { }

        public ElectricVehiclesContext(DbContextOptions<ElectricVehiclesContext> options)
           : base(options)
        {}
    }
}