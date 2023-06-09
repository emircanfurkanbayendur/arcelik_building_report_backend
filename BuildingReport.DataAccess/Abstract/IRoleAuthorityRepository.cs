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

        List<RoleAuthority> GetRoleAuthoritiesByRoleId(long roleid);

        RoleAuthority CreateRoleAuthority(RoleAuthority roleAuthority);

        RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority);

        void DeleteRoleAuthority(long id);

        bool CheckIfRoleAuthorityExistsByName(string roleName, string authorityName);

        bool CheckIfRoleAuthorityExistsById(long id);
        
        bool RoleAuthorityExistsById(long roleId, long authorityId);


    }
}
