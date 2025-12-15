namespace ISIP523_Korzh.Model.Equipment
{
    public class Weapon : Equipment
    {
        public int Damage { get; set; }

        public Weapon(int durability, int damage) : base(durability)
        {
            Damage = damage;
        }
    }
}