using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Data;
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

        public AuthorityResponse CreateAuthority(AuthorityRequest request )
        {
            Authority authority = _mapper.Map<Authority>(request);
            checkIfAuthorityExistsByName(authority.Name);
            AuthorityResponse response = _mapper.Map<AuthorityResponse>(_authorityRepository.CreateAuthority(authority));
            return response;
        }

        public void DeleteAuthority(long id)
        {
            checkIfAuthorityExistsById(id);
            _authorityRepository.DeleteAuthority(id);
        }

        public List<AuthorityResponse> GetAllAuthorities()
        {
            List<AuthorityResponse> response = _mapper.Map<List<AuthorityResponse>>(_authorityRepository.GetAllAuthorities());
            return response;
        }

        public AuthorityResponse GetAuthorityById(long id)
        {
            AuthorityResponse response = _mapper.Map<AuthorityResponse>(_authorityRepository.GetAuthorityById(id));
            return response;
        }

        public AuthorityResponse UpdateAuthority(UpdateAuthorityRequest authorityDTO)
        {
           
            Authority authority = _mapper.Map<Authority>(authorityDTO);
            AuthorityResponse response = _mapper.Map<AuthorityResponse>(_authorityRepository.UpdateAuthority(authority));
            return response;
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
