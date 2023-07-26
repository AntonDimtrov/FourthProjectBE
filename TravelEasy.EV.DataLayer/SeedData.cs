using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DB.Models.Diesel;

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
                        BrandId = 1,
                        HorsePowers = 369,
                        Model = "BMW i8",
                        Range = 252,
                        PricePerDay = 47,
                        ImageURL = "28fa30d1a3441f41a1b04dfb5fe5bea6.png",
                        CategoryId = 1
                    },
                    new ElectricVehicle 
                    {
                        BrandId = 1,
                        HorsePowers = 516,
                        Model = "BMW iX",
                        Range = 382,
                        PricePerDay = 112,
                        ImageURL = "813bd72757481e107ee6dcf44c5776f4.png",
                        CategoryId = 1
                    },
                    new ElectricVehicle
                    {
                        BrandId = 1,
                        HorsePowers = 281,
                        Model = "BMW i4",
                        Range = 390,
                        PricePerDay = 94,
                        ImageURL = "f7299783392a5924b84df5b0541c9e37.png",
                        CategoryId = 1
                    },
                    new ElectricVehicle 
                    {
                        BrandId = 2,
                        HorsePowers = 394,
                        Model = "I-Pace",
                        Range = 470,
                        PricePerDay = 91,
                        ImageURL = "6b1fc26c04412bdf6cbabe51379f4594.png",
                        CategoryId = 1
                    },
                    new ElectricVehicle
                    {
                        BrandId = 3,
                        HorsePowers = 110,
                        Model = "Model S",
                        Range = 520,
                        PricePerDay = 50,
                        ImageURL = "2a9371e9381cba875104b1d67da9d7de.png",
                        CategoryId = 2
                    },
                    new ElectricVehicle 
                    {
                        BrandId = 3,
                        HorsePowers = 110,
                        Model = "Model E",
                        Range = 670,
                        PricePerDay = 66,
                        ImageURL = "28fa30d1a3441f41a1b04dfb5fe5bea6.png",
                        CategoryId = 2
                    },
                    new ElectricVehicle
                    {
                        BrandId = 3,
                        HorsePowers = 130,
                        Model = "Model X",
                        Range = 632,
                        PricePerDay = 67,
                        ImageURL = "332d75cbbd2e5b975a79beafd30e1493.png",
                        CategoryId = 2
                    }
                    ,
                    new ElectricVehicle
                    {
                        BrandId = 3,
                        HorsePowers = 432,
                        Model = "Model Y",
                        Range = 570,
                        PricePerDay = 90,
                        ImageURL = "39e0cf1d23c2fe8aff669300e27936da.png",
                        CategoryId = 2
                    }
                    ,
                    new ElectricVehicle
                    {
                        BrandId = 1,
                        HorsePowers = 342,
                        Model = "BWM i8",
                        Range = 252,
                        PricePerDay = 77,
                        ImageURL = "a71599bb8d2770ac3a87afd39362051a.png",
                        CategoryId = 3
                    },
                    new ElectricVehicle
                    {
                        BrandId = 3,
                        HorsePowers = 232,
                        Model = "Roadster",
                        Range = 382,
                        PricePerDay = 112,
                        ImageURL = "c36834cce48dde330e08c03824ac4978.png",
                        CategoryId = 3
                    },
                    new ElectricVehicle
                    {
                        BrandId = 4,
                        HorsePowers = 324,
                        Model = "e-tron GT",
                        Range = 390,
                        PricePerDay = 98,
                        ImageURL = "d59c6073409c091cf844855d354872b1.png",
                        CategoryId = 4
                    },
                    new ElectricVehicle
                    {
                        BrandId = 5,
                        HorsePowers = 321,
                        Model = "Taycan",
                        Range = 474,
                        PricePerDay = 100,
                        ImageURL = "2e673b8641b37bd48ecd810eb952e5c6.png",
                        CategoryId = 4
                    },
                    new ElectricVehicle
                    {
                        BrandId = 6,
                        HorsePowers = 203,
                        Model = "XC40 P8",
                        Range = 320,
                        PricePerDay = 76,
                        ImageURL = "a506abeecbc3cb49084f74fedb557447.png",
                        CategoryId = 4
                    },
                    new ElectricVehicle
                    {
                        BrandId = 4,
                        HorsePowers = 209,
                        Model = "Q8 e-tron",
                        Range = 376,
                        PricePerDay = 56,
                        ImageURL = "5d84045775c10d49cd7c9d5ac7ec9e98.png",
                        CategoryId = 4
                    }
                );

                context.Brand.AddRange(
                    new Brand
                    {
                       Name="BMW"
                    },
                    new Brand
                    {
                        Name = "Jaguar"
                    },
                    new Brand
                    {
                        Name = "Tesla"
                    },
                    new Brand
                    {
                        Name = "Audi"
                    },
                    new Brand
                    {
                        Name = "Porsche"
                    },
                    new Brand
                    {
                        Name = "Volvo"
                    });

                context.Category.AddRange(
                   new Category
                   {
                       Name = "recently-added"
                   },
                   new Category
                   {
                       Name = "best-range"
                   },
                   new Category
                   {
                       Name = "summer-fit"
                   },
                   new Category
                   {
                       Name = "most-rented"
                   });

                context.SaveChanges();
            }
        }
    }
}
