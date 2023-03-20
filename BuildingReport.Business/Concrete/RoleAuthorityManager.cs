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
    public class RoleAuthorityManager : IRoleAuthorityService
    {
        private IRoleAuthorityRepository _roleAuthorityRepository;

        public RoleAuthorityManager()
        {
            _roleAuthorityRepository = new RoleAuthorityRepository();
        }

        public RoleAuthority CreateRoleAuthority(RoleAuthority roleAuthority)
        {
            return _roleAuthorityRepository.CreateRoleAuthority(roleAuthority);
        }

        public void DeleteRoleAuthority(long id)
        {
            _roleAuthorityRepository.DeleteRoleAuthority(id);
        }


        public List<RoleAuthority> GetAllRoleAuthorities()
        {
            return _roleAuthorityRepository.GetAllRoleAuthorities();
        }

        public RoleAuthority GetRoleAuthorityById(long id)
        {
            return _roleAuthorityRepository.GetRoleAuthorityById(id);
        }


        public RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority)
        {
            return _roleAuthorityRepository.UpdateRoleAuthority(roleAuthority);
        }
        public bool RoleAuthorityExists(string roleName, string authorityName)
        {
            return _roleAuthorityRepository.RoleAuthorityExists(roleName, authorityName);
        }
    }
}
