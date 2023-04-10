using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class UpdateRoleAuthorityRequest
    {
        public long Id { get; set; }
        public long RoleId { get; set; }

        public long AuthorityId { get; set; }


    }
}
