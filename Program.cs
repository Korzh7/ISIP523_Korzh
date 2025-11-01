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
        static void DisplayGameStatus()
        {
            using (var context = new Pr7GordovKorzhContext())
            {
                var service = context.Services.First();
                Console.WriteLine("=== АВТОСЕРВИС ===");
                Console.WriteLine($"Баланс: {service.Balance} руб.");
                Console.WriteLine($"Обработано машин: {service.TotalCarsProcessed}");
                Console.WriteLine($"Успешных ремонтов: {service.SuccessfulRepairs}");
                Console.WriteLine($"Ожидающих поставок: {Core.PendingDeliveries.Count}");
                Console.WriteLine("===================");
            }
        }

        static void DisplayActionMenu()
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1 - Принять заказ");
            Console.WriteLine("2 - Отказаться от заказ");
            Console.WriteLine("3 - Купить запчасти");
            Console.WriteLine("4 - Показать склад");
            Console.WriteLine("5 - Статистика");
            Console.WriteLine("6 - Выйти из игры");
        }

        static int GetUserChoice()
        {
            Console.Write("Ваш выбор: ");
            return int.TryParse(Console.ReadLine(), out int choice) ? choice : 0;
        }
        static TempClient GenerateRandomClient()
        {
            var random = new Random();

            using (var carsContext = new Pr7CarsContext())
            {
                using (var mainContext = new Pr7GordovKorzhContext())
                {
                    var carsCount = carsContext.Cars.Count();
                    var skipCount = random.Next(carsCount);
                    var randomCar = carsContext.Cars
                        .OrderBy(c => c.Id)
                        .Skip(skipCount)
                        .First();

                    var spares = mainContext.Spares.ToList();
                    var randomSpare = spares[random.Next(spares.Count)];
                    var repairCost = randomSpare.PurchasePrice * randomSpare.RepairMarkup;

                    return new TempClient
                    {
                        CarModel = $"{randomCar.Brand} {randomCar.Model} ({randomCar.Year})",
                        BrokenPartID = randomSpare.SpareId,
                        BrokenPartName = randomSpare.SpareName,
                        RepairCost = repairCost
                    };
                }
            }
        }

        static void DisplayClientRequest(TempClient client)
        {
            using (var context = new Pr7GordovKorzhContext())
            {
                var spare = context.Spares.First(s => s.SpareId == client.BrokenPartID);
                Console.WriteLine($"\nПриехал клиент на {client.CarModel}");
                Console.WriteLine($"Поломка: {client.BrokenPartName}");
                Console.WriteLine($"Стоимость ремонта: {client.RepairCost} руб.");
                Console.WriteLine($"На складе: {spare.Quantity} шт.");
            }
        }
        static void AcceptOrder(TempClient client)
        {
            using (var context = new Pr7GordovKorzhContext())
            {
                var service = context.Services.First();
                var brokenSpare = context.Spares.First(s => s.SpareId == client.BrokenPartID);

                service.TotalCarsProcessed++;
                Core.CarsProcessed++;

                if (brokenSpare.Quantity > 0)
                {
                    brokenSpare.Quantity--;
                    service.Balance += client.RepairCost;
                    service.SuccessfulRepairs++;

                    var order = new Order
                    {
                        CarModel = client.CarModel,
                        BrokenPartId = client.BrokenPartID,
                        UsedPartId = client.BrokenPartID,
                        ServiceId = 1,
                        Status = "Completed",
                        RepairCost = client.RepairCost,
                        FinalProfit = client.RepairCost - brokenSpare.PurchasePrice,
                        OrderDate = DateTime.Now
                    };
                    context.Orders.Add(order);

                    Console.WriteLine($"Ремонт выполнен успешно! Получено: {client.RepairCost} руб.");
                }
                else
                {
                    Console.WriteLine("Нужной детали нет на складе! Производим замену случайной деталью...");

                    var randomSpare = GetRandomAvailableSpare(context);
                    if (randomSpare != null)
                    {
                        randomSpare.Quantity--;

                        var penalty = 150.00m;
                        service.Balance -= penalty;

                        var order = new Order
                        {
                            CarModel = client.CarModel,
                            BrokenPartId = client.BrokenPartID,
                            UsedPartId = randomSpare.SpareId,
                            ServiceId = 1,
                            Status = "Failed",
                            RepairCost = 0,
                            FinalProfit = -penalty,
                            OrderDate = DateTime.Now
                        };
                        context.Orders.Add(order);

                        Console.WriteLine($"Клиент недоволен! Штраф: {penalty} руб.");
                        Console.WriteLine($"Использована случайная деталь: {randomSpare.SpareName}");
                    }
                    else
                    {
                        Console.WriteLine("На складе нет вообще никаких деталей! Штраф удвоен.");
                        service.Balance -= 300.00m;
                    }
                }

                service.LastUpdated = DateTime.Now;
                context.SaveChanges();
            }
        }

        static Spare GetRandomAvailableSpare(Pr7GordovKorzhContext context)
        {
            var availableSpares = context.Spares.Where(s => s.Quantity > 0).ToList();
            return availableSpares.Any() ? availableSpares[new Random().Next(availableSpares.Count)] : null;
        }
        static void DeclineOrder(TempClient client)
        {
            using (var context = new Pr7GordovKorzhContext())
            {
                var service = context.Services.First();
                var penalty = 50.00m;

                service.Balance -= penalty;
                service.TotalCarsProcessed++;
                service.LastUpdated = DateTime.Now;
                Core.CarsProcessed++;

                var order = new Order
                {
                    CarModel = client.CarModel,
                    BrokenPartId = client.BrokenPartID,
                    UsedPartId = null,
                    ServiceId = 1,
                    Status = "Declined",
                    RepairCost = 0,
                    FinalProfit = -penalty,
                    OrderDate = DateTime.Now
                };
                context.Orders.Add(order);
                context.SaveChanges();

                Console.WriteLine($"Заказ отклонен. Штраф: {penalty} руб.");
            }
        }
        static void ShowPurchaseMenu()
        {
            Console.Clear();
            Console.WriteLine("=== ПОКУПКА ЗАПЧАСТЕЙ ===");

            using (var context = new Pr7GordovKorzhContext())
            {
                var spares = context.Spares.ToList();
                var service = context.Services.First();

                for (int i = 0; i < spares.Count; i++)
                {
                    var spare = spares[i];
                    Console.WriteLine($"{i + 1}. {spare.SpareName} - {spare.PurchasePrice} руб. (на складе: {spare.Quantity})");
                }

                Console.Write("\nВыберите номер запчасти: ");
                if (int.TryParse(Console.ReadLine(), out int spareIndex) && spareIndex >= 1 && spareIndex <= spares.Count)
                {
                    var selectedSpare = spares[spareIndex - 1];

                    Console.Write("Введите количество: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        var totalCost = selectedSpare.PurchasePrice * quantity;

                        if (service.Balance >= totalCost)
                        {
                            service.Balance -= totalCost;
                            service.LastUpdated = DateTime.Now;

                            var delivery = new PendingDelivery
                            {
                                SpareID = selectedSpare.SpareId,
                                SpareName = selectedSpare.SpareName,
                                Quantity = quantity,
                                TotalCost = totalCost,
                                OrderPlacedAtCar = Core.CarsProcessed
                            };

                            Core.PendingDeliveries.Add(delivery);
                            context.SaveChanges();

                            Console.WriteLine($"Заказ оформлен! Поставка через 2 машины. Списано: {totalCost} руб.");
                        }
                        else
                        {
                            Console.WriteLine("Недостаточно средств!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Неверное количество!");
                    }
                }
                else
                {
                    Console.WriteLine("Неверный выбор!");
                }
            }
        }