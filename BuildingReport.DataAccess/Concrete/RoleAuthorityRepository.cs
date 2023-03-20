using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAcess;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BuildingReport.DataAccess.Concrete
{
    public class RoleAuthorityRepository : IRoleAuthorityRepository
    {
        public RoleAuthority CreateRoleAuthority(RoleAuthority roleAuthority)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                //roleAuthorityDbContext.Entry(roleAuthority.Role).State = EntityState.Unchanged;
                //roleAuthorityDbContext.Entry(roleAuthority.Authority).State = EntityState.Unchanged;

                roleAuthorityDbContext.RoleAuthorities.Add(roleAuthority);
                roleAuthorityDbContext.SaveChanges();
                return GetRoleAuthorityById(roleAuthority.Id);
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
                List<RoleAuthority> ra = roleAuthorityDbContext.RoleAuthorities.Include(x => x.Role).Include(x => x.Authority).ToList();

                //return roleAuthorityDbContext.RoleAuthorities.ToList();

                return ra;

            }
        }



        public RoleAuthority GetRoleAuthorityById(long id)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                RoleAuthority ra = roleAuthorityDbContext
                    .RoleAuthorities.Include(x => x.Role).Include(x => x.Authority).First(s => s.Id == id);

                //return roleAuthorityDbContext.RoleAuthorities.Find(id);

                return ra;

                

            }
        }

        public RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                roleAuthorityDbContext.RoleAuthorities.Update(roleAuthority);
                roleAuthorityDbContext.SaveChanges();
                return GetRoleAuthorityById(roleAuthority.Id);

            }
        }

        public bool RoleAuthorityExists(string roleName, string authorityName)
        {
            using (var roleAuthorityDbContext = new ArcelikBuildingReportDbContext())
            {
                return roleAuthorityDbContext.RoleAuthorities.Any(r => r.Authority.Name == authorityName && r.Role.Name == roleName);

            }
        }

    }
}
