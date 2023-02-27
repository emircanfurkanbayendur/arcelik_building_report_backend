using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO
{
    public class BuildingDTO
    {
        public long Id { get; set; }

        public string? Name { get; set; }

        public string Adress { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string Latitude { get; set; } = null!;

        public string Longitude { get; set; } = null!;

        public DateTime RegisteredAt { get; set; }

        public bool? IsActive { get; set; }

        public long CreatedByUserId { get; set; }
    }
}
