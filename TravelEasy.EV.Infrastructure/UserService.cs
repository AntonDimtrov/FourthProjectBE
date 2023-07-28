using TravelEasy.ElectricVehicles.DB.Models;
using TravelEasy.EV.DataLayer;
using TravelEasy.EV.Infrastructure.Abstract;

namespace TravelEasy.EV.Infrastructure
{
    public class UserService : IUserService
    {
        private readonly ElectricVehiclesContext _EVContext;
        public UserService(ElectricVehiclesContext EVContext)
        {
            _EVContext = EVContext;
        }
        public void AddUserToDB(User user)
        {
            _EVContext.Users.Add(user);
            _EVContext.SaveChanges();
        }

        public void RemoveUserFromDB(User user)
        {
            _EVContext.Users.Remove(user);
            _EVContext.SaveChanges();
        }

        public bool ExistingUsersInDB()
        {
            return _EVContext.Users.Any();
        }

        public bool CheckIfUserExistsById(int userId)
        {
            return _EVContext.Users.Any(u => u.Id == userId);
        }

        public bool CheckIfUserExistsByUsername(string username)
        {
            return _EVContext.Users.Any(u => u.Username == username);
        }

        public User GetUserByUsername(string username)
        {
            return _EVContext.Users.Where(u => u.Username == username).FirstOrDefault();
        }

        public User GetUserByID(int userid)
        {
            return _EVContext.Users.Where(u => u.Id == userid).FirstOrDefault();
        }
       
        public ICollection<User> GetUsers()
        {
            return _EVContext.Users.ToList();
        }

        public int RegisterUser(string username, string email, string password)
        {
            User user = new()
            {
                Username = username,
                Email = email,
                Password = password
            };

            AddUserToDB(user);

            return user.Id;
        }
    }
}