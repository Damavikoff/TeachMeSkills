while (true)
{
    Console.WriteLine("Укажите номер месяца: ");
    string userInput = Console.ReadLine();
    string userOutput;

    if (int.TryParse(userInput, out int numberOfMonth))
    { 
        switch (numberOfMonth)
        {
            case 1:
            case 2:
            case 12:
                userOutput = "Зима";
                break;
            case 3:
            case 4:
            case 5:
                userOutput = "Весна";
                break;
            case 6:
            case 7:
            case 8:
                userOutput = "Лето";
                break;
            case 9:
            case 10:
            case 11:
                userOutput = "Осень";
                break;
            
            default:
                userOutput = "Нужно указать число от 1 до 12 (включительно)";
                break;
        }
    }
    else
    {
        userOutput = "Вы ввели не целое число. Повторите ввод";
    }
    Console.WriteLine(userOutput);
}