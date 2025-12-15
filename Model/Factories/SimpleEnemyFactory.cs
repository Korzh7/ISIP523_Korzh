using System;
using System.Collections.Generic;
using ISIP523_Korzh.Model;

namespace ISIP523_Korzh.Model.Factories
{
    public class SimpleEnemyFactory : IEnemyFactory
    {
        private Random _random;

        public SimpleEnemyFactory(Random random)
        {
            _random = random;
        }

        public Enemy CreateRandomEnemy()
        {
            int enemyType = _random.Next(4);
            return enemyType switch
            {
                0 => CreateGoblin(),
                1 => CreateSkeleton(),
                2 => CreateMage(),
                3 => CreateSlime(),
                _ => CreateGoblin()
            };
        }

        public Enemy CreateBoss(int turnCount)
        {
            int bossType = _random.Next(5);
            return bossType switch
            {
                0 => CreateGoblin(true),
                1 => CreateSkeleton(true),
                2 => CreateMage(true),
                3 => CreateSpecialSkeleton(true),
                4 => CreateSlime(true),
                _ => CreateGoblin(true)
            };
        }

        private Enemy CreateGoblin(bool isBoss = false)
        {
            if (!isBoss)
            {
                return new Enemy("Гоблин", 30, 5, 8,
                    new List<string> { "гоблин" }, critChance: 15);
            }
            else
            {
                return new Enemy("ВВГ (Босс Гоблин)", 60, 6, 12,
                    new List<string> { "гоблин", "босс" }, critChance: 25);
            }
        }

        private Enemy CreateSkeleton(bool isBoss = false)
        {
            if (!isBoss)
            {
                return new Enemy("Скелет", 25, 3, 10,
                    new List<string> { "скелет" }, ignoresArmor: true);
            }
            else
            {
                return new Enemy("Ковальский (Босс Скелет)", 63, 4, 13,
                    new List<string> { "скелет", "босс" }, ignoresArmor: true);
            }
        }

        private Enemy CreateMage(bool isBoss = false)
        {
            if (!isBoss)
            {
                return new Enemy("Маг", 20, 2, 12,
                    new List<string> { "маг" }, freezeChance: 20);
            }
            else
            {
                return new Enemy("Архимаг C++ (Босс Маг)", 36, 2, 19,
                    new List<string> { "маг", "босс" }, freezeChance: 30);
            }
        }

        private Enemy CreateSlime(bool isBoss = false)
        {
            if (!isBoss)
            {
                return new Slime("Слизень", 35, 2, 6,
                    new List<string> { "слизень" });
            }
            else
            {
                return new Slime("Король Слизней (Босс Слизень)", 70, 3, 10,
                    new List<string> { "слизень", "босс" });
            }
        }

        private Enemy CreateSpecialSkeleton(bool isBoss = false)
        {
            return new Enemy("Пестов С-- (Особый Скелет)", 33, 3, 18,
                new List<string> { "скелет", "босс" }, freezeChance: 35, ignoresArmor: true);
        }
    }
}