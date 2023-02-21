﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace arcelik_building_report_backend.Models;
[Keyless]
public partial class RoleAuthority
{
    public long RoleId { get; set; }

    public long AuthorityId { get; set; }

    public virtual Authority Authority { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
