using System;
using ISIP523_Korzh.Model.Factories;
using ISIP523_Korzh.Model.Systems;
using ISIP523_Korzh.Model.Equipment;

namespace ISIP523_Korzh.Model
{
    public class Game
    {
        private Hero _player;
        private Random _random = new Random();
        private int _turnCount = 0;
        private bool _gameRunning = true;
        private IEnemyFactory _enemyFactory;
        private BattleSystem _battleSystem;
        private ChestSystem _chestSystem;
        private UIManager _uiManager;

        public Game()
        {
            Armor startingArmor = new Armor(50, 10);
            Weapon startingWeapon = new Weapon(50, 15);
            _player = new Hero(startingArmor, startingWeapon);

            _enemyFactory = new SimpleEnemyFactory(_random);
            _battleSystem = new BattleSystem(_random);
            _chestSystem = new ChestSystem(_random);
            _uiManager = new UIManager();
        }

        public void Start()
        {
            Console.WriteLine("=== ТЕКСТОВАЯ ПОШАГОВАЯ РОГАЛИК-ИГРА ===");
            Console.WriteLine("Нажмите любую клавишу для начала...");
            Console.ReadKey();

            while (_gameRunning && _player.HP > 0)
            {
                _turnCount++;
                Console.WriteLine($"\n--- Ход {_turnCount} ---");

                _uiManager.ShowPlayerStats(_player);

                if (_turnCount % 10 == 0)
                {
                    Enemy boss = _enemyFactory.CreateBoss(_turnCount);
                    Console.WriteLine($"\nПОЯВИЛСЯ БОСС: {boss.Name}!");
                    _battleSystem.StartBattle(_player, boss);
                }
                else
                {
                    if (_random.Next(2) == 0)
                    {
                        Enemy enemy = _enemyFactory.CreateRandomEnemy();
                        Console.WriteLine($"\nВСТРЕЧА С ВРАГОМ: {enemy.Name}");
                        _battleSystem.StartBattle(_player, enemy);
                    }
                    else
                    {
                        Console.WriteLine($"\nВЫ НАШЛИ СУНДУК!");
                        _chestSystem.OpenChest(_player);
                    }
                }

                if (_player.HP <= 0)
                {
                    Console.WriteLine("\nВЫ ПРОИГРАЛИ! Игра окончена.");
                    _gameRunning = false;
                }
                else
                {
                    Console.WriteLine("\nНажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                }
            }

            Console.WriteLine($"\nИгра завершена. Пройдено ходов: {_turnCount}");
        }
    }
}