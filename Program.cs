static void InitializeTestData()
{
    books.AddRange(new[]
    {
        new Book { Id = nextId++, Title = "Властелин Колец", Author = "Дж. Р. Р. Толкин", Genre = Genre.Fantasy, Year = 1954, Price = 1500 },
        new Book { Id = nextId++, Title = "1984", Author = "Джордж Оруэлл", Genre = Genre.ScienceFiction, Year = 1949, Price = 800 },
        new Book { Id = nextId++, Title = "Убийство в Восточном экспрессе", Author = "Агата Кристи", Genre = Genre.Mystery, Year = 1934, Price = 700 },
        new Book { Id = nextId++, Title = "Гордость и предубеждение", Author = "Джейн Остин", Genre = Genre.Romance, Year = 1813, Price = 600 },
        new Book { Id = nextId++, Title = "Шерлок Холмс", Author = "Артур Конан Дойл", Genre = Genre.Mystery, Year = 1887, Price = 900 }
    });

    Console.WriteLine("Добавлено 5 тестовых книг.");
}

static void ShowMenu()
{
    Console.WriteLine("\n=== ГЛАВНОЕ МЕНЮ ===");
    Console.WriteLine("1. Добавить книгу");
    Console.WriteLine("2. Удалить книгу по ID");
    Console.WriteLine("3. Найти книги");
    Console.WriteLine("4. Отсортировать книги");
    Console.WriteLine("5. Самая дорогая и дешёвая книга");
    Console.WriteLine("6. Книги по авторам");
    Console.WriteLine("7. Показать все книги");
    Console.WriteLine("8. Выход");
    Console.Write("Выберите действие: ");
}


static void Main(string[] args)
{
    InitializeTestData();

    Console.WriteLine("=== СИСТЕМА УЧЁТА БИБЛИОТЕКИ ===");

    while (true)
    {
        ShowMenu();

        int choice;
        if (!int.TryParse(Console.ReadLine(), out choice))
        {
            Console.WriteLine("Ошибка ввода! Введите число от 1 до 8.");
            continue;
        }

        switch (choice)
        {
            case 1:
               
                break;
            case 8:
                Console.WriteLine("До свидания!");
                return;
            default:
                Console.WriteLine("Неверный выбор! Введите число от 1 до 8.");
                break;
        }
    }
}