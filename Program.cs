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
        using (var context = new Pr8GordovMainContext())
        {
            while (true)
            {
                Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
                Console.WriteLine("1 - Регистрация");
                Console.WriteLine("2 - Авторизация");
                Console.WriteLine("3 - Просмотр товаров");
                Console.WriteLine("4 - Выход");
                Console.Write("Выберите действие: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Registration(context);
                        break;
                    case "2":
                        User currentUser = Authorization(context);
                        if (currentUser != null)
                        {
                            UserMenu(context, currentUser);
                        }
                        break;
                    case "3":
                        ProductsView(context);
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор!");
                        break;
                }
            }
        }
    }

    public static void Registration(Pr8GordovMainContext context)
    {
        bool registrationSuccess = false;

        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine("\n=== РЕГИСТРАЦИЯ ===");
            Console.WriteLine("Для выхода введите 'exit' в любое время");
            Console.Write("Введите никнейм: ");
            var nickName = Console.ReadLine();
            if (nickName?.ToLower() == "exit")
            {
                Console.WriteLine("Регистрация отменена.");
                return;
            }

            Console.Write("Введите логин: ");
            var login = Console.ReadLine();
            if (login?.ToLower() == "exit")
            {
                Console.WriteLine("Регистрация отменена.");
                return;
            }

            Console.Write("Введите пароль: ");
            var password = Console.ReadLine();
            if (password?.ToLower() == "exit")
            {
                Console.WriteLine("Регистрация отменена.");
                return;
            }

            Console.Write("Введите пароль повторно: ");
            var password_ = Console.ReadLine();
            if (password_?.ToLower() == "exit")
            {
                Console.WriteLine("Регистрация отменена.");
                return;
            }

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

    public static User Authorization(Pr8GordovMainContext context)
    {
        Console.WriteLine("\n=== АВТОРИЗАЦИЯ ===");
        Console.Write("Введите логин: ");
        string login = Console.ReadLine();
        Console.Write("Введите пароль: ");
        string password = Console.ReadLine();

        var user = context.Users.FirstOrDefault(u => u.Login == login);

        if (user != null)
        {
            if (PasswordHasher.VerifyPasswordBCrypt(password, user.PasswordHash))
            {
                Console.WriteLine("Успешный вход! Добро пожаловать, " + user.Name + "!");
                return user;
            }
            else
            {
                Console.WriteLine("Неверный пароль!");
                return null;
            }
        }
        else
        {
            Console.WriteLine("Пользователь с таким логином не найден!");
            return null;
        }
    }

    public static void UserMenu(Pr8GordovMainContext context, User user)
    {
        while (true)
        {
            Console.WriteLine("\n=== ЛИЧНЫЙ КАБИНЕТ (" + user.Name + ") ===");
            Console.WriteLine("1 - Просмотр товаров с возможностью добавления в корзину");
            Console.WriteLine("2 - Просмотр корзины");
            Console.WriteLine("3 - Оформление заказа");
            Console.WriteLine("4 - Просмотр истории заказов");
            Console.WriteLine("5 - Выйти из аккаунта");
            Console.Write("Выберите действие: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ProductsViewWithCart(context, user.Id);
                    break;
                case "2":
                    ShowCart(context, user.Id);
                    break;
                case "3":
                    CreateOrder(context, user.Id);
                    break;
                case "4":
                    ShowOrderHistory(context, user.Id);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }

    public static void ProductsView(Pr8GordovMainContext context)
    {
        Console.WriteLine("\n=== ПРОСМОТР ТОВАРОВ ===");
        Console.WriteLine("1 - Просмотр всех товаров");
        Console.WriteLine("2 - По категориям");
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

    public static void ProductsViewWithCart(Pr8GordovMainContext context, int userId)
    {
        Console.WriteLine("\n=== ПРОСМОТР ТОВАРОВ С ДОБАВЛЕНИЕМ В КОРЗИНУ ===");
        Console.WriteLine("1 - Просмотр всех товаров");
        Console.WriteLine("2 - По категориям");
        var input = Console.ReadLine();

        if (char.TryParse(input, out char n))
        {
            switch (n)
            {
                case '1':
                    ShowAllProductsWithCart(context, userId);
                    break;
                case '2':
                    ShowProductsByCategoryWithCart(context, userId);
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

    private static void ShowAllProducts(Pr8GordovMainContext context)
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

            Console.WriteLine("\n--- Страница " + (page + 1) + " ---");
            foreach (var product in products)
            {
                Console.WriteLine("[ID: " + product.Id + "] " + product.Name + " - " + product.Price + " руб. (" + product.Category + ")");
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

    private static void ShowAllProductsWithCart(Pr8GordovMainContext context, int userId)
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

            Console.WriteLine("\n--- Страница " + (page + 1) + " ---");
            foreach (var product in products)
            {
                Console.WriteLine("[ID: " + product.Id + "] " + product.Name + " - " + product.Price + " руб. (" + product.Category + ")");
            }

            Console.WriteLine("\nХотите добавить товар в корзину?");
            Console.WriteLine("Введите ID товара или 0 для продолжения:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                if (productId != 0)
                {
                    AddToCart(context, userId, productId);
                }
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

    private static void ShowProductsByCategory(Pr8GordovMainContext context)
    {
        Console.WriteLine("Выберите категорию: \n1. Электроника\n2. Одежда\n3. Книги\n4. Спорт\n5. Красота");
        var categoryInput = Console.ReadLine();

        var categories = new Dictionary<char, string>()
        {
            { '1', "Электроника" }, { '2', "Одежда" }, { '3', "Книги" },
            { '4', "Спорт" }, { '5', "Красота" }
        };

        if (char.TryParse(categoryInput, out char choice) && categories.ContainsKey(choice))
        {
            var products = context.Products
                .Where(p => p.Category == categories[choice])
                .ToList();

            Console.WriteLine("\n--- Товары в категории " + categories[choice] + " ---");
            foreach (var product in products)
            {
                Console.WriteLine("[ID: " + product.Id + "] " + product.Name + " - " + product.Price + " руб.");
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор категории");
        }
    }

    private static void ShowProductsByCategoryWithCart(Pr8GordovMainContext context, int userId)
    {
        Console.WriteLine("Выберите категорию: \n1. Электроника\n2. Одежда\n3. Книги\n4. Спорт\n5. Красота");
        var categoryInput = Console.ReadLine();

        var categories = new Dictionary<char, string>()
        {
            { '1', "Электроника" }, { '2', "Одежда" }, { '3', "Книги" },
            { '4', "Спорт" }, { '5', "Красота" }
        };

        if (char.TryParse(categoryInput, out char choice) && categories.ContainsKey(choice))
        {
            var products = context.Products
                .Where(p => p.Category == categories[choice])
                .ToList();

            Console.WriteLine("\n--- Товары в категории " + categories[choice] + " ---");
            foreach (var product in products)
            {
                Console.WriteLine("[ID: " + product.Id + "] " + product.Name + " - " + product.Price + " руб.");
            }

            Console.WriteLine("\nХотите добавить товар в корзину?");
            Console.WriteLine("Введите ID товара или 0 для выхода:");
            if (int.TryParse(Console.ReadLine(), out int productId) && productId != 0)
            {
                AddToCart(context, userId, productId);
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор категории");
        }
    }

    public static void AddToCart(Pr8GordovMainContext context, int userId, int productId)
    {
        var product = context.Products.FirstOrDefault(p => p.Id == productId);
        if (product == null)
        {
            Console.WriteLine("Товар с таким ID не найден!");
            return;
        }

        var existingCartItem = context.CartItems
            .FirstOrDefault(ci => ci.UserId == userId && ci.ProductId == productId);

        if (existingCartItem != null)
        {
            existingCartItem.Quantity++;
            Console.WriteLine("Товар '" + product.Name + "' добавлен в корзину (количество: " + existingCartItem.Quantity + ")");
        }
        else
        {
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                Quantity = 1
            };
            context.CartItems.Add(cartItem);
            Console.WriteLine("Товар '" + product.Name + "' добавлен в корзину!");
        }

        context.SaveChanges();
    }

    public static void ShowCart(Pr8GordovMainContext context, int userId)
    {
        var cartItems = context.CartItems
            .Where(ci => ci.UserId == userId)
            .Join(context.Products,
                  ci => ci.ProductId,
                  p => p.Id,
                  (ci, p) => new { Product = p, Quantity = ci.Quantity })
            .ToList();

        if (cartItems.Count == 0)
        {
            Console.WriteLine("Ваша корзина пуста!");
            return;
        }

        Console.WriteLine("\n=== ВАША КОРЗИНА ===");
        decimal total = 0;

        foreach (var item in cartItems)
        {
            decimal itemTotal = (decimal)(item.Product.Price * item.Quantity);
            total += itemTotal;
            Console.WriteLine(item.Product.Name + " - " + item.Product.Price + " руб. x " + item.Quantity + " = " + itemTotal + " руб.");
        }

        Console.WriteLine("\nОбщая сумма: " + total + " руб.");
    }

}