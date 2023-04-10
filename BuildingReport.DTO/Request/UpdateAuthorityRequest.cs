using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class UpdateAuthorityRequest
    {
        public long Id { get; set; }

        public string Name { get; set; } = null!;

    }
}
