using Lesson_11;

MyStack<int> stack = new MyStack<int>(10);
//stack.Peek();
//stack.Pop();
stack.IsEmpty();
stack.IsFull();
stack.Push(5);
stack.Push(6);
stack.Peek();
stack.Pop();
stack.Push(7);
stack.Push(8);
stack.Push(9);
stack.IsEmpty();
stack.IsFull();
stack.Push(1);
stack.Push(2);
stack.Push(3);
stack.Push(4);
stack.Push(1);
stack.Push(1);
//stack.Push(1);
foreach (int i in stack)
{
    Console.WriteLine(i);
}
Console.WriteLine(stack.Count);