using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildingReport.Entities;
public partial class RoleAuthority
{
    [Key]
    public long Id { get; set; }
    public long RoleId { get; set; }

    public long AuthorityId { get; set; }

    public virtual Authority Authority { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
