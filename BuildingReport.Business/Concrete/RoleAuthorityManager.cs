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

        public void DeleteRoleAuthorityByAuthorityId(long id)
        {
            _roleAuthorityRepository.DeleteRoleAuthorityByAuthorityId(id);
        }

        public void DeleteRoleAuthorityByRoleId(long id)
        {
            _roleAuthorityRepository.DeleteRoleAuthorityByRoleId(id);
        }

        public List<RoleAuthority> GetAllRoleAuthorities()
        {
            return _roleAuthorityRepository.GetAllRoleAuthorities();
        }

        public RoleAuthority GetRoleAuthorityByAuthorityId(long id)
        {
            return _roleAuthorityRepository.GetRoleAuthorityByAuthorityId(id);
        }

        public RoleAuthority GetRoleAuthorityByRoleId(long id)
        {
            return _roleAuthorityRepository.GetRoleAuthorityByRoleId(id);
        }

        public RoleAuthority UpdateRoleAuthority(RoleAuthority roleAuthority)
        {
            return _roleAuthorityRepository.UpdateRoleAuthority(roleAuthority);
        }
    }
}
