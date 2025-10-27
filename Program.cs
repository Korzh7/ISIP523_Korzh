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


class EnemyFactory
{
    private Random random;

    public EnemyFactory(Random rand)
    {
        random = rand;
    }

    public Enemy CreateRandomEnemy()
    {
        int enemyType = random.Next(3);
        return enemyType switch
        {
            0 => CreateGoblin(),
            1 => CreateSkeleton(),
            2 => CreateMage(),
            _ => CreateGoblin()
        };
    }

    public Enemy CreateBoss(int turnCount)
    {
        int bossType = random.Next(4);
        return bossType switch
        {
            0 => CreateGoblin(true),
            1 => CreateSkeleton(true),
            2 => CreateMage(true),
            3 => CreateSpecialSkeleton(true),
            _ => CreateGoblin(true)
        };
    }

    private Enemy CreateGoblin(bool isBoss = false)
    {
        if (!isBoss)
        {
            return new Enemy("Гоблин", 30, 5, 8, new List<string> { "гоблин" }, critChance: 15);
        }
        else
        {
            return new Enemy("ВВГ (Босс Гоблин)", 60, 6, 12, new List<string> { "гоблин", "босс" }, critChance: 25);
        }
    }

    private Enemy CreateSkeleton(bool isBoss = false)
    {
        if (!isBoss)
        {
            return new Enemy("Скелет", 25, 3, 10, new List<string> { "скелет" }, ignoresArmor: true);
        }
        else
        {
            return new Enemy("Ковальский (Босс Скелет)", 63, 4, 13, new List<string> { "скелет", "босс" }, ignoresArmor: true);
        }
    }

    private Enemy CreateMage(bool isBoss = false)
    {
        if (!isBoss)
        {
            return new Enemy("Маг", 20, 2, 12, new List<string> { "маг" }, freezeChance: 20);
        }
        else
        {
            return new Enemy("Архимаг C++ (Босс Маг)", 36, 2, 19, new List<string> { "маг", "босс" }, freezeChance: 30);
        }
    }

    private Enemy CreateSpecialSkeleton(bool isBoss = false)
    {
        return new Enemy("Пестов С-- (Особый Скелет)", 33, 3, 18, new List<string> { "скелет", "босс" },
                        freezeChance: 35, ignoresArmor: true);
    }
}






class ChestSystem
{
    private Random random;

    public ChestSystem(Random rand)
    {
        random = rand;
    }

    public void OpenChest(Hero player)
    {
        int itemType = random.Next(3);

        switch (itemType)
        {
            case 0: 
                Console.WriteLine("Вы нашли зелье здоровья!");
                player.HP = 100;
                Console.WriteLine("HP полностью восстановлено!");
                break;

            case 1: 
                Weapon newWeapon = GenerateRandomWeapon();
                Console.WriteLine($"Вы нашли новое оружие:");
                Console.WriteLine($"   Урон: {newWeapon.Damage}, Прочность: {newWeapon.Durability}");
                ShowWeaponComparison(player, newWeapon);
                break;

            case 2: 
                Armor newArmor = GenerateRandomArmor();
                Console.WriteLine($"Вы нашли новые доспехи:");
                Console.WriteLine($"   Защита: {newArmor.ArmorDefense}, Прочность: {newArmor.Durability}");
                ShowArmorComparison(player, newArmor);
                break;
        }
    }

    private Weapon GenerateRandomWeapon()
    {
        int damage = random.Next(10, 21);
        int durability = random.Next(30, 61);
        return new Weapon(durability, damage);
    }

    private Armor GenerateRandomArmor()
    {
        int defense = random.Next(8, 16);
        int durability = random.Next(30, 61);
        return new Armor(durability, defense);
    }

    private void ShowWeaponComparison(Hero player, Weapon newWeapon)
    {
        Console.WriteLine($"Ваше текущее оружие - Урон: {player.Weapon_.Damage}, Прочность: {player.Weapon_.Durability}");
        Console.Write("Заменить оружие? (y/n): ");

        if (Console.ReadLine().ToLower() == "y")
        {
            player.Weapon_ = newWeapon;
            Console.WriteLine("Оружие заменено!");
        }
        else
        {
            Console.WriteLine("Оставили старое оружие.");
        }
    }

    private void ShowArmorComparison(Hero player, Armor newArmor)
    {
        Console.WriteLine($"Ваши текущие доспехи - Защита: {player.Armor_.ArmorDefense}, Прочность: {player.Armor_.Durability}");
        Console.Write("Заменить доспехи? (y/n): ");

        if (Console.ReadLine().ToLower() == "y")
        {
            player.Armor_ = newArmor;
            Console.WriteLine("Доспехи заменены!");
        }
        else
        {
            Console.WriteLine("Оставили старые доспехи.");
        }
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