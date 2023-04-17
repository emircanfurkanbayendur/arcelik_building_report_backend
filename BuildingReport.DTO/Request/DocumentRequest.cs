using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{

    public class DocumentRequest
    {
        [Required(ErrorMessage ="UploadedByUserId is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "UploadedByUserId must be greater than 0.")]
        public long UploadedByUserId { get; set; }

        [Required(ErrorMessage ="BuildingId is required.")]
        [Range(1, long.MaxValue, ErrorMessage = "BuildingId must be greater than 0.")]
        public long BuildingId { get; set; }

        [Required(ErrorMessage = "Report is required.")] 
        [MinLength(1, ErrorMessage = "Report must not be empty.")]
        public byte[] Report { get; set; }
    }
}
