using BuildingReport.Entities;

namespace arcelik_building_report_backend.Abstract
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        User GetUserById(long id);

        User CreateUser(User user);

        User UpdateUser(User user);

        void DeleteUser(long id);


    }
}
