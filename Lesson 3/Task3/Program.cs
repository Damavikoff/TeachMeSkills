while (true)
{
    Console.WriteLine("Введите целое число: ");
    string userInput = Console.ReadLine();
    string userOutput;

    if (int.TryParse(userInput, out int number))
    {
        userOutput = number % 2 == 0 ? "Четное" : "Нечетное";
    }
    else
    {
        userOutput = "Вы ввели не целое число. Повторите ввод";
    }
    Console.WriteLine(userOutput);
}