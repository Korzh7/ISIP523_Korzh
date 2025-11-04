using System;
using System.Collections.Generic;
using System.Linq;
using ISIP523_Korzh;

public static class PasswordHasher
{
    public static string HashPasswordBCrypt(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool VerifyPasswordBCrypt(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}

class Program
{
    static void Main(string[] args)
    {
        
    }

    public static void Registration()
    {
        using (var context = new Pr8GordovMainContext())
        {
            bool registrationSuccess = false;

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Введите никнейм: ");
                var nickName = Console.ReadLine();
                Console.WriteLine("Введите логин: ");
                var login = Console.ReadLine();
                Console.WriteLine("Введите пароль: ");
                var password = Console.ReadLine();
                Console.WriteLine("Введите пароль повторно: ");
                var password_ = Console.ReadLine();

                if (password == password_)
                {
                    
                    if (context.Users.Any(u => u.Login == login))
                    {
                        Console.WriteLine("Пользователь с таким логином уже существует!");
                        continue;
                    }

                    Console.WriteLine("Вы успешно зарегистрировались!");
                    var hashedPassword = PasswordHasher.HashPasswordBCrypt(password);
                    var user = new User
                    {
                        Name = nickName,
                        Login = login,
                        PasswordHash = hashedPassword
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    registrationSuccess = true;
                    break;
                }
                else
                {
                    Console.WriteLine("Пароли не совпадают, попробуйте еще раз.");
                    if (i == 4)
                    {
                        Console.WriteLine("Вы превысили лимит неудачных попыток. Попробуйте позже.");
                    }
                }
            }

            if (!registrationSuccess)
            {
                Console.WriteLine("Регистрация не завершена. Попробуйте позже.");
            }
        }
    }

    public static void ProductsView()
    {
        using (var context = new Pr8GordovMainContext())
        {
            Console.WriteLine("Как вы хотите просматривать каталог товаров?\n 1. Просмотр всех товаров\n 2. По категориям");
            var input = Console.ReadLine();

            if (char.TryParse(input, out char n))
            {
                switch (n)
                {
                    case '1':
                        ShowAllProducts(context);
                        break;

                    case '2':
                        ShowProductsByCategory(context);
                        break;

                    default:
                        Console.WriteLine("Неизвестная команда");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ошибка: введите один символ");
            }
        }
    }

    public static void ShowAllProducts(Pr8GordovMainContext context)
    {
        Random rand = new Random();
        var allProductIds = context.Products.Select(p => p.Id).ToList();
        var shuffledIds = allProductIds.OrderBy(x => rand.Next()).ToList();

        int page = 0;
        bool showMore = true;

        while (showMore)
        {
            var pageIds = shuffledIds.Skip(page * 10).Take(10).ToList();
            var products = context.Products
                .Where(p => pageIds.Contains(p.Id))
                .ToList();

            Console.WriteLine($"\n--- Страница {page + 1} ---");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Name} - {product.Price} руб. ({product.Category})");
            }

            if (pageIds.Count == 10)
            {
                Console.WriteLine("\n1 - Следующие 10 товаров");
                Console.WriteLine("0 - Выход");
                var choice = Console.ReadLine();
                showMore = (choice == "1");
                page++;
            }
            else
            {
                Console.WriteLine("\nВсе товары показаны!");
                showMore = false;
            }
        }
    }

    public static void ShowProductsByCategory(Pr8GordovMainContext context)
    {
        Console.WriteLine("Выберите категорию: \n 1. Электроника\n 2. Одежда\n 3. Книги\n 4. Спорт\n 5. Красота");
        var categoryInput = Console.ReadLine();

        var categories = new Dictionary<char, string>()
        {
            { '1', "Электроника" },
            { '2', "Одежда" },
            { '3', "Книги" },
            { '4', "Спорт" },
            { '5', "Красота" }
        };

        if (char.TryParse(categoryInput, out char choice) && categories.ContainsKey(choice))
        {
            var products = context.Products
                .Where(p => p.Category == categories[choice])
                .ToList();

            Console.WriteLine($"\n--- Товары в категории {categories[choice]} ---");

            if (products.Count > 0)
            {
                foreach (var product in products)
                {
                    Console.WriteLine($"Название: {product.Name}");
                    Console.WriteLine($"Описание: {product.Description}");
                    Console.WriteLine($"Цена: {product.Price} руб.");
                    Console.WriteLine("---");
                }
            }
            else
            {
                Console.WriteLine("В этой категории пока нет товаров");
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор категории");
        }
    }

    public static void Authorization(Pr8GordovMainContext context)
    {
        
    }


}