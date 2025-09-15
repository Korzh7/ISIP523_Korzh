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
    Console.WriteLine("Введите услугу или товар и стоимость (через ;): ");
    string str = Console.ReadLine();
    string[] words = str.Split(new char[] { ';' });
    productOrService[i] = words[0];
    cost[i] = Convert.ToInt32(words[1]);

}


void outputValues(string[] productOrService, int[] cost,  int quantity)
{
    for (int i = 0; i < quantity; i++)
    {
        Console.WriteLine($"{productOrService[i]} - {cost[i]} руб.");
    }
}

void stats(string[] productOrService, int[] cost, int quantity)
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
    Console.WriteLine($"{average} "average.ToString(), sum.ToString(), min.ToString(), max.ToString());

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
    Console.WriteLine("3. Тугрик");
    Console.WriteLine("4. Своя валюта");

    decimal[] costcopy = new decimal[quantity];
    for(int i = 0; i < quantity; i++)
    {
        costcopy[i] = cost[i];
    }

    int n = Convert.ToInt32(Console.ReadLine());
    string choice = "";
    decimal usdRate = 83.0m;
    decimal eurRate = 97.0m;
    decimal tugrRate = 0.02m;
    decimal perRate = 0;

    if (n == 1)
    {
        choice = "Доллар";
        for(int i = 0; i < quantity; i++)
        {
            
            costcopy[i] = (decimal)(costcopy[i] / usdRate);
        }
    }
    if (n == 2)
    {
        for (int i = 0; i < quantity; i++)
        {
            costcopy[i] = (decimal)(costcopy[i] / eurRate);
        }
    }
    if (n == 3)
    {
        for (int i = 0; i < quantity; i++)
        {
            costcopy[i] = (decimal)(costcopy[i] / tugrRate);
        }
    }
    if(n == 4)
    {
        Console.WriteLine("Введите курс: ");
        perRate = Convert.ToDecimal(Console.ReadLine());
        for (int i = 0; i < quantity; i++)
        {
            costcopy[i] = (decimal)(costcopy[i] / perRate);
        }
    }
    for (int i = 0; i < quantity; i++)
    {
        Console.WriteLine($"{productOrService[i]} - {costcopy[i]} {choice} (а/ов)");
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
        stats(productOrService, cost, quantity);
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