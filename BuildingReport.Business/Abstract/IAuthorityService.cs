using BuildingReport.DTO;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IAuthorityService
    {
        List<Authority> GetAllAuthorities();

        Authority GetAuthorityById(long id);

        Authority CreateAuthority(AuthorityDTO authorityDTO);

        Authority UpdateAuthority(AuthorityDTO authority);

        void DeleteAuthority(long id);
        void checkIfAuthorityExistsByName(string name);
        void checkIfAuthorityExistsById(long id);
    }
}
