using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;

namespace TravelEasy.EV.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly ElectricVehiclesContext _EVContext;
        public UserService(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }
        public bool ExistingUsersInDB()
        {
            return _EVContext.Users.Any();
        }
        public User GetUserByUsername(string username)
        {
            return _EVContext.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public User GetUserByID(int userid)
        {
            return _EVContext.Users.Where(u => u.Id == userid).FirstOrDefault();
        }

        public bool UserExists(int userId)
        {
            return _EVContext.Users.Where(u => u.Id == userId).Any();
        }

        public ICollection<User> GetUsers()
        {
            return _EVContext.Users.ToList();
        }

        public void AddUser(User user)
        {
            _EVContext.Users.Add(user);
            _EVContext.SaveChanges();
        }

        public void RemoveUser(User user)
        {
            _EVContext.Users.Remove(user);
            _EVContext.SaveChanges();
        }
    }
}