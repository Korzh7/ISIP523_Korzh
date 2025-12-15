using System;
using ISIP523_Korzh.Model;

namespace ISIP523_Korzh.Model.Systems
{
    public class BattleSystem
    {
        private Random _random;
        private bool _playerFrozen = false;

        public BattleSystem(Random random)
        {
            _random = random;
        }

        public void StartBattle(Hero player, Enemy enemy)
        {
            Console.WriteLine($"Бой с {enemy.Name} (HP: {enemy.HP}, Урон: {enemy.Damage})");

            while (enemy.HP > 0 && player.HP > 0)
            {
                if (!_playerFrozen)
                {
                    PlayerTurn(player, enemy);
                }
                else
                {
                    Console.WriteLine("Вы заморожены и пропускаете ход!");
                    _playerFrozen = false;
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
                int baseDamage = player.Damage + player.Weapon.Damage;
                int finalDamage = enemy.CalculateIncomingDamage(baseDamage);

                enemy.HP -= finalDamage;
                player.Weapon.Durability -= 1;

                Console.WriteLine($"Вы атаковали и нанесли {finalDamage} урона! (исходный урон: {baseDamage})");
            }
            else if (input == "2")
            {
                if (_random.Next(100) < 40)
                {
                    Console.WriteLine("Вы успешно уклонились от атаки!");
                    return;
                }
                else
                {
                    int blockPercent = _random.Next(70, 101);
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

            if (enemy.Types.Contains("гоблин") && enemy.TryCriticalHit(_random))
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
                int damageReduction = player.Defense + (int)player.Armor.ArmorDefense;
                finalDamage = Math.Max(1, finalDamage - damageReduction);
            }

            if (enemy.Types.Contains("маг") && enemy.TryFreeze(_random))
            {
                _playerFrozen = true;
                Console.WriteLine($"{enemy.Name} замораживает вас! Вы пропустите следующий ход.");
            }

            player.HP -= finalDamage;
            player.Armor.Durability -= 1;

            Console.WriteLine($"{enemy.Name} атакует и наносит {finalDamage} урона!");
            Console.WriteLine($"Ваше HP: {player.HP}");

            CheckEquipmentDurability(player);
        }

        private void CheckEquipmentDurability(Hero player)
        {
            if (player.Weapon.Durability <= 0)
            {
                Console.WriteLine("Ваше оружие сломалось!");
                player.Weapon = new Equipment.Weapon(0, 0);
            }

            if (player.Armor.Durability <= 0)
            {
                Console.WriteLine("Ваши доспехи сломались!");
                player.Armor = new Equipment.Armor(0, 0);
            }
        }
    }
}