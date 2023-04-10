using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DataAccess.Abstract
{
    public interface IRoleAuthorityRepository
    {
        List<RoleAuthority> GetAllRoleAuthorities();

        RoleAuthority GetRoleAuthorityById(long id);

        RoleAuthority CreateRoleAuthority(RoleAuthority roleAuthority);

        RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority);

        void DeleteRoleAuthority(long id);

        bool RoleAuthorityExists(string roleName, string authorityName);
        bool RoleAuthorityExistsById(long roleId, long authorityId);


    }
}
