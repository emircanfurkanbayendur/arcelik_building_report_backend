using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class BuildingRequest
    {
        public string? Name { get; set; }
        public string City { get; set; } = null!;
        public string District { get; set; } = null!;
        public string Neighbourhood { get; set; } = null!;

        public string Street { get; set; } = null!;

        public int BuildingNumber { get; set; }

        public string Code { get; set; } = null!;

        public string Latitude { get; set; } = null!;

        public string Longitude { get; set; } = null!;

        public long CreatedByUserId { get; set; }
    }
}
