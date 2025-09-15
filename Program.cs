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
