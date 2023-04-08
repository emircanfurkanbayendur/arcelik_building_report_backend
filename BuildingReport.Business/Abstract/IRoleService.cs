using BuildingReport.DTO;
using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();

        Role GetRoleById(long id);

        Role CreateRole(RoleDTO roleDTO);

        Role UpdateRole(RoleDTO roleDTO);

        void DeleteRole(long id);
        void CheckIfRoleExistsByName(string name);
        void CheckIfRoleExistsById(long id);

    }
}
