using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO
{
    public class ReturnDto
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public byte[] Password { get; set; } = null!;

        public string Token { get; set; } = null!;
        public long RoleId { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool? IsActive { get; set; }
    }
}
