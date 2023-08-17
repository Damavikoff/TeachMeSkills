//Реализовать алгоритм сортировки пузырьком.
Random random = new Random();

int[] arrayBubble = new int[10];
Console.Write("Числа массива arrayBubble: ");
for (int i = 0; i < arrayBubble.Length; i++)
{
    int randomValue = random.Next(1, 500);
    arrayBubble[i] = randomValue;
    Console.Write(arrayBubble[i] + " ");
}
Console.WriteLine("\n" + "Отсортированный массив: " + string.Join(" ", BubbleSort(arrayBubble)));
static int[] BubbleSort(int[] array)
{
    for (int i = 1; i < array.Length; i++)
        for (int j = 0; j < array.Length - i; j++)
        {
            if (array[j] > array[j + 1])
            {
                int temp = array[j];
                array[j] = array[j + 1];
                array[j + 1] = temp;
            }
        }
    return array;
}