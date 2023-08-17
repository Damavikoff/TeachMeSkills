//2 Реализовать калькулятор целых чисел (только простые одинарные операции +-*/) как консольное приложение.
// Пример: пользователь вводит строку в консоли "12 + 143" (пробелы не обязательны). На выходе получает 155. 
// Приложение должно предлагать пользователю повторный ввод, если запись невозможно обработать (неправильный ввод пользователя). 

using System.Text.RegularExpressions;


while (true)
{
    string pattern = @"^\d+[(+-/*)](\d+)$";
    string number = @"\d+";
    
    var userInput = Console.ReadLine();
    if (Regex.IsMatch(userInput, pattern))
    {
        string target = "";
        Regex regex = new Regex(number);
        string operation = regex.Replace(userInput, target);

        MatchCollection matches = regex.Matches(userInput);
        List<int> numbers = new List<int>();
        if (matches.Count > 0)
        {
            foreach (Match match in matches)
                numbers.Add((Convert.ToInt32(match.Value)));
        }
        switch (operation)
        {
            case "+":
                int sum = numbers[0] + numbers[1];
                Console.WriteLine(" = " + sum);
                    break;
            case "-":
                int sub = numbers[0] - numbers[1];
                Console.WriteLine(" = " + sub);
                break;
            case "/":
                double div = Convert.ToDouble(numbers[0]) / Convert.ToDouble(numbers[1]);
                Console.WriteLine(" = " + div);
                break;
            case "*":
                int mul = numbers[0] * numbers[1];
                Console.WriteLine(" = " + mul);
                break;
        }
    }
    else
    {
        Console.WriteLine("Incorrect input, please try again (example: 2+2)");
    }
}