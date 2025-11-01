using System;
using System.Collections.Generic;

namespace ISIP523_Korzh;

public partial class Service
{
    public int ServiceId { get; set; }

    public decimal Balance { get; set; }

    public int TotalCarsProcessed { get; set; }

    public int SuccessfulRepairs { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Spare> Spares { get; set; } = new List<Spare>();
}
