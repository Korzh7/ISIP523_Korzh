using System;
using ISIP523_Korzh.Model;
using ISIP523_Korzh.Model.Equipment;

namespace ISIP523_Korzh.Model.Systems
{
    public class ChestSystem
    {
        private Random _random;

        public ChestSystem(Random random)
        {
            _random = random;
        }

        public void OpenChest(Hero player)
        {
            int itemType = _random.Next(3);

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
            int damage = _random.Next(10, 21);
            int durability = _random.Next(30, 61);
            return new Weapon(durability, damage);
        }

        private Armor GenerateRandomArmor()
        {
            int defense = _random.Next(8, 16);
            int durability = _random.Next(30, 61);
            return new Armor(durability, defense);
        }

        private void ShowWeaponComparison(Hero player, Weapon newWeapon)
        {
            Console.WriteLine($"Ваше текущее оружие - Урон: {player.Weapon.Damage}, Прочность: {player.Weapon.Durability}");
            Console.Write("Заменить оружие? (y/n): ");

            if (Console.ReadLine().ToLower() == "y")
            {
                player.Weapon = newWeapon;
                Console.WriteLine("Оружие заменено!");
            }
            else
            {
                Console.WriteLine("Оставили старое оружие.");
            }
        }

        private void ShowArmorComparison(Hero player, Armor newArmor)
        {
            Console.WriteLine($"Ваши текущие доспехи - Защита: {player.Armor.ArmorDefense}, Прочность: {player.Armor.Durability}");
            Console.Write("Заменить доспехи? (y/n): ");

            if (Console.ReadLine().ToLower() == "y")
            {
                player.Armor = newArmor;
                Console.WriteLine("Доспехи заменены!");
            }
            else
            {
                Console.WriteLine("Оставили старые доспехи.");
            }
        }
    }
}