using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Response
{
    public class DocumentResponse
    {
        public long Id { get; set; }

        public byte[] Report { get; set; } = null!;

        public DateTime UploadedAt { get; set; }

        public bool? IsActive { get; set; }

        public long UploadedByUserId { get; set; }

        public long BuildingId { get; set; }
    }
}
