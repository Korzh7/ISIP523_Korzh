static void SearchBooks()
{
    if (!books.Any())
    {
        Console.WriteLine("Библиотека пуста!");
        return;
    }

    Console.WriteLine("\n=== ПОИСК КНИГ ===");
    Console.WriteLine("1. По названию");
    Console.WriteLine("2. По автору");
    Console.WriteLine("3. По жанру");
    Console.Write("Выберите тип поиска: ");

    if (!int.TryParse(Console.ReadLine(), out int searchType) || searchType < 1 || searchType > 3)
    {
        Console.WriteLine("Ошибка: выберите вариант от 1 до 3!");
        return;
    }

    List<Book> searchResults;

    switch (searchType)
    {
        case 1:
            Console.Write("Введите название для поиска: ");
            string titleSearch = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(titleSearch))
            {
                Console.WriteLine("Поисковый запрос не может быть пустым!");
                return;
            }
            searchResults = books
                .Where(b => b.Title.ToLower().Contains(titleSearch.ToLower()))
                .ToList();
            break;
        case 2:
            Console.Write("Введите автора для поиска: ");
            string authorSearch = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(authorSearch))
            {
                Console.WriteLine("Поисковый запрос не может быть пустым!");
                return;
            }
            searchResults = books
                .Where(b => b.Author.ToLower().Contains(authorSearch.ToLower()))
                .ToList();
            break;
        case 3:
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
            Genre selectedGenre = (Genre)(genreChoice - 1);
            searchResults = books
                .Where(b => b.Genre == selectedGenre)
                .ToList();
            break;
        default:
            return;
    }

    if (searchResults.Count > 0)
    {
        Console.WriteLine($"\nНайдено книг: {searchResults.Count}");
        Console.WriteLine(new string('-', 80));
        foreach (var book in searchResults)
        {
            Console.WriteLine(book);
        }
    }
    else
    {
        Console.WriteLine("Книги по вашему запросу не найдены.");
    }
}

static void SortBooks()
{
    if (!books.Any())
    {
        Console.WriteLine("Библиотека пуста!");
        return;
    }

    Console.WriteLine("\n=== СОРТИРОВКА КНИГ ===");
    Console.WriteLine("1. По названию (А-Я)");
    Console.WriteLine("2. По названию (Я-А)");
    Console.WriteLine("3. По году (сначала старые)");
    Console.WriteLine("4. По году (сначала новые)");
    Console.WriteLine("5. По цене (сначала дешёвые)");
    Console.WriteLine("6. По цене (сначала дорогие)");
    Console.Write("Выберите тип сортировки: ");

    if (!int.TryParse(Console.ReadLine(), out int sortType) || sortType < 1 || sortType > 6)
    {
        Console.WriteLine("Ошибка: выберите вариант от 1 до 6!");
        return;
    }

    List<Book> sortedBooks;

    switch (sortType)
    {
        case 1:
            sortedBooks = books
                .OrderBy(b => b.Title)
                .ToList();
            break;
        case 2:
            sortedBooks = books
                .OrderByDescending(b => b.Title)
                .ToList();
            break;
        case 3:
            sortedBooks = books
                .OrderBy(b => b.Year)
                .ToList();
            break;
        case 4:
            sortedBooks = books
                .OrderByDescending(b => b.Year)
                .ToList();
            break;
        case 5:
            sortedBooks = books
                .OrderBy(b => b.Price)
                .ToList();
            break;
        case 6:
            sortedBooks = books
                .OrderByDescending(b => b.Price)
                .ToList();
            break;
        default:
            return;
    }

    Console.WriteLine("\nОтсортированные книги:");
    Console.WriteLine(new string('-', 80));
    foreach (var book in sortedBooks)
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
    case 8:
        Console.WriteLine("До свидания!");
        return;
    default:
        Console.WriteLine("Неверный выбор! Введите число от 1 до 8.");
        break;
}