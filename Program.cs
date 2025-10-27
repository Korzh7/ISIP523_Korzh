using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}

class Game
{
    private Hero player;
    private Random random = new Random();
    private int turnCount = 0;
    private bool gameRunning = true;
    private EnemyFactory enemyFactory;
    private BattleSystem battleSystem;
    private ChestSystem chestSystem;
    private UIManager uiManager;

    public Game()
    {
        
        Armor startingArmor = new Armor(50, 10);
        Weapon startingWeapon = new Weapon(50, 15);
        player = new Hero(startingArmor, startingWeapon);
        player.HP = 100;
        player.Defense = 5;
        player.Damage = 10;

        enemyFactory = new EnemyFactory(random);
        battleSystem = new BattleSystem(random);
        chestSystem = new ChestSystem(random);
        uiManager = new UIManager();
    }

    public void Start()
    {
        Console.WriteLine("=== ТЕКСТОВАЯ ПОШАГОВАЯ РОГАЛИК-ИГРА ===");
        Console.WriteLine("Нажмите любую клавишу для начала...");
        Console.ReadKey();

        while (gameRunning && player.HP > 0)
        {
            turnCount++;
            Console.WriteLine($"\n--- Ход {turnCount} ---");

            uiManager.ShowPlayerStats(player);

           
            if (turnCount % 10 == 0)
            {
                Enemy boss = enemyFactory.CreateBoss(turnCount);
                Console.WriteLine($"\nПОЯВИЛСЯ БОСС: {boss.Name}!");
                battleSystem.StartBattle(player, boss);
            }
            else
            {
                
                if (random.Next(2) == 0)
                {
                    Enemy enemy = enemyFactory.CreateRandomEnemy();
                    Console.WriteLine($"\nВСТРЕЧА С ВРАГОМ: {enemy.Name}");
                    battleSystem.StartBattle(player, enemy);
                }
                else
                {
                    Console.WriteLine($"\nВЫ НАШЛИ СУНДУК!");
                    chestSystem.OpenChest(player);
                }
            }

            if (player.HP <= 0)
            {
                Console.WriteLine("\nВЫ ПРОИГРАЛИ! Игра окончена.");
                gameRunning = false;
            }
            else
            {
                Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                Console.ReadKey();
            }
        }

        Console.WriteLine($"\nИгра завершена. Пройдено ходов: {turnCount}");
    }
}

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


class BattleSystem
{
    private Random random;
    private bool playerFrozen = false;

    public BattleSystem(Random rand)
    {
        random = rand;
    }

    public void StartBattle(Hero player, Enemy enemy)
    {
        Console.WriteLine($"Бой с {enemy.Name} (HP: {enemy.HP}, Урон: {enemy.Damage})");

        while (enemy.HP > 0 && player.HP > 0)
        {
            
            if (!playerFrozen)
            {
                PlayerTurn(player, enemy);
            }
            else
            {
                Console.WriteLine("Вы заморожены и пропускаете ход!");
                playerFrozen = false;
            }

            if (enemy.HP <= 0) break;

         
            EnemyTurn(player, enemy);
        }

        if (enemy.HP <= 0)
        {
            Console.WriteLine($"Вы победили {enemy.Name}!");
        }
    }

    private void PlayerTurn(Hero player, Enemy enemy)
    {
        Console.WriteLine("\nВаш ход:");
        Console.WriteLine("1 - Атаковать");
        Console.WriteLine("2 - Защищаться");
        Console.Write("Выберите действие: ");

        string input = Console.ReadLine();

        if (input == "1")
        {
            int damage = player.Damage + player.Weapon_.Damage;
            enemy.HP -= damage;
            player.Weapon_.Durability -= 1;
            Console.WriteLine($"Вы атаковали и нанесли {damage} урона!");
        }
        else if (input == "2")
        {
           
            if (random.Next(100) < 40)
            {
                Console.WriteLine("Вы успешно уклонились от атаки!");
                return;
            }
            else
            {
             
                int blockPercent = random.Next(70, 101);
                int damageReduction = (int)(player.Defense * blockPercent / 100.0);
                Console.WriteLine($"Вы блокируете {blockPercent}% защиты ({damageReduction} урона)");
                
            }
        }
    }

    private void EnemyTurn(Hero player, Enemy enemy)
    {
        Console.WriteLine($"\nХод {enemy.Name}:");

        int baseDamage = enemy.Damage;
        int finalDamage = baseDamage;


        if (enemy.Types.Contains("гоблин") && enemy.TryCriticalHit(random))
        {
            finalDamage = (int)(baseDamage * 1.5);
            Console.WriteLine($"Критический урон! Урон увеличен до {finalDamage}!");
        }

        if (enemy.Types.Contains("скелет") && enemy.IgnoresArmor)
        {
            Console.WriteLine($"{enemy.Name} игнорирует вашу защиту!");
           
        }
        else
        {
           
            int damageReduction = player.Defense + (int)player.Armor_.ArmorDefense;
            finalDamage = Math.Max(1, finalDamage - damageReduction);
        }

        if (enemy.Types.Contains("маг") && enemy.TryFreeze(random))
        {
            playerFrozen = true;
            Console.WriteLine($"{enemy.Name} замораживает вас! Вы пропустите следующий ход.");
        }

        player.HP -= finalDamage;
        player.Armor_.Durability -= 1;

        Console.WriteLine($"{enemy.Name} атакует и наносит {finalDamage} урона!");
        Console.WriteLine($"Ваше HP: {player.HP}");

       
        CheckEquipmentDurability(player);
    }

    private void CheckEquipmentDurability(Hero player)
    {
        if (player.Weapon_.Durability <= 0)
        {
            Console.WriteLine("Ваше оружие сломалось!");
            player.Weapon_ = new Weapon(0, 0); 
        }

        if (player.Armor_.Durability <= 0)
        {
            Console.WriteLine("Ваши доспехи сломались!");
            player.Armor_ = new Armor(0, 0); 
        }
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