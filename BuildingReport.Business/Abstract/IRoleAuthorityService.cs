using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.Business.Abstract
{
    public interface IRoleAuthorityService
    {
        List<RoleAuthority> GetAllRoleAuthorities();

        RoleAuthority GetRoleAuthorityByRoleId(long id);

        RoleAuthority GetRoleAuthorityByAuthorityId(long id);

        RoleAuthority CreateRoleAuthority(RoleAuthority roleAuthority);

        RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority);

        void DeleteRoleAuthorityByRoleId(long id);

        void DeleteRoleAuthorityByAuthorityId(long id);
    }
}
