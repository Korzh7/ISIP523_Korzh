using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;

namespace ISIP523_Korzh
{
    class Program
    {
        static void Main(string[] args)
        {
            InitializeGame();

            while (true)
            {
                Console.Clear();
                DisplayGameStatus();

                var clientInfo = GenerateRandomClient();
                DisplayClientRequest(clientInfo);

                DisplayActionMenu();
                var choice = GetUserChoice();

                switch (choice)
                {
                    case 1: AcceptOrder(clientInfo); break;
                    case 2: DeclineOrder(clientInfo); break;
                    case 3: ShowPurchaseMenu(); break;
                    case 4: ShowWarehouseStatus(); break;
                    case 5: ShowStatistics(); break;
                    case 6: return;
                    default: Console.WriteLine("Неверный выбор!"); break;
                }

                ProcessDeliveries();
                CheckGameOver();

                Console.WriteLine("\nНажмите любую клавишу...");
                Console.ReadKey();
            }
        }
        static void InitializeGame()
        {
            using (var context = new Pr7GordovKorzhContext())
            {
                if (!context.Services.Any())
                {
                    var service = new Service
                    {
                        ServiceId = 1,
                        Balance = 1000.00m,
                        TotalCarsProcessed = 0,
                        SuccessfulRepairs = 0,
                        LastUpdated = DateTime.Now
                    };
                    context.Services.Add(service);
                    context.SaveChanges();
                }

                if (!context.Spares.Any())
                {
                    var spares = new List<Spare>
                    {
                        new Spare { SpareName = "Тормозные колодки", PurchasePrice = 80.00m, Quantity = 3, MinimumStockLevel = 2, RepairMarkup = 1.5m, ServiceId = 1 },
                        new Spare { SpareName = "Масляный фильтр", PurchasePrice = 15.00m, Quantity = 5, MinimumStockLevel = 3, RepairMarkup = 1.8m, ServiceId = 1 },
                        new Spare { SpareName = "Воздушный фильтр", PurchasePrice = 25.00m, Quantity = 4, MinimumStockLevel = 2, RepairMarkup = 1.6m, ServiceId = 1 },
                        new Spare { SpareName = "Свечи зажигания", PurchasePrice = 45.00m, Quantity = 6, MinimumStockLevel = 3, RepairMarkup = 1.7m, ServiceId = 1 },
                        new Spare { SpareName = "Аккумулятор", PurchasePrice = 120.00m, Quantity = 2, MinimumStockLevel = 1, RepairMarkup = 1.4m, ServiceId = 1 }
                    };

                    context.Spares.AddRange(spares);
                    context.SaveChanges();
                }
            }

            Console.WriteLine("Игра 'Автосервис' запущена!");
        }