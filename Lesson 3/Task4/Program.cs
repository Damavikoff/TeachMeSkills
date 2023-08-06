while (true)
{
    Console.WriteLine("Введите температуру: ");
    string userInput = Console.ReadLine();
    int temperature;
    if (int.TryParse(userInput, out temperature))
    {
        if (temperature > -5)
        {
            Console.WriteLine("Тепло");
        }
        else if (temperature <= -5 && temperature > -20)
        {
            Console.WriteLine("Нормально");
        }
        else if (temperature <= -20)
        {
            Console.WriteLine("Холодно");
        }
    }
    else
    {
        Console.WriteLine("Вы ввели не целое число. Повторите ввод");
    }

}


//каждое подзадание в виде отдельного коммита с соотв комментом