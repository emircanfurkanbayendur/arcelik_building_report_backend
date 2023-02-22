using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Abstract
{
    public interface IAuthorityRepository
    {
        List<Authority> GetAllAuthorities();

        Authority GetAuthorityById(int id);

        Authority CreateAuthority(Authority authority);

        Authority UpdateAuthority(Authority authority);

        Authority DeleteAuthority(int id);
    }
}
