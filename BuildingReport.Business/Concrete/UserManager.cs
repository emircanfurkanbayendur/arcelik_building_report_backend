using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Concrete;
using BuildingReport.Business.Abstract;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{

    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;

        public UserManager()
        {
            _userRepository = new UserRepository();
        }

        public User CreateUser(User user)
        {
            return _userRepository.CreateUser(user);
        }

        public void DeleteUser(long id)
        {
            _userRepository.DeleteUser(id);
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetAllUsers();
        }

        public User GetUserById(long id)
        {
            return _userRepository.GetUserById(id);
        }

        public List<User> GetUsersByRole(long roleId)
        {
            return _userRepository.GetUsersByRole(roleId);
        }

        public User UpdateUser(User user)
        {
            return (_userRepository.UpdateUser(user));
        }

        public bool UserExists(string email)
        {
            return _userRepository.UserExists(email);
        }
    }
}
