static void ShowPriceExtremes()
{
    if (!books.Any())
    {
        Console.WriteLine("Библиотека пуста!");
        return;
    }

    Console.WriteLine("\n=== САМАЯ ДОРОГАЯ И ДЕШЁВАЯ КНИГА ===");

    List<Book> expensiveBooks = books
        .OrderByDescending(b => b.Price)
        .Take(1)
        .ToList();

    List<Book> cheapBooks = books
        .OrderBy(b => b.Price)
        .Take(1)
        .ToList();

    if (expensiveBooks.Count > 0)
    {
        Console.WriteLine($"Самая дорогая книга: {expensiveBooks[0]}");
    }
    if (cheapBooks.Count > 0)
    {
        Console.WriteLine($"Самая дешёвая книга: {cheapBooks[0]}");
    }
}

static void ShowBooksByAuthors()
{
    if (!books.Any())
    {
        Console.WriteLine("Библиотека пуста!");
        return;
    }

    Console.WriteLine("\n=== КНИГИ ПО АВТОРАМ ===");

    List<IGrouping<string, Book>> booksByAuthor = books
        .GroupBy(b => b.Author)
        .OrderBy(g => g.Key)
        .ToList();

    foreach (var authorGroup in booksByAuthor)
    {
        List<Book> authorBooks = authorGroup
            .OrderBy(b => b.Year)
            .ToList();

        Console.WriteLine($"\nАвтор: {authorGroup.Key}");
        Console.WriteLine($"Количество книг: {authorBooks.Count}");
        Console.WriteLine("Книги:");
        foreach (var book in authorBooks)
        {
            Console.WriteLine($"  - {book.Title} ({book.Year}) - {book.Genre} - {book.Price:C}");
        }
        decimal totalValue = authorBooks.Sum(b => b.Price);
        Console.WriteLine($"Общая стоимость: {totalValue:C}");
    }
}

static void ShowAllBooks()
{
    if (!books.Any())
    {
        Console.WriteLine("Библиотека пуста!");
        return;
    }

    Console.WriteLine("\n=== ВСЕ КНИГИ В БИБЛИОТЕКЕ ===");
    Console.WriteLine($"Всего книг: {books.Count}");
    decimal totalValue = books.Sum(b => b.Price);
    Console.WriteLine($"Общая стоимость коллекции: {totalValue:C}");
    Console.WriteLine(new string('-', 80));

    foreach (var book in books)
    {
        Console.WriteLine(book);
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
    case 3:
        SearchBooks();
        break;
    case 4:
        SortBooks();
        break;
    case 5:
        ShowPriceExtremes();
        break;
    case 6:
        ShowBooksByAuthors();
        break;
    case 7:
        ShowAllBooks();
        break;
    case 8:
        Console.WriteLine("До свидания!");
        return;
    default:
        Console.WriteLine("Неверный выбор! Введите число от 1 до 8.");
        break;
}