using System;
using System.Collections.Generic;

namespace ISIP523_Korzh;

public partial class Spare
{
    public int SpareId { get; set; }

    public string SpareName { get; set; } = null!;

    public decimal PurchasePrice { get; set; }

    public int Quantity { get; set; }

    public int MinimumStockLevel { get; set; }

    public decimal RepairMarkup { get; set; }

    public int ServiceId { get; set; }

    public virtual Service Service { get; set; } = null!;
}
