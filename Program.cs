//1.Вывод данных
//    2.Статистика(среднее, максимальное, минимальное, сумма)
//    3.Сортировка по цене(пузырьковая сортировка)
//    4.Конвертация валюты(пользователь вводит курс или выбирает из списка)
//    5.Поиск по названию 
//    0. Выход

using System.Net.Http.Headers;

Console.WriteLine("Введите кол-во операций: ");
int quantity = Convert.ToInt32(Console.ReadLine());

string[] productOrService = new string[quantity];
int[] cost = new int[quantity];
for (int i = 0; i < quantity; i++)
{
    Console.WriteLine("Введите услугу или товар: ");
    productOrService[i] = Console.ReadLine();
    Console.WriteLine("Введите стоимость: ");
    cost[i] = Convert.ToInt32(Console.ReadLine());
    

}


void outputValues(string[] productOrService, int[] cost,  int quantity)
{
    for (int i = 0; i < quantity; i++)
    {
        Console.WriteLine($"{productOrService[i]} - {cost[i]} руб.");
    }
}

(int average, int max, int min, int sum) stats(string[] productOrService, int[] cost, int quantity)
{
    int average = 0, max = 0, min = 9999, sum = 0;
    for (int i = 0; i < quantity; i++)
    {
        average += cost[i];
        if (min > cost[i]) min = cost[i];
        if (max <  cost[i]) max = cost[i];
        sum+= cost[i];


    }
    average /= quantity;
    return (average, max, min, sum);

}

void bubbleSorteCost(string[] productOrService, int[] cost, int quantity)
{

    for (int i = 0; i < quantity; i++)
    {
        for (int j = 0; j < quantity - 1; j++)
        {
            if (cost[i] < cost[j])
            {
                int temp = cost[i];
                cost[i] = cost[j];
                cost[j] = temp;
                string temp2 = productOrService[i];
                productOrService[i] = productOrService[j];
                productOrService[j] = temp2;
            }
        }
    }
    
}

void currencyConverter(int[] cost, int quantity)
{
    Console.WriteLine("Выберите в какую валюту хотите перевести: ");
    Console.WriteLine("1. Доллар");
    Console.WriteLine("2. Евро");
    Console.WriteLine("3. Тугрики");
    
    int n = Convert.ToInt32(Console.ReadLine());
    decimal usdRate = 83.0m;
    decimal eurRate = 97.0m;
    decimal tugrRate = 0.02m;

    if (n == 1)
    {
        for(int i = 0; i < quantity; i++)
        {
            
            cost[i] = (int)(cost[i] * usdRate);
        }
    }
    if (n == 2)
    {
        for (int i = 0; i < quantity; i++)
        {
            cost[i] = (int)(cost[i] * eurRate);
        }
    }
    if (n == 3)
    {
        for (int i = 0; i < quantity; i++)
        {
            cost[i] = (int)(cost[i] * tugrRate);
        }
    }
}

void searchByName(string[] producrOrService, int[] cost, int quantity)
{
    Console.WriteLine("Введите товар или услугу стоимость которой хотите увидеть: ");
    string pos = Console.ReadLine();
    for (int i = 0; i < quantity; i++)
    {
        if (pos == producrOrService[i])
        {
            Console.WriteLine(cost[i]);
        }
    }
}


Console.WriteLine("Меню:");
Console.WriteLine("1. Вывод данных");
Console.WriteLine("2. Статистика");
Console.WriteLine("3. Сортировка по цене");
Console.WriteLine("4. Конвертация валюты");
Console.WriteLine("5. Поиск по названию ");
Console.WriteLine("0. Выход");

for (int i = 0; i < 100000; i++)
{
    
    int n = Convert.ToInt32(Console.ReadLine());
    if (n == 1)
    {
        outputValues(productOrService, cost, quantity);
    }
    if (n == 2) {
        var result = stats(productOrService, cost, quantity);
        Console.WriteLine($"Среднее: {result.average}, Мин: {result.min}, Макс: {result.max}, Сумма: {result.sum}");
    }
    if (n == 3) {
        bubbleSorteCost(productOrService, cost, quantity);
    }
    if (n == 4) { 
        currencyConverter(cost, quantity);
    }
    if (n == 5) {
        searchByName(productOrService, cost, quantity);
    }
    if (n == 0) {
        break;    
    }
}