using BuildingReport.Entities;

namespace arcelik_building_report_backend.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        List<User> GetUsersByRole(long roleId);

        User GetUserById(long id);

        User CreateUser(User user);

        User UpdateUser(User user);

        void DeleteUser(long id);

        bool UserExistsByEmail(string email);
        bool UserExistsById(long id);
        bool findUserExistsByEmailAndPassword(string email, byte[] _password);
    }
}
