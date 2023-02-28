using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUserById(long id);

        User CreateUser(User user);

        User UpdateUser(User user);

        void DeleteUser(long id);
        bool UserExists(string email);
        List<User> GetUsersByRole(long roleId);

    }
}
