using arcelik_building_report_backend.Abstract;
using arcelik_building_report_backend.Models;

namespace arcelik_building_report_backend.Concrete
{
    public class UserRepository : IUserRepository
    {
        public User CreateUser(User user)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                userDbContext.Users.Add(user);
                userDbContext.SaveChanges();
                return user;

            }
        }

        public void DeleteUser(int id)
        {
            var user = new User() {Id = id, IsActive = false};
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {

                userDbContext.Attach(user);
                userDbContext.Entry(user).Property(x => x.IsActive).IsModified = true;
                userDbContext.SaveChanges();
                
                

            }
        }

        public List<User> GetAllUsers()
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                return userDbContext.Users.ToList();

            }
        }

        public User GetUserById(int id)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                return userDbContext.Users.Find(id);

            }
        }

        public User UpdateUser(User user)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                userDbContext.Users.Update(user);
                return user;

            }
        }
    }
}
