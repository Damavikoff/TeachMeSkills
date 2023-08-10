Random random = new Random();

//Реализовать алгоритм нахождения максимального элемента в одномерном массиве целых чисел (интов). Массив передается как параметр, на выходе int.
int[] arrayMax = new int[10];
for (int i = 0; i < arrayMax.Length; i++)
{
    int randomValue = random.Next(1, 20);
    arrayMax[i] = randomValue;
}
Console.WriteLine("\n" + "Максимальное число массива arrayMax: " + findMax(arrayMax));

//Реализовать алгоритм нахождения среднего арифметического всех элементов  зубчатого массива. Массив передается как параметр, на выходе double.
int[][] arrayZub = new int[3][];
arrayZub[0] = new int[3];
arrayZub[1] = new int[6];
arrayZub[2] = new int[9];
for (int i = 0; i < arrayZub.Length; i++) 
{
    for (int j = 0; j < arrayZub[i].Length; j++) 
    {
        int randomValue = random.Next(1, 20);
        arrayZub[i][j] = randomValue;
    }
}
Console.WriteLine("Среднее арифметическое всех элементов массивов arrayZub: " + findAverage(arrayZub));

//Реализовать алгоритм составления массива первых n чисел последовательности Фибоначчи
Console.WriteLine("Числа Фибоначчи: " + string.Join(" ", findFibonachi(11)));

static int findMax (int[] arrayMax)
{
    Console.Write("Элементы массива arrayMax: ");
    for (int i = 0; i < arrayMax.Length; i++) 
    {
        Console.Write(arrayMax[i] + " ");
    }
    return arrayMax.Max();
}
static double findAverage(int[][] arrayZub)
{
    double sumNumbers = 0;
    double sumLength = 0;
    Console.Write("Элементы массива arrayZub: ");
    for (int i = 0; i < arrayZub.Length; i++)
    {     
        for (int j = 0; j < arrayZub[i].Length; j++)
        {
            sumNumbers += arrayZub[i][j];
            sumLength++;  
            Console.Write(arrayZub[i][j] + " ");
        }  
    }
    Console.WriteLine("\n" + "Сумма всех элементов массивов arrayZub: " + sumNumbers + ", количество всех элементов: " + sumLength);
    return sumNumbers / sumLength;
}
static int[] findFibonachi(int n)
{
    int fibonachi = 0;
    int temp = 0;
    int fn = 1;
    //fn = fn-1 + fn-2

    int[] arrayFib = new int[n];
    for (int i = 0; i < n; i++)
    {
        temp = fibonachi;//0 1 1 2 3
        fibonachi = fn;  //1 1 2 3 5
        fn += temp;      //1 2 3 5 
        arrayFib[i] = fibonachi;
    }
    return arrayFib;
}

