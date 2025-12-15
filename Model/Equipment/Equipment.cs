namespace ISIP523_Korzh.Model.Equipment
{
    public abstract class Equipment
    {
        public int Durability { get; set; }

        protected Equipment(int durability)
        {
            Durability = durability;
        }
    }
}