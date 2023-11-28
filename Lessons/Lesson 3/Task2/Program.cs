while (true)
{
    Console.WriteLine("Укажите номер месяца: ");
    string userInput = Console.ReadLine();
    string userOutput;

    if (int.TryParse(userInput, out int numberOfMonth))
    { 
        if ((numberOfMonth >= 1 && numberOfMonth <= 2) || (numberOfMonth == 12))
        {
            userOutput = "Зима";
        }
        else if (numberOfMonth >= 3 && numberOfMonth <= 5)
        {
            userOutput = "Весна";
        }
        else if (numberOfMonth >= 6 && numberOfMonth <= 8)
        {
            userOutput = "Лето";
        }
        else if (numberOfMonth >= 9 && numberOfMonth <= 11)
        {
            userOutput = "Осень";
        }
        else
        {
            userOutput = "Нужно указать число от 1 до 12 (включительно)";
        }
    }
    else
    {
        userOutput = "Вы ввели не целое число. Повторите ввод";
    }
    Console.WriteLine(userOutput);
}
