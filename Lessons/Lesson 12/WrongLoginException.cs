namespace Lesson_12
{
    internal class WrongLoginException : Exception
    {
        public WrongLoginException() { }
        public WrongLoginException(string message)
        : base(message) { }
    }
}