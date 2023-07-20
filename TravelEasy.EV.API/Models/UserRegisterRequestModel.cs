namespace TravelEasy.EV.API.Models
{
    public class UserRegisterRequestModel
    {
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
