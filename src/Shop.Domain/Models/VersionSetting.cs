﻿using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class VersionSetting
{
    public string? Version { get; set; }

    public string? Description { get; set; }

    public string? Image { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndTime { get; set; }
}
