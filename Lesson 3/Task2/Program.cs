while (true)
{
    Console.WriteLine("Укажите номер месяца: ");
    string userInput = Console.ReadLine();
    int numberOfMonth;

    if (int.TryParse(userInput, out numberOfMonth))
    {
        numberOfMonth = Convert.ToInt32(userInput);
        if ((numberOfMonth > 0 && numberOfMonth <= 2) || (numberOfMonth == 12))
        {
            Console.WriteLine("Зима");
        }
        else if (numberOfMonth >= 3 && numberOfMonth <= 5)
        {
            Console.WriteLine("Весна");
        }
        else if (numberOfMonth >= 6 && numberOfMonth <= 8)
        {
            Console.WriteLine("Лето");
        }
        else if (numberOfMonth >= 9 && numberOfMonth <= 11)
        {
            Console.WriteLine("Осень");
        }
        else
        {
            Console.WriteLine("Нужно указать число от 1 до 12 (включительно)");
        }
    }
    else
    {
        Console.WriteLine("Вы ввели не целое число. Повторите ввод");
    }
}