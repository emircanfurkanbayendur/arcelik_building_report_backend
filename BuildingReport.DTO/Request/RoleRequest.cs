using System.ComponentModel.DataAnnotations;

namespace BuildingReport.DTO.Request
{
    public class RoleRequest
    {
        [Required]
        [MaxLength(50)]

        [RegularExpression("^[a-zA-Z0-9_ çÇğĞıİöÖşŞüÜ]*$", ErrorMessage = "Name can only contain letters, numbers, underscores, spaces, and dashes.")]
        public string Name { get; set; } = null!;
    }
}
