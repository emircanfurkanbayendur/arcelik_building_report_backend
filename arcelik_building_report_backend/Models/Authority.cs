using System;
using System.Collections.Generic;

namespace arcelik_building_report_backend.Models;

public partial class Authority
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;
}
