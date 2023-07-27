using TravelEasy.ElectricVehicles.DB.Models;

namespace TravelEasy.EV.Infrastructure.Abstract
{
    public interface IUserService
    {
        public bool CheckIfUserExists(int userid);
        public User GetUserByID(int userid);
        public bool ExistingUsersInDB();
        public User GetUserByUsername(string username);
        public ICollection<User> GetUsers();
        public void AddUserToDB(User user);
        public void RemoveUserFromDB(User user);
        public int RegisterUser(string username, string email, string password);
    }
}
