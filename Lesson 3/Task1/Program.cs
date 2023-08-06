while (true)
{
    Console.WriteLine("Укажите номер месяца: ");
    string userInput = Console.ReadLine();
    int numberOfMonth;

    if (int.TryParse(userInput, out numberOfMonth))
    {
        numberOfMonth = Convert.ToInt32(userInput);
        switch (numberOfMonth)
        {
            case 1:
                Console.WriteLine("Зима");
                break;
            case 2:
                Console.WriteLine("Зима");
                break;
            case 3:
                Console.WriteLine("Весна");
                break;
            case 4:
                Console.WriteLine("Весна");
                break;
            case 5:
                Console.WriteLine("Весна");
                break;
            case 6:
                Console.WriteLine("Лето");
                break;
            case 7:
                Console.WriteLine("Лето");
                break;
            case 8:
                Console.WriteLine("Лето");
                break;
            case 9:
                Console.WriteLine("Осень");
                break;
            case 10:
                Console.WriteLine("Осень");
                break;
            case 11:
                Console.WriteLine("Осень");
                break;
            case 12:
                Console.WriteLine("Зима");
                break;
            default:
                Console.WriteLine("Нужно указать число от 1 до 12 (включительно)");
                break;
        }
    }
    else
    {
        Console.WriteLine("Вы ввели не целое число. Повторите ввод");
    }
}