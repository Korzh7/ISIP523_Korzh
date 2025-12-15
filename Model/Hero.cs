using ISIP523_Korzh.Model.Equipment;

namespace ISIP523_Korzh.Model
{
    public class Hero : AbstractEntity
    {
        public Armor Armor { get; set; }
        public Weapon Weapon { get; set; }

        public Hero(Armor armor, Weapon weapon)
        {
            Armor = armor;
            Weapon = weapon;
            HP = 100;
            Defense = 5;
            Damage = 10;
        }
    }
}