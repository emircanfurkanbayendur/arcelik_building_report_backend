﻿using System;
using System.Collections.Generic;

namespace arcelik_building_report_backend.Models;

public partial class Document
{
    public long Id { get; set; }

    public byte[] Report { get; set; } = null!;

    public DateTime UploadedAt { get; set; }

    public bool? IsActive { get; set; }

    public User UploadedByUser { get; set; }

    public long BuildingId { get; set; }

    public virtual Building Building { get; set; } = null!;
}
