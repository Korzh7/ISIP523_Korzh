using System;
using System.Collections.Generic;

namespace ISIP523_Korzh;

public partial class Car
{
    public int Id { get; set; }

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public string Country { get; set; } = null!;

    public decimal? Price { get; set; }

    public decimal? EngineVolume { get; set; }

    public DateTime? CreatedAt { get; set; }
}
