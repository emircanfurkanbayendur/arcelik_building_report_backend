using BuildingReport.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingReport.DTO.Request
{
    public class UpdateDocumentRequest
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "Report is required.")]
        [MinLength(1, ErrorMessage = "Report must not be empty.")]
        public byte[] Report { get; set; }

        [Required]
        public DateTime UploadedAt { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "UploadedByUserId must be greater than 0.")]
        public long UploadedByUserId { get; set; }

        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "BuildingId must be greater than 0.")]
        public long BuildingId { get; set; }
    }
}
