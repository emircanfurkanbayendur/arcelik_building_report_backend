using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        public Role CreateRole(Role role)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                roleDbContext.Roles.Add(role);
                roleDbContext.SaveChanges();
                return GetRoleById(role.Id);
            }
        }

        public void DeleteRole(long id)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                var deletedRole = GetRoleById(id);
                roleDbContext.Roles.Remove(deletedRole);
                roleDbContext.SaveChanges();
                
            }
        }

        public List<Role> GetAllRoles()
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                List<Role> role = roleDbContext.Roles.Include(x => x.Users).ToList();
                //return roleDbContext.Roles.ToList();
                return role;
            }
        }

        public Role GetRoleById(long id)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                Role role = roleDbContext.Roles.Include(x => x.Users).First(s => s.Id == id);
                //return roleDbContext.Roles.Find(id);
                return role;
            }
        }

        public bool RoleExists(string name)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                return roleDbContext.Roles.Any(b => b.Name == name);
            }
        }

        public Role UpdateRole(Role role)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                roleDbContext.Roles.Update(role);
                roleDbContext.SaveChanges();
                return GetRoleById(role.Id);
            }
        }
    }
}
