using System;
using System.Collections.Generic;

namespace BuildingReport.Entities;

public partial class User
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool? IsActive { get; set; }

    public long RoleId { get; set; }

    public virtual ICollection<Building> Buildings { get; } = new List<Building>();

    public virtual ICollection<Document> Documents { get; } = new List<Document>();

    public virtual Role Role { get; set; } = null!;
}
