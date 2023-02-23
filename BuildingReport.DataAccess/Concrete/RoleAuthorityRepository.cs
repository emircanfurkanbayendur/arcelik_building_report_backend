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
    public class RoleAuthorityRepository : IRoleAuthorityRepository
    {
        public RoleAuthority CreateRoleAuthority(RoleAuthority roleAuthority)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                roleAuthorityDbContext.RoleAuthorities.Add(roleAuthority);
                roleAuthorityDbContext.SaveChanges();
                return roleAuthority;
            }
        }

        public void DeleteRoleAuthority(long id)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                var deletedRoleAuthority = GetRoleAuthorityById(id);
                roleAuthorityDbContext.RoleAuthorities.Remove(deletedRoleAuthority);
                roleAuthorityDbContext.SaveChanges();
            }
        }


        public List<RoleAuthority> GetAllRoleAuthorities()
        {
            using(var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                return roleAuthorityDbContext.RoleAuthorities.ToList();
            }
        }



        public RoleAuthority GetRoleAuthorityById(long id)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                return roleAuthorityDbContext.RoleAuthorities.Find(id);

            }
        }

        public RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                roleAuthorityDbContext.RoleAuthorities.Update(roleAuthority);
                roleAuthorityDbContext.SaveChanges();
                return roleAuthority;

            }
        }
    }
}
