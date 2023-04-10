using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class DocumentRequest
    {
        public long UploadedByUserId { get; set; }
        public long BuildingId { get; set; }
        public byte[] Report { get; set; }
    }
}
