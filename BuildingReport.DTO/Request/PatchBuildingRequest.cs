using BuildingReport.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class PatchBuildingRequest
    {

        [Range(1, long.MaxValue, ErrorMessage = "Id must be greater than 0.")]
        public long Id { get; set; }


        [Range(1, long.MaxValue, ErrorMessage = "CreatedByUserId must be a positive number.")]
        public long CreatedByUserId { get; set; }


        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "Name can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string? Name { get; set; }

        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "City can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string City { get; set; } = null!;

        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "District can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string District { get; set; } = null!;

        [MaxLength(70)]
        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "Neighbourhood can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string Neighbourhood { get; set; } = null!;


        [MaxLength(70)]
        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "Street can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string Street { get; set; } = null!;


        public int BuildingNumber { get; set; }

        [MaxLength(50)]
        [Range(1, long.MaxValue, ErrorMessage = "Code must be a positive number.")]
        public string Code { get; set; } = null!;


        public string Latitude { get; set; } = null!;

        public string Longitude { get; set; } = null!;

        public DateTime RegisteredAt { get; set; }

        public bool? IsActive { get; set; }

    }
}
