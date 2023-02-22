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
    public class RoleManager : IRoleService
    {
        private IRoleRepository _roleRepository;

        public RoleManager()
        {
            _roleRepository = new RoleRepository();
        }
        public Role CreateRole(Role role)
        {
            return _roleRepository.CreateRole(role);
        }

        public void DeleteRole(long id)
        {
            _roleRepository.DeleteRole(id);
        }

        public List<Role> GetAllRoles()
        {
            return _roleRepository.GetAllRoles();
        }

        public Role GetRoleById(long id)
        {
            return _roleRepository.GetRoleById(id);
        }

        public Role UpdateRole(Role role)
        {
            return _roleRepository.UpdateRole(role);
        }
    }
}
