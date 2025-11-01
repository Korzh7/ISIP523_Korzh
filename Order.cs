using System;
using System.Collections.Generic;

namespace ISIP523_Korzh;

public partial class Order
{
    public int OrderId { get; set; }

    public string CarModel { get; set; } = null!;

    public int BrokenPartId { get; set; }

    public int? UsedPartId { get; set; }

    public int ServiceId { get; set; }

    public string Status { get; set; } = null!;

    public decimal RepairCost { get; set; }

    public decimal FinalProfit { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual Service Service { get; set; } = null!;
}
