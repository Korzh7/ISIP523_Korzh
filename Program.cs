using System;
using System.Collections.Generic;

class Program
{
    static void Add(List<Product> products)
    {
        Console.WriteLine("Введите данные о товаре:");

        
        int maxId = 0;
        foreach (var product in products)
        {
            if (product.Id > maxId)
            {
                maxId = product.Id;
            }
        }
        int id = maxId + 1;

        Console.Write("Название: ");
        string name = Console.ReadLine();

        Console.Write("Цена: ");
        int price = Convert.ToInt32(Console.ReadLine());

        Console.Write("Количество: ");
        int quantity = Convert.ToInt32(Console.ReadLine());

        Console.Write("В наличии (true/false): ");
        bool isAvailable = Convert.ToBoolean(Console.ReadLine());

        Console.WriteLine("Выберите категорию:");
        Console.WriteLine("1. Электроника");
        Console.WriteLine("2. Одежда");
        Console.WriteLine("3. Еда");
        Console.WriteLine("4. Книги");
        Console.WriteLine("5. Спорт");
        Console.Write("Категория (номер): ");

        int categoryChoice = Convert.ToInt32(Console.ReadLine());
        ProductCategory category = (ProductCategory)(categoryChoice - 1);

        products.Add(new Product
        {
            Id = id,
            Name = name,
            Price = price,
            Quantity = quantity,
            IsAvailable = isAvailable,
            Category = category
        });

        Console.WriteLine("Товар добавлен!");
    }

    static void Remove(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст!");
            return;
        }

        Console.WriteLine("Список товаров:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Название: {product.Name}");
        }

        Console.Write("Введите ID товара для удаления: ");
        int idToRemove = Convert.ToInt32(Console.ReadLine());

        Product productToRemove = null;
        foreach (var product in products)
        {
            if (product.Id == idToRemove)
            {
                productToRemove = product;
                break;
            }
        }

        if (productToRemove != null)
        {
            products.Remove(productToRemove);
            Console.WriteLine($"Товар '{productToRemove.Name}' удален!");
        }
        else
        {
            Console.WriteLine("Товар с таким ID не найден!");
        }
    }
    static void ShowAllProducts(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Товаров нет!");
            return;
        }

        Console.WriteLine("\n Все товары");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Название: {product.Name}, " +
                            $"Цена: {product.Price}, Количество: {product.Quantity}, " +
                            $"В наличии: {product.IsAvailable}, Категория: {product.Category}");
        }
    }

    static void Order(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст!");
            return;
        }

        Console.WriteLine("Список товаров:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Название: {product.Name}");
        }

        Console.WriteLine("Введите название товара поставку которого хотите увеличить: ");
        string nameForOrder = Console.ReadLine();

        Product productToOrder = null;
        foreach (var product in products)
        {
            if (product.Name == nameForOrder)
            {
                productToOrder = product;
                break;
            }
        }

        if (productToOrder != null)
        {
            Console.WriteLine("Введите сколько хотите заказать товара: ");
            int quantityToOrder = Convert.ToInt32(Console.ReadLine());
            productToOrder.Quantity += quantityToOrder;
            Console.WriteLine($"Количество товара '{productToOrder.Name}' увеличено на {quantityToOrder}");
        }
        else
        {
            Console.WriteLine("Товар с таким именем не найден!");
        }
    }

    static void Sell(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст!");
            return;
        }

        Console.WriteLine("Список товаров:");
        foreach (var product in products)
        {
            Console.WriteLine($"ID: {product.Id}, Название: {product.Name}, Количество: {product.Quantity}");
        }

        Console.WriteLine("Введите название товара который хотите продать: ");
        string nameForSell = Console.ReadLine();

        Product productToSell = null;
        foreach (var product in products)
        {
            if (product.Name.Equals(nameForSell, StringComparison.OrdinalIgnoreCase))
            {
                productToSell = product;
                break;
            }
        }

        if (productToSell != null)
        {
            Console.WriteLine("Введите сколько хотите продать товара: ");
            int quantityToSell = Convert.ToInt32(Console.ReadLine());

            if (productToSell.Quantity <= quantityToSell)
            {
                products.Remove(productToSell);
                Console.WriteLine($"Товар '{productToSell.Name}' удален!");
            }
            else
            {
                productToSell.Quantity -= quantityToSell;
                Console.WriteLine($"Продано {quantityToSell} единиц товара '{productToSell.Name}'");
            }
        }
        else
        {
            Console.WriteLine("Товар с таким именем не найден!");
        }
    }
    static void SearchByAnything(List<Product> products)
    {
        if (products.Count == 0)
        {
            Console.WriteLine("Список товаров пуст!");
            return;
        }

        Console.WriteLine("По какому параметру будете искать товар");
        Console.WriteLine("1. ID");
        Console.WriteLine("2. Название");
        Console.WriteLine("3. Категория");

        int which = Convert.ToInt32(Console.ReadLine());

        if (which == 1)
        {
            Console.WriteLine("Введите ID товара: ");
            int idToSearch = Convert.ToInt32(Console.ReadLine());

            Product foundProduct = null;
            foreach (var product in products)
            {
                if (product.Id == idToSearch)
                {
                    foundProduct = product;
                    break;
                }
            }

            if (foundProduct != null)
            {
                Console.WriteLine($"{foundProduct.Id} - {foundProduct.Name} - {foundProduct.Category}");
            }
            else
            {
                Console.WriteLine("Товар с таким ID не найден!");
            }
        }
        else if (which == 2)
        {
            Console.WriteLine("Введите название товара: ");
            string nameToSearch = Console.ReadLine();

            Product foundProduct = null;
            foreach (var product in products)
            {
                if (product.Name.Equals(nameToSearch, StringComparison.OrdinalIgnoreCase))
                {
                    foundProduct = product;
                    break;
                }
            }

            if (foundProduct != null)
            {
                Console.WriteLine($"{foundProduct.Id} - {foundProduct.Name} - {foundProduct.Category}");
            }
            else
            {
                Console.WriteLine("Товар с таким названием не найден!");
            }
        }
        else if (which == 3)
        {
            Console.WriteLine("Выберите категорию товара: ");
            Console.WriteLine("1. Электроника");
            Console.WriteLine("2. Одежда");
            Console.WriteLine("3. Еда");
            Console.WriteLine("4. Книги");
            Console.WriteLine("5. Спортивное");

            int categoryChoice = Convert.ToInt32(Console.ReadLine());

            if (categoryChoice >= 1 && categoryChoice <= 5)
            {
                ProductCategory selectedCategory = (ProductCategory)categoryChoice;

                List<Product> productsInCategory = new List<Product>();
                foreach (var product in products)
                {
                    if (product.Category == selectedCategory)
                    {
                        productsInCategory.Add(product);
                    }
                }

                if (productsInCategory.Count > 0)
                {
                    Console.WriteLine($"\nТовары в категории '{selectedCategory}':");
                    foreach (var product in productsInCategory)
                    {
                        Console.WriteLine($"{product.Id} - {product.Name} - Цена: {product.Price} - Количество: {product.Quantity}");
                    }
                }
                else
                {
                    Console.WriteLine($"Товары в категории '{selectedCategory}' не найдены!");
                }
            }
            else
            {
                Console.WriteLine("Неверный выбор категории!");
            }
        }
        else
        {
            Console.WriteLine("Неверный выбор параметра поиска!");
        }
    }

    static void Main(string[] args)
    {
        List<Product> products = new List<Product>();

        while (true)
        {

            Console.WriteLine("1. Добавить товар");
            Console.WriteLine("2. Удалить товар");
            Console.WriteLine("3. Показать все товары");
            Console.WriteLine("4. Заказать товар (увеличить количество)");
            Console.WriteLine("5. Продать товары");
            Console.WriteLine("6. Поиск товаров");
            Console.WriteLine("7. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Add(products);
                    break;
                case "2":
                    Remove(products);
                    break;
                case "3":
                    ShowAllProducts(products);
                    break;
                case "4":
                    Order(products);
                    break;
                case "5":
                    Sell(products);
                    break;
                case "6":
                    SearchByAnything(products);
                    break;
                case "7":
                    Console.WriteLine("Выход из программы...");
                    return;
                default:
                    Console.WriteLine("Неверный выбор!");
                    break;
            }
        }
    }
}



public enum ProductCategory
{
    Электроника = 1,
    Одежда = 2,
    Еда = 3,
    Книги = 4,
    Спортивное = 5
}

class Product
{
    public int Id;
    public string Name;
    public int Price;
    public int Quantity;
    public bool IsAvailable;
    public ProductCategory Category;
};