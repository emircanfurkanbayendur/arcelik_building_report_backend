﻿using BuildingReport.Entities;
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

        Authority GetAuthorityById(long id);

        Authority CreateAuthority(Authority authority);

        Authority UpdateAuthority(Authority authority);

        List<Authority> GetAuthoritiesByEmail(string email);

        void DeleteAuthority(long id);
        bool AuthorityExistsByName(string name);
        bool AuthorityExistsById(long id);

    }
}
