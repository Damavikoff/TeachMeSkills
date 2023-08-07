while (true)
{
    Console.WriteLine("Введите температуру: ");
    string userInput = Console.ReadLine();
    string userOutput;

    if (int.TryParse(userInput, out int temperature))
    { 
        if (temperature > -5)
        {
            userOutput = "Тепло";
        }
        else if (temperature > -20)
        {
            userOutput = "Нормально";
        }
        else
        {
            userOutput = "Холодно";
        }
    }
    else
    {
        userOutput = "Вы ввели не целое число. Повторите ввод";
    }
    Console.WriteLine(userOutput);
}


