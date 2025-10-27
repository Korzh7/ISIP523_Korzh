using System;
using System.Collections.Generic;
using System.Linq;



class Enemy : Abstract
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

    public bool TryCriticalHit(Random random)
    {
        return random.Next(100) < CritChance;
    }

    public bool TryFreeze(Random random)
    {
        return random.Next(100) < FreezeChance;
    }
}



class UIManager
{
    public void ShowPlayerStats(Hero player)
    {
        Console.WriteLine($"\n=== ИГРОК ===");
        Console.WriteLine($"HP: {player.HP}");
        Console.WriteLine($"Атака: {player.Damage} + {player.Weapon_.Damage} (оружие)");
        Console.WriteLine($"Защита: {player.Defense} + {player.Armor_.ArmorDefense} (доспехи)");
        Console.WriteLine($"Прочность оружия: {player.Weapon_.Durability}");
        Console.WriteLine($"Прочность доспехов: {player.Armor_.Durability}");
    }
}


class Armor
{
    public int Durability { get; set; }
    public decimal ArmorDefense { get; set; }

    public Armor(int durability, decimal armorDefense)
    {
        Durability = durability;
        ArmorDefense = armorDefense;
    }
}

class Weapon
{
    public int Durability { get; set; }
    public int Damage { get; set; }

    public Weapon(int durability, int damage)
    {
        Durability = durability;
        Damage = damage;
    }
}

class Abstract
{
    public int HP { get; set; }
    public int Defense { get; set; }
    public int Damage { get; set; }
}

class Hero : Abstract
{
    public Armor Armor_ { get; set; }
    public Weapon Weapon_ { get; set; }

    public Hero(Armor armor, Weapon weapon)
    {
        Armor_ = armor;
        Weapon_ = weapon;
    }
}