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

void output(string[] productOrService, int[] cost,  int quantity)
{
    for (int i = 0; i < quantity; i++)
    {
        Console.WriteLine($"{productOrService[i]} - {cost[i]} руб.");
    }
}

(int average, int max, int min, int sum) stats(string[] productOrService, int[] cost, int quantity)
{
    int average = 0, max = 0, min = 0, sum = 0;
    for (int i = 0; i < quantity; i++)
    {
        average += cost[i];
        if (min > cost[i]) min = cost[i];
        if (max <  cost[i]) max = cost[i];
        sum+= cost[i];


    }
    average /= quantity;
    return (average, min, max, sum);

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
        output(productOrService, cost, quantity);
    }
}