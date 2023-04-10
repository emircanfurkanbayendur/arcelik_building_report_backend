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
            Role role = _mapper.Map<Role>(roleDTO);
            CheckIfRoleExistsByName(role.Name);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.CreateRole(role));
            return response;
        }

        public void DeleteRole(long id)
        {
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
            CheckIfRoleExistsById(id);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.GetRoleById(id));
            return response;
        }

        public RoleResponse UpdateRole(UpdateRoleRequest roleDTO)
        {
            Role role = _mapper.Map<Role>(roleDTO);
            RoleResponse response = _mapper.Map<RoleResponse>(_roleRepository.UpdateRole(role));
            return response;
        }

        public void CheckIfRoleExistsByName(string name)
        {
            if (_roleRepository.RoleExistsByName(name))
            {
                throw new NotImplementedException("Role already exists.");
            }
        }
        public void CheckIfRoleExistsById(long id)
        {
            if (!_roleRepository.RoleExistsById(id))
            {
                throw new NotImplementedException("Role cannot found.");
            }
        }

    }
}
