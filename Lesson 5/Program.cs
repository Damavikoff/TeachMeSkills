string path = null;  //text.txt
string text = null;
while (text == null)
{
    path = Console.ReadLine();
    if (File.Exists(path))
    {
        StreamReader sr = File.OpenText(path);
        text = sr.ReadToEnd();
        if (text.Length == 0)
        {
            Console.WriteLine($"{path}" + " пустой");
            text = null;
        }
    }
    else
    {
        Console.WriteLine($"{path}" + " не существует");
    }
}
while (true)
{
    string userInput = Console.ReadLine();
    bool result = int.TryParse(userInput, out var number);
    if (result == true)
    {
        string[] stringSplit = text.Split(new Char[] { ' ', ',', '.', ':', '!', '?', ';' }, StringSplitOptions.RemoveEmptyEntries);
        string[] stringSplit2 = text.Split(new Char[] { '.', '!', '?' });
        if (number == 1) //Найти самое длинное слово и определить, сколько раз оно встретилось в тексте
        {
            string str = null;
            int maxLenght = 0, count = 0;
            foreach (string s in stringSplit)
            {
                if (maxLenght < s.Length)
                {
                    str = s;
                    maxLenght = s.Length;
                }
            }
            for (int i = 0; stringSplit.Length > i; i++)
            {
                if (stringSplit[i].Length == maxLenght)
                {
                    count++;
                }
            }
        }
        else if (number == 2) //Заменить цифры от 0 до 9 на слова «ноль», «один», …, «девять».
        {
            string replacedText;
            replacedText = text.Replace("1", "один").Replace("2", "два")
                .Replace("3", "три").Replace("4", "четыре")
                .Replace("5", "пять").Replace("6", "шесть")
                .Replace("7", "семь").Replace("8", "восемь")
                .Replace("9", "девять").Replace("0", "ноль");
        }
        else if (number == 3)//Вывести на экран сначала вопросительные, а затем восклицательные предложения.
        {
            string trimmedText;
            trimmedText = text.Replace("?", "?/").Replace("!", "!/");
            string[] stringSplit3 = trimmedText.Split(new Char[] { '.', '/' }); //


            foreach (string s in stringSplit3)
            {
                if (s.Contains('?') == true)
                {
                    string trimmed = s.Trim();
                    Console.WriteLine(trimmed.Substring(0, trimmed.IndexOf('?') + 1));
                }
            }
            foreach (string s in stringSplit3)
            {
                if (s.Contains('!') == true)
                {
                    string trimmed = s.Trim();
                    Console.WriteLine(trimmed.Substring(0, trimmed.IndexOf('!') + 1));
                }
            }

        }
        else if (number == 4)//Вывести на экран только предложения, не содержащие запятых.
        {
            foreach (string s in stringSplit2)
            {
                if (!s.Contains(','))
                    Console.WriteLine(s.Trim());
            }
        }
        else if (number == 5)//Найти слова, начинающиеся и заканчивающиеся на одну и ту же букву.
        {
            for (int i = 0; i < stringSplit.Length; i++)
            {
                if (stringSplit[i][0] == stringSplit[i][stringSplit[i].Length - 1])
                {
                    Console.WriteLine(stringSplit[i]);
                }
            }
        }
        else if (number == 6)//Найти слова, содержащие максимальное количество цифр
        {
            int maxLenght = 0, index = 0;
            for (int i = 0; i < stringSplit.Length; i++)
            {
                int maxNumber = 0;
                for (int j = 0; j < stringSplit[i].Length; j++)
                {
                    if (char.IsNumber(stringSplit[i][j]))
                    {
                        maxNumber++;
                    }
                }
                if (maxNumber > maxLenght)
                {
                    maxLenght = maxNumber;
                    index = i;
                }
            }
        }
    }
    else
    {
        Console.WriteLine($"{userInput}" + " не цифра");
    }
}



