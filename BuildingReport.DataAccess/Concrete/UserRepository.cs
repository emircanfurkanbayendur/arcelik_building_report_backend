using arcelik_building_report_backend.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace arcelik_building_report_backend.Concrete
{
    public class UserRepository : IUserRepository
    {
        public User CreateUser(User user)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                var aa = userDbContext.Roles.Where(a => a.Id == user.RoleId).FirstOrDefault();
                user.Role = aa;
                userDbContext.Users.Add(user);
                userDbContext.SaveChanges();
                return GetUserById(user.Id);

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
                List<User> users = userDbContext.Users
                    //.Include(x => x.Role)
                    //.Include(x => x.Buildings)
                    //.Include(x => x.Documents)
                    .ToList();
                //return userDbContext.Users.ToList();

                return users;

            }
        }

        public User GetUserById(long id)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                User user = userDbContext.Users
                    .Include(x => x.Role)
                    .Include(x => x.Buildings)
                    .Include(x => x.Documents)
                    .First(s => s.Id == id);


                //return userDbContext.Users.Find(id);
                return user;

            }
        }

        public List<User> GetUsersByRole(long roleId)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                return userDbContext.Users
                    //.Include(x => x.Role)
                    //.Include(x => x.Buildings)
                    //.Include(x => x.Documents)
                    .Where(b => b.RoleId == roleId).ToList();
            }
        }

        public User UpdateUser(User user)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                userDbContext.Users.Update(user);
                userDbContext.SaveChanges();
                return GetUserById(user.Id);

            }
        }

        public User ChangeRoleToAdmin(User user)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                var aa = userDbContext.Roles.Where(a => a.Id == user.RoleId).FirstOrDefault();
                user.Role = aa;
                userDbContext.Users.Update(user);
                userDbContext.SaveChanges();
                return GetUserById(user.Id);

            }
        }

        public bool UserExistsByEmail(string email)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                return userDbContext.Users.Any(b => b.Email == email);
            }
        }

        public bool UserExistsById(long id)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                return userDbContext.Users.Any(b => b.Id == id);
            }
        }

        public bool findUserExistsByEmailAndPassword(string email, byte[] _password)
        {
            
            if (GetAllUsers().Any(u => u.Email == email && u.Password.SequenceEqual(_password)))
            {
                return true;
            }
            return false;
        }

        public User GetUserByEmail(string email)
        {
            using (var userDbContext = new ArcelikBuildingReportDbContext())
            {
                User user = userDbContext.Users
                    .Include(x => x.Role)
                    .First(s => s.Email == email);

                return user;
            }

            
        }
    }
}
