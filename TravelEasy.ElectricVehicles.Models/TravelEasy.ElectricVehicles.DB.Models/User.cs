using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEasy.ElectricVehicles.DB.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? FirstName { get; set; } = null;

        [Required]
        public string? LastName { get; set; } = null;

        [Required]
        public string? Username { get; set; } = null;

        [Required]
        public string? Email { get; set; } = null;

        [Required]
        public string? Password { get; set; } = null;

        [Required]
        public string? PhoneNumber { get; set; } = null;
    }
}
