using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class UpdateRoleAuthorityRequest
    {
        [Required(ErrorMessage = "Id is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public long Id { get; set; }


        [Required(ErrorMessage = "RoleId is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "RoleId must be greater than 0.")]
        public long RoleId { get; set; }

        [Required(ErrorMessage = "AuthorityId is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "AuthorityId must be greater than 0.")]
        public long AuthorityId { get; set; }


    }
}
