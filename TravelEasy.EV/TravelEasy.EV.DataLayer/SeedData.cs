using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelEasy.ElectricVehicles.DB.Models;

namespace TravelEasy.EV.DataLayer
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ElectricVehiclesContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ElectricVehiclesContext>>()))
            {
                if (context == null || context.ElectricVehicles == null)
                {
                    throw new ArgumentNullException("Null ElectricVehiclesContext");
                }

                // Look for any EV.
                if (context.ElectricVehicles.Any())
                {
                    return;   // DB has been seeded
                }

                context.ElectricVehicles.AddRange(
                    new ElectricVehicle
                    {
                        Brand = "VW",
                        HorsePowers = 100,
                        Model = "E-up!",
                        Range = 300,
                        PricePerDay = 40
                    },
                    new ElectricVehicle
                    {
                        Brand = "VW",
                        HorsePowers = 130,
                        Model = "E-GOLF",
                        Range = 320,
                        PricePerDay = 50
                    },
                    new ElectricVehicle
                    {
                        Brand = "Hyundai",
                        HorsePowers = 150,
                        Model = "Kona 40",
                        Range = 350,
                        PricePerDay = 60
                    },
                    new ElectricVehicle
                    {
                        Brand = "Nissan",
                        HorsePowers = 150,
                        Model = "Leaf 40",
                        Range = 340,
                        PricePerDay = 50
                    },
                    new ElectricVehicle
                    {
                        Brand = "Renault",
                        HorsePowers = 110,
                        Model = "Zoe 40",
                        Range = 320,
                        PricePerDay = 50
                    },
                    new ElectricVehicle
                    {
                        Brand = "Skoda",
                        HorsePowers = 110,
                        Model = "CITIGO e iV",
                        Range = 300,
                        PricePerDay = 40
                    },
                    new ElectricVehicle
                    {
                        Brand = "Dacia",
                        HorsePowers = 130,
                        Model = "Spring",
                        Range = 330,
                        PricePerDay = 40
                    }
                    ,
                    new ElectricVehicle
                    {
                        Brand = "Huyndai",
                        HorsePowers = 170,
                        Model = "IONIQ 30",
                        Range = 350,
                        PricePerDay = 90
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
