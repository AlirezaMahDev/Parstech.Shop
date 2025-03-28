﻿namespace Parstech.Shop.Context.Application.DTOs.State;

public class SteteDto
{
    public int Id { get; set; }

    public string StateTitle { get; set; } = null!;

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }
}