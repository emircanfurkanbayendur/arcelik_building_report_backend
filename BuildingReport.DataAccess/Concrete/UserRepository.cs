using arcelik_building_report_backend.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;

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

        public void DeleteUser(long id)
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

        public User GetUserById(long id)
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
                userDbContext.SaveChanges();
                return user;

            }
        }

        public bool UserExists(string email)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                return userDbContext.Users.Any(b => b.Email == email);
            }
        }
    }
}
