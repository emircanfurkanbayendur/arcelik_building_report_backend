using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingReport.DTO.Request
{
    public class UpdateAuthorityRequest
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "Name can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string Name { get; set; } = null!;

    }
}
