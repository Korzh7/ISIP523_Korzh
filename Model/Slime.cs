using System;
using System.Collections.Generic;

namespace ISIP523_Korzh.Model
{
    public class Slime : Enemy
    {
        private const int DamageReduction = 2;

        public Slime(string name, int hp, int defense, int damage, List<string> types,
                    int critChance = 0, int freezeChance = 0, bool ignoresArmor = false)
            : base(name, hp, defense, damage, types, critChance, freezeChance, ignoresArmor)
        {
        }

        public override int CalculateIncomingDamage(int damage)
        {
            int reducedDamage = damage - DamageReduction;
            return reducedDamage > 0 ? reducedDamage : 1;
        }
    }
}