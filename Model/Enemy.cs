using System;
using System.Collections.Generic;

namespace ISIP523_Korzh.Model
{
    public class Enemy : AbstractEntity
    {
        public string Name { get; set; }
        public List<string> Types { get; set; }
        public bool IsBoss { get; set; }
        public int CritChance { get; set; }
        public int FreezeChance { get; set; }
        public bool IgnoresArmor { get; set; }

        public Enemy(string name, int hp, int defense, int damage, List<string> types,
                    int critChance = 0, int freezeChance = 0, bool ignoresArmor = false)
        {
            Name = name;
            HP = hp;
            Defense = defense;
            Damage = damage;
            Types = types;
            CritChance = critChance;
            FreezeChance = freezeChance;
            IgnoresArmor = ignoresArmor;
        }

        public virtual bool TryCriticalHit(Random random)
        {
            return random.Next(100) < CritChance;
        }

        public virtual bool TryFreeze(Random random)
        {
            return random.Next(100) < FreezeChance;
        }

        public virtual int CalculateIncomingDamage(int damage)
        {
            return damage;
        }
    }
}