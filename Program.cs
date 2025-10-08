static void AddBook()
{
    Console.WriteLine("\n=== ДОБАВЛЕНИЕ НОВОЙ КНИГИ ===");

    Console.Write("Название: ");
    string title = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(title))
    {
        Console.WriteLine("Ошибка: название не может быть пустым!");
        return;
    }

    Console.Write("Автор: ");
    string author = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(author))
    {
        Console.WriteLine("Ошибка: автор не может быть пустым!");
        return;
    }

    Console.WriteLine("Выберите жанр:");
    var genres = Enum.GetValues(typeof(Genre));
    for (int i = 0; i < genres.Length; i++)
    {
        Console.WriteLine($"{i + 1}. {genres.GetValue(i)}");
    }

    Console.Write("Жанр (номер): ");
    if (!int.TryParse(Console.ReadLine(), out int genreChoice) || genreChoice < 1 || genreChoice > genres.Length)
    {
        Console.WriteLine($"Ошибка: выберите жанр от 1 до {genres.Length}!");
        return;
    }

    Console.Write("Год издания: ");
    if (!int.TryParse(Console.ReadLine(), out int year) || year < 1000 || year > DateTime.Now.Year)
    {
        Console.WriteLine($"Ошибка: год должен быть от 1000 до {DateTime.Now.Year}!");
        return;
    }

    Console.Write("Цена: ");
    if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price < 0)
    {
        Console.WriteLine("Ошибка: цена должна быть положительным числом!");
        return;
    }

    var newBook = new Book
    {
        Id = nextId++,
        Title = title.Trim(),
        Author = author.Trim(),
        Genre = (Genre)(genreChoice - 1),
        Year = year,
        Price = price
    };

    books.Add(newBook);
    Console.WriteLine($"Книга '{title}' успешно добавлена с ID {newBook.Id}!");
}

static void RemoveBook()
{
    if (!books.Any())
    {
        Console.WriteLine("Библиотека пуста!");
        return;
    }

    Console.WriteLine("\n=== УДАЛЕНИЕ КНИГИ ===");
    ShowAllBooks();

    Console.Write("Введите ID книги для удаления: ");
    if (!int.TryParse(Console.ReadLine(), out int idToRemove))
    {
        Console.WriteLine("Ошибка: ID должен быть числом!");
        return;
    }

    var bookToRemove = books.FirstOrDefault(b => b.Id == idToRemove);

    if (bookToRemove != null)
    {
        books.Remove(bookToRemove);
        Console.WriteLine($"Книга '{bookToRemove.Title}' успешно удалена!");
    }
    else
    {
        Console.WriteLine("Книга с таким ID не найдена!");
    }
}


switch (choice)
{
    case 1:
        AddBook();
        break;
    case 2:
        RemoveBook();
        break;
    case 8:
        Console.WriteLine("До свидания!");
        return;
    default:
        Console.WriteLine("Неверный выбор! Введите число от 1 до 8.");
        break;
}