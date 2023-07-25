using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEasy.ElectricVehicles.DB.Models;

namespace TravelEasy.EV.Infrastructure
{
    public interface IUserService
    {
        public bool UserExists(int userid);
        public User GetUserByID(int userid);
        public bool ExistingUsersInDB();
        public User GetUserByUsername(string username);
        public ICollection<User> GetUsers();
        public void AddUser(User user);
        public void RemoveUser(User user);

    }
}
