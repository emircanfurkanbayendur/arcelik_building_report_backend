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
    public class RoleManager : IRoleService
    {
        private IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleManager(IMapper mapper)
        {
            _roleRepository = new RoleRepository();
            _mapper = mapper;
        }
        public Role CreateRole(RoleDTO roleDTO)
        {
            Role role = _mapper.Map<Role>(roleDTO);
            CheckIfRoleExistsByName(role.Name);
            return _roleRepository.CreateRole(role);
        }

        public void DeleteRole(long id)
        {
            CheckIfRoleExistsById(id);
            _roleRepository.DeleteRole(id);
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public Role GetRoleById(long id)
        {
            CheckIfRoleExistsById(id);
            return _roleRepository.GetRoleById(id);
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

        public Role UpdateRole(RoleDTO roleDTO)
        {
            Role role = _mapper.Map<Role>(roleDTO);
            return _roleRepository.UpdateRole(role);
        }
    }
}
