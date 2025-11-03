using System;
using System.Collections.Generic;

namespace ISIP523_Korzh;

public partial class PickupPoint
{
    public int Id { get; set; }

    public string? Address { get; set; }

    public string? WorkingHours { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
