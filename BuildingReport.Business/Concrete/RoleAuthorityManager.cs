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
    public class RoleAuthorityManager : IRoleAuthorityService
    {
        private IRoleAuthorityRepository _roleAuthorityRepository;
        private IRoleService _roleService;
        private IAuthorityService _authorityService;
        private readonly IMapper _mapper;

        public RoleAuthorityManager(IMapper mapper, IRoleService roleService, IAuthorityService authorityService)
        {
            _roleAuthorityRepository = new RoleAuthorityRepository();
            _mapper = mapper;
            _authorityService = authorityService;
            _roleService = roleService;
            
        }

        public RoleAuthorityResponse CreateRoleAuthority(RoleAuthorityRequest request)
        {
            if (!_roleAuthorityRepository.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 2))
            {
                return null;
            }
            RoleAuthority roleAuthority = _mapper.Map<RoleAuthority>(request);
            RoleAuthorityExists(_roleService.GetRoleById(request.RoleId).Name, _authorityService.GetAuthorityById(request.AuthorityId).Name);
            RoleAuthorityResponse response = _mapper.Map<RoleAuthorityResponse>(_roleAuthorityRepository.CreateRoleAuthority(roleAuthority));
            return response;
        }

        public bool DeleteRoleAuthority(long id)
        {
            if (!_roleAuthorityRepository.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 3))
            {
                return false;
            }
            _roleAuthorityRepository.DeleteRoleAuthority(id);
            return true;
        }


        public List<RoleAuthorityResponse> GetAllRoleAuthorities()
        {
            List<RoleAuthorityResponse> response = _mapper.Map<List<RoleAuthorityResponse>>(_roleAuthorityRepository.GetAllRoleAuthorities());
            return response;
        }

        public RoleAuthorityResponse GetRoleAuthorityById(long id)
        {
            RoleAuthorityResponse response = _mapper.Map<RoleAuthorityResponse>(_roleAuthorityRepository.GetRoleAuthorityById(id));
            return response;
        }

        public RoleAuthorityResponse UpdateRoleAuthority(UpdateRoleAuthorityRequest roleAuthorityDTO)
        {
            if (!_roleAuthorityRepository.RoleAuthorityExistsById(UserManager.LoginUser.RoleId, 4))
            {
                return null;
            }
            var roleAuthority = _mapper.Map<RoleAuthority>(roleAuthorityDTO);
            RoleAuthorityResponse response = _mapper.Map<RoleAuthorityResponse>(_roleAuthorityRepository.UpdateRoleAuthority(roleAuthority));
            return response;
        }
        public void RoleAuthorityExists(string roleName, string authorityName)
        {
            if (_roleAuthorityRepository.RoleAuthorityExists(roleName, authorityName))
            {
                throw new NotImplementedException("RoleAuthority already exists.");
            }
        }

        public bool RoleAuthorityExistsById(long roleId, long authorityId)
        {
            if (_roleAuthorityRepository.RoleAuthorityExistsById(roleId, authorityId))
            {
                return true;
            }
            return false;
        }
    }
}
