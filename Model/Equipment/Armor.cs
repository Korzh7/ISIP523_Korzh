namespace ISIP523_Korzh.Model.Equipment
{
    public class Armor : Equipment
    {
        public decimal ArmorDefense { get; set; }

        public Armor(int durability, decimal armorDefense) : base(durability)
        {
            ArmorDefense = armorDefense;
        }
    }
}