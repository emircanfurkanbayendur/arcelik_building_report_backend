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

        Role CreateRole(Role role);

        Role UpdateRole(Role role);

        void DeleteRole(long id);

    }
}
