using BuildingReport.DTO;
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

        RoleAuthority GetRoleAuthorityById(long id);

        RoleAuthority CreateRoleAuthority(RoleAuthorityDTO roleAuthorityDTO);

        RoleAuthority UpdateRoleAuthority(RoleAuthorityDTO roleAuthorityDTO);

        void DeleteRoleAuthority(long id);
        void RoleAuthorityExists(string roleName, string authorityName);

    }
}
