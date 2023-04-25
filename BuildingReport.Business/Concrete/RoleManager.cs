using AutoMapper;
using BuildingReport.Business.Abstract;
using BuildingReport.Business.CustomExceptionMiddleware.AuthorityExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.IdExceptions;
using BuildingReport.Business.CustomExceptionMiddleware.RoleExceptions;
using BuildingReport.Business.CustomExceptions.RoleExceptions;
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
    public class RoleManager : IRoleService
    {
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleManager(IMapper mapper)
        {
            _roleRepository = new RoleRepository();
            _mapper = mapper;
        }
        public RoleResponse CreateRole(RoleRequest roleDTO)
        {
            _ = roleDTO ?? throw new ArgumentNullException(nameof(roleDTO)," cannot be null.");
            Role role = _mapper.Map<Role>(roleDTO);
            CheckIfRoleExistsByName(role.Name);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.CreateRole(role));
            return response;
        }

        public void DeleteRole(long id)
        {
            ValidateId(id);
            CheckIfRoleExistsById(id);
            _roleRepository.DeleteRole(id);
        }

        public List<RoleResponse> GetAllRoles()
        {
            List<RoleResponse> response = _mapper.Map<List<RoleResponse>>(_roleRepository.GetAllRoles());
            return response;
        }

        public RoleResponse GetRoleById(long id)
        {

            ValidateId(id);
            CheckIfRoleExistsById(id);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.GetRoleById(id));
            return response;
        }

        public RoleResponse UpdateRole(UpdateRoleRequest roleDTO)
        {
            CheckIfRoleExistsById(roleDTO.Id);
            Role role = _mapper.Map<Role>(roleDTO);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.UpdateRole(role));
            return response;
        }


        //BusinessRules

        public void CheckIfRoleExistsByName(string name)
        {
            if (_roleRepository.RoleExistsByName(name))
            {
                throw new RoleAlreadyExistsException("Role already exists.");
            }
        }
        public void CheckIfRoleExistsById(long id)
        {
            if (!_roleRepository.RoleExistsById(id))
            {
                throw new RoleNotFoundException("Role cannot be found.");
            }
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
