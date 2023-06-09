using arcelik_building_report_backend.Abstract;
using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.RoleExceptions;
using BuildingReport.Business.CustomExceptions.RoleExceptions;
using BuildingReport.DataAccess.Abstract;
using BuildingReport.DataAccess.Concrete;
using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
using BuildingReport.Entities;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IUserRepository _userRepository;

        public RoleAuthorityManager(IMapper mapper, IRoleService roleService, IAuthorityService authorityService, IHttpContextAccessor httpContextAccessor)
        {
            _roleAuthorityRepository = new RoleAuthorityRepository();
            _mapper = mapper;
            _authorityService = authorityService;
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;
        }

        public RoleAuthorityResponse CreateRoleAuthority(RoleAuthorityRequest request)
        {
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var user = _userRepository.GetAllUsers().Where(u => u.Id == long.Parse(userIdString)).FirstOrDefault();
            if (!_roleAuthorityRepository.RoleAuthorityExistsById(user.RoleId, 2))
            {
                return null;
            }

            _ = request ?? throw new ArgumentNullException(nameof(request), "cannot be null.");

            RoleAuthority roleAuthority = _mapper.Map<RoleAuthority>(request);
            CheckIfRoleAuthorityExistsByName(_roleService.GetRoleById(request.RoleId).Name, _authorityService.GetAuthorityById(request.AuthorityId).Name);
            RoleAuthorityResponse response = _mapper.Map<RoleAuthorityResponse>(_roleAuthorityRepository.CreateRoleAuthority(roleAuthority));
            return response;
        }

        public bool DeleteRoleAuthority(long id)
        {
            // var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");

           // var user = _userRepository.GetAllUsers().Where(u => u.Id == long.Parse(userIdString)).FirstOrDefault();
            //if (!_roleAuthorityRepository.RoleAuthorityExistsById(user.RoleId, 3))
            //{
            //    return false;
            //}

            ValidateId(id);
            CheckIfRoleAuthorityExistsById(id);


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
            ValidateId(id);

            RoleAuthorityResponse response = _mapper.Map<RoleAuthorityResponse>(_roleAuthorityRepository.GetRoleAuthorityById(id));
            return response;
        }

        public RoleAuthorityResponse UpdateRoleAuthority(UpdateRoleAuthorityRequest roleAuthorityDTO)
        {
            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");

            var user = _userRepository.GetAllUsers().Where(u => u.Id == long.Parse(userIdString)).FirstOrDefault();

            if (!_roleAuthorityRepository.RoleAuthorityExistsById(user.RoleId, 4))
            {
                return null;
            }

            _ = roleAuthorityDTO ?? throw new ArgumentNullException(nameof(roleAuthorityDTO), "cannot be null.");
            CheckIfRoleAuthorityExistsById(roleAuthorityDTO.Id);

            var roleAuthority = _mapper.Map<RoleAuthority>(roleAuthorityDTO);
            RoleAuthorityResponse response = _mapper.Map<RoleAuthorityResponse>(_roleAuthorityRepository.UpdateRoleAuthority(roleAuthority));
            return response;
        }


        //BusinessRules
        public void CheckIfRoleAuthorityExistsByName(string roleName, string authorityName)
        {
            if (_roleAuthorityRepository.CheckIfRoleAuthorityExistsByName(roleName, authorityName))
            {
                throw new RoleAuthorityAlreadyExistsException("RoleAuthority already exists.");
            }


        }

        public void CheckIfRoleAuthorityExistsById(long id)
        {
            if (!_roleAuthorityRepository.CheckIfRoleAuthorityExistsById(id))
            {
                throw new RoleAuthorityNotFoundException("RoleAuthority Not Found.");
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

        private void ValidateId(long id)
        {
            if (id <= 0 || id > long.MaxValue)
            {
                throw new IdOutOfRangeException(nameof(id), id);
            }
        }


    }
}
