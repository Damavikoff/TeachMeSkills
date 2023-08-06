while (true)
{
    Console.WriteLine("Введите целое число: ");
    string userInput = Console.ReadLine();
    int number;
    if (int.TryParse(userInput, out number))
    {
        if (number % 2 == 0)
        {
            Console.WriteLine("Четное");
        }
        else
        {
            Console.WriteLine("Нечетное");
        }
    }
    else
    {
        Console.WriteLine("Вы ввели не целое число. Повторите ввод");
    }
}