using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
                return role;
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
                return roleDbContext.Roles.ToList();
            }
        }

        public Role GetRoleById(long id)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                return roleDbContext.Roles.Find(id);
            }
        }

        public Role UpdateRole(Role role)
        {
            using (var roleDbContext = new ArcelikBuildingReportDbContext())
            {
                roleDbContext.Roles.Update(role);
                roleDbContext.SaveChanges();
                return role;
            }
        }
    }
}
