using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
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
        private readonly IMapper _mapper;
        public AuthorityManager(IMapper mapper)
        {
            _authorityRepository = new AuthorityRepository();
            _mapper = mapper;           
        }

        public Authority CreateAuthority(AuthorityDTO authorityDTO)
        {
            Authority authority = _mapper.Map<Authority>(authorityDTO);
            checkIfAuthorityExistsByName(authority.Name);
            return _authorityRepository.CreateAuthority(authority);
        }

        public void DeleteAuthority(long id)
        {
            checkIfAuthorityExistsById(id);
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

        public Authority UpdateAuthority(AuthorityDTO authorityDTO)
        {
            Authority authority = _mapper.Map<Authority>(authorityDTO);
            return _authorityRepository.UpdateAuthority(authority);
        }

        //BusinessRules
        public void checkIfAuthorityExistsByName(string name)
        {
            if (_authorityRepository.AuthorityExistsByName(name))
            {
                throw new NotImplementedException("Authority already exists.");
            }        
        }
        public void checkIfAuthorityExistsById(long id)
        {
            if (!_authorityRepository.AuthorityExistsById(id))
            {
                throw new NotImplementedException("Authority cannot find.");
            }
        }
    }
}
