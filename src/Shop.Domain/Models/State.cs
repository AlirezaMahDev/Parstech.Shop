using System;
using System.Collections.Generic;

namespace Shop.Domain.Models;

public partial class State
{
    public int Id { get; set; }

    public string StateTitle { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public virtual ICollection<Representation> Representations { get; set; } = new List<Representation>();
}
