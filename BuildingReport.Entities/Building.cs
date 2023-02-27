﻿using System;
using System.Collections.Generic;

namespace BuildingReport.Entities;

public partial class Building
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string Adress { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string Latitude { get; set; } = null!;

    public string Longitude { get; set; } = null!;

    public DateTime RegisteredAt { get; set; }

    public bool? IsActive { get; set; }

    public long CreatedByUserId { get; set; }
    public User CreatedByUser { get; set; }

    public virtual ICollection<Document> Documents { get; } = new List<Document>();
}
