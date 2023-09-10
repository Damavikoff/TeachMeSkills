using System.Collections;

namespace Lesson_11
{
    internal class MyStack<T> : IEnumerable<T>
    {
        private readonly T[] stack;
        private readonly int size;
        private const int defaultSize = 10;
        private int count;
        public int Count
        {
            get { return count; }
        }
        public MyStack()
        {
            stack = new T[defaultSize];
        }
        public MyStack(int size)
        {
            this.size = size;
            stack = new T[size];
        }
        public void Push(T item)
        {
            if (IsFull()) throw new InvalidOperationException("Стек полон"); 
            stack[count++] = item;
        }
        public T Peek() 
        {
            if (IsEmpty()) throw new InvalidOperationException("Стек пуст");
            return stack[count-1];
        }
        public T Pop()
        {
            if (IsEmpty()) throw new InvalidOperationException("Стек пуст");
            T item = stack[--count];
            stack[count] = default;
            return item;
        }
        public bool IsEmpty()
        {
            return count == 0;
        }
        public bool IsFull()
        {
            return count == stack.Length;
        }
        public IEnumerator<T> GetEnumerator()
        {
            if (IsEmpty()) throw new InvalidOperationException("Стек пуст");
            for (int i = count; i > 0; i--)
                yield return stack[i - 1];
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}