using Azure;
using Microsoft.AspNetCore.JsonPatch;
using System.ComponentModel.DataAnnotations;
namespace BuildingReport.DTO.Request
{
    public class PatchDocumentRequest : JsonPatchDocument<PatchDocumentRequest>
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public long Id { get; set; }

        [MinLength(1, ErrorMessage = "Report must not be empty.")]
        public byte[] Report { get; set; }

        public DateTime UploadedAt { get; set; }

        public bool IsActive { get; set; }

        [Range(1, long.MaxValue, ErrorMessage = "UploadedByUserId must be greater than 0.")]
        public long UploadedByUserId { get; set; }


        [Range(1, long.MaxValue, ErrorMessage = "BuildingId must be greater than 0.")]
        public long BuildingId { get; set; }
    }
}