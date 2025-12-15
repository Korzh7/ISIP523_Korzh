using System;
using ISIP523_Korzh.Model;

namespace ISIP523_Korzh.Model.Systems
{
    public class UIManager
    {
        public void ShowPlayerStats(Hero player)
        {
            Console.WriteLine($"\n=== ИГРОК ===");
            Console.WriteLine($"HP: {player.HP}");
            Console.WriteLine($"Атака: {player.Damage} + {player.Weapon.Damage} (оружие)");
            Console.WriteLine($"Защита: {player.Defense} + {player.Armor.ArmorDefense} (доспехи)");
            Console.WriteLine($"Прочность оружия: {player.Weapon.Durability}");
            Console.WriteLine($"Прочность доспехов: {player.Armor.Durability}");
        }
    }
}