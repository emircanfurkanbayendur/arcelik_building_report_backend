using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Concrete
{

    public class AuthorityManager : IAuthorityService
    {
        private IAuthorityRepository _authorityRepository;
        public AuthorityManager()
        {
            _authorityRepository = new AuthorityRepository();
        }

        public Authority CreateAuthority(Authority authority)
        {
            return _authorityRepository.CreateAuthority(authority);
        }

        public void DeleteAuthority(long id)
        {
            _authorityRepository.DeleteAuthority(id);
        }

        public List<Authority> GetAllAuthorities()
        {
            return _authorityRepository.GetAllAuthorities();
        }

        public Authority GetAuthorityById(long id)
        {
            return _authorityRepository.GetAuthorityById(id);
        }

        public Authority UpdateAuthority(Authority authority)
        {
            return _authorityRepository.UpdateAuthority(authority);
        }
    }
}
