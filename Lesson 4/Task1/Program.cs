Random random = new Random();
int[] array = new int[10];
static int[] FillArrayWithRandomNumbers(int[] array, Random random)
{
    for (int i = 0; i < array.Length; i++)
    {
        int randomValue = random.Next(1, 20);
        array[i] = randomValue;
    }
    return array;
}

//Реализовать алгоритм нахождения максимального элемента в одномерном массиве целых чисел (интов). Массив передается как параметр, на выходе int.
Console.WriteLine("\n" + "Максимальное число массива arrayMax: " + FindMax(FillArrayWithRandomNumbers(array, random)));
static int FindMax(int[] array)
{
    return array.Max();
}

//Реализовать алгоритм нахождения среднего арифметического всех элементов  зубчатого массива. Массив передается как параметр, на выходе double.
int[][] arrayZub = new int[3][];
arrayZub[0] = new int[3];
arrayZub[1] = new int[6];
arrayZub[2] = new int[9];
for (int i = 0; i <2; i++)
{
    FillArrayWithRandomNumbers(arrayZub[i], random);
}
static double FindAverage(int[][] arrayZub)
{
    double sumNumbers = 0;
    double sumLength = 0;
    for (int i = 0; i < arrayZub.Length; i++)
    {     
        for (int j = 0; j < arrayZub[i].Length; j++)
        {
            sumNumbers += arrayZub[i][j];
            sumLength++;  
        }  
    }
    return sumNumbers / sumLength;
}
Console.WriteLine("Среднее арифметическое всех элементов массивов arrayZub: " + FindAverage(arrayZub));

//Реализовать алгоритм составления массива первых n чисел последовательности Фибоначчи
Console.WriteLine("Числа Фибоначчи: " + string.Join(" ", FindFibonachi(11)));
static int[] FindFibonachi(int n)
{
    int[] array = new int[n];
    if (n>=2)
    {
        array[0] = 0;
        array[1] = 1;
    }
    for (int i = 2; i < n; i++) 
    {
        array[i] = array[i - 1] + array[i-2];
    }
    return array;
}

