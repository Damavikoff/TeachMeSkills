using System.Text;

string path = null;  //text.txt
string text = null;
string[] formatedTextByWords = null;
string[] formatedTextByPunctiation = null;

while (text == null)
{
    path = Console.ReadLine();
    TryConnection(path);
}
void TryConnection(string path)
{
    if (File.Exists(path))
    {
        text = File.ReadAllText(path);
        formatedTextByWords = text.Split(new Char[] { ' ', ',', '.', ':', '!', '?', ';' }, StringSplitOptions.RemoveEmptyEntries);
        formatedTextByPunctiation = text.Split(new Char[] { '.', '!', '?' });
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
static string FromNumbersToWords(int number) => number switch
{
    1 => "one",
    2 => "two",
    3 => "three",
    4 => "four",
    5 => "five",
    6 => "six",
    7 => "seven",
    8 => "eight",
    9 => "nine",
    0 => "zero"
};
void FindLargestWord()
{
    string str = null;
    int maxLenght = 0, count = 0;
    foreach (string s in formatedTextByWords)
    {
        if (maxLenght < s.Length)
        {
            str = s;
            maxLenght = s.Length;
        }
    }
    for (int i = 0; formatedTextByWords.Length > i; i++)
    {
        if (formatedTextByWords[i].Length == maxLenght)
        {
            count++;
        }
    }
}
void FindExclamationAndQuestion()
{
    StringBuilder withExclamations = new StringBuilder();
    StringBuilder withQuestions = new StringBuilder();
    string trimmedText;
    trimmedText = text.Replace("?", "?/").Replace("!", "!/");
    string[] formatedText3 = trimmedText.Split(new Char[] { '.', '/' });
    foreach (string s in formatedText3)
    {
        if (s.Contains('?') == true)
        {
            string trimmed = s.Trim();
            withQuestions.AppendLine(trimmed.Substring(0, trimmed.IndexOf('?') + 1));
        }
    }
    foreach (string s in formatedText3)
    {
        if (s.Contains('!') == true)
        {
            string trimmed = s.Trim();
            withExclamations.AppendLine(trimmed.Substring(0, trimmed.IndexOf('!') + 1));
        }
    }
    Console.WriteLine(withQuestions.ToString(), withExclamations.ToString());
}
void ChangeNumbersToWords()
{
    string replacedText = null;
    for (int i = 0; i < 9; i++)
    {
        string numberWord = FromNumbersToWords(i);
        replacedText = text.Replace(i.ToString(), numberWord);
    }
}
void FindWordsWithSameEndAndStart()
{
    StringBuilder sameEndAndStart = new StringBuilder();
    for (int i = 0; i < formatedTextByWords.Length; i++)
    {            
        if (formatedTextByWords[i][0] == formatedTextByWords[i][formatedTextByWords[i].Length - 1])
        {
            sameEndAndStart.AppendLine(formatedTextByWords[i]);
        }
    }
    Console.WriteLine(sameEndAndStart);
}
void FindStringsWithoutPunctiation()
{
    StringBuilder withoutPunctiation = new StringBuilder();
    foreach (string s in formatedTextByPunctiation)
    {
        if (!s.Contains(',')) //если нет запятой
            withoutPunctiation.Append(s);           
    }
    Console.WriteLine(withoutPunctiation);
}
void FindWordsWithMaxNumbers()
{
    int maxLenght = 0, index = 0;
    for (int i = 0; i < formatedTextByWords.Length; i++)
    {
        int maxNumber = 0;
        for (int j = 0; j < formatedTextByWords[i].Length; j++)
        {
            if (char.IsNumber(formatedTextByWords[i][j]))
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
while (true)
{
    string userInput = Console.ReadLine();
    bool succes = int.TryParse(userInput, out var number);
    if (succes)
    {      
        switch (number)
        {
            case 1:
                FindLargestWord();
                break;
            case 2:
                ChangeNumbersToWords();
                break;
            case 3:
                FindExclamationAndQuestion();
                break;
            case 4:
                FindStringsWithoutPunctiation();
                break;
            case 5:
                FindWordsWithSameEndAndStart();
                break;
            case 6:
                FindWordsWithMaxNumbers();
                break;
            default:
                Console.WriteLine("Введите диапазон от 1 до 6");
                break;
        }
    }
    else
    {
        Console.WriteLine($"{userInput}" + " не цифра");
    }
}