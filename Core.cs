using System;
using System.Collections.Generic;

namespace ISIP523_Korzh
{
    internal class Core
    {
        public static Pr7GordovKorzhContext Context = new Pr7GordovKorzhContext();
        public static Pr7CarsContext CarsContext = new Pr7CarsContext();
        public static int CarsProcessed = 0;
        public static List<PendingDelivery> PendingDeliveries = new List<PendingDelivery>();
    }

    public class PendingDelivery
    {
        public int SpareID { get; set; }
        public string SpareName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalCost { get; set; }
        public int OrderPlacedAtCar { get; set; }
    }

    public class TempClient
    {
        public string CarModel { get; set; }
        public int BrokenPartID { get; set; }
        public string BrokenPartName { get; set; }
        public decimal RepairCost { get; set; }
    }
}