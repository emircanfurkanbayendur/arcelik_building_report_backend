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
    public class AuthorityRepository : IAuthorityRepository
    {
        public Authority CreateAuthority(Authority authority)
        {
            using (var authorityDbContext = new ArcelikBuildingReportDbContext())
            {
                authorityDbContext.Authorities.Add(authority);
                authorityDbContext.SaveChanges();  
                return authority;
            }
        }

        public void DeleteAuthority(long id)
        {
            using (var authorityDbContext = new ArcelikBuildingReportDbContext())
            {
                var deletedAuthority = GetAuthorityById(id);
                authorityDbContext.Authorities.Remove(deletedAuthority);
                authorityDbContext.SaveChanges();
            }
        }

        public List<Authority> GetAllAuthorities()
        {
            using (var authorityDbContext = new ArcelikBuildingReportDbContext())
            {
                return authorityDbContext.Authorities.ToList();
            }
        }

        public Authority GetAuthorityById(long id)
        {
            using (var authorityDbContext = new ArcelikBuildingReportDbContext())
            {
                return authorityDbContext.Authorities.Find(id);
            }
        }

        public Authority UpdateAuthority(Authority authority)
        {
            using (var authorityDbContext = new ArcelikBuildingReportDbContext())
            {
                authorityDbContext.Authorities.Update(authority);
                authorityDbContext.SaveChanges();
                return authority;
            }
        }
    }
}
