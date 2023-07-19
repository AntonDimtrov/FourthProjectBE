using System.ComponentModel.DataAnnotations;

namespace TravelEasy.ElectricVehicles.DB.Models
{
    public class ElectricVehicle
    {
        [Key]
        public int Id { get; set; } 

        [Required]
        public string? Brand { get; set; }

        [Required]
        public string? Model { get; set; }

        [Required]
        public int HorsePowers { get; set; }

        [Required]
        public int Range { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }
    }
}