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
    public class RoleAuthorityManager : IRoleAuthorityService
    {
        private IRoleAuthorityRepository _roleAuthorityRepository;
        private readonly IMapper _mapper;

        public RoleAuthorityManager(IMapper mapper)
        {
            _roleAuthorityRepository = new RoleAuthorityRepository();
            _mapper = mapper;
        }

        public RoleAuthority CreateRoleAuthority(RoleAuthorityDTO roleAuthorityDTO)
        {
            RoleAuthority roleAuthority = _mapper.Map<RoleAuthority>(roleAuthorityDTO);
            return _roleAuthorityRepository.CreateRoleAuthority(roleAuthority);
        }

        public void DeleteRoleAuthority(long id)
        {
            _roleAuthorityRepository.DeleteRoleAuthority(id);
        }


        public List<RoleAuthority> GetAllRoleAuthorities()
        {
            return _roleAuthorityRepository.GetAllRoleAuthorities();
        }

        public RoleAuthority GetRoleAuthorityById(long id)
        {
            return _roleAuthorityRepository.GetRoleAuthorityById(id);
        }

        public RoleAuthority UpdateRoleAuthority(RoleAuthorityDTO roleAuthorityDTO)
        {
            var roleAuthority = _mapper.Map<RoleAuthority>(roleAuthorityDTO);
            return _roleAuthorityRepository.UpdateRoleAuthority(roleAuthority);
        }
        public void RoleAuthorityExists(string roleName, string authorityName)
        {
            if (_roleAuthorityRepository.RoleAuthorityExists(roleName, authorityName))
            {
                throw new NotImplementedException("RoleAuthority already exists.");
            }
        }
    }
}
