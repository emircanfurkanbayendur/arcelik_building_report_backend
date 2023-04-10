using BuildingReport.DTO;
using BuildingReport.DTO.Request;
using BuildingReport.DTO.Response;
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
        List<RoleAuthorityResponse> GetAllRoleAuthorities();

        RoleAuthorityResponse GetRoleAuthorityById(long id);

        RoleAuthorityResponse CreateRoleAuthority(RoleAuthorityRequest request);

        RoleAuthorityResponse UpdateRoleAuthority(UpdateRoleAuthorityRequest request);

        bool DeleteRoleAuthority(long id);
        void RoleAuthorityExists(string roleName, string authorityName);
        bool RoleAuthorityExistsById(long roleId, long authorityId);

    }
}
