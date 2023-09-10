namespace Lesson_12
{
    internal class WrongPasswordException : Exception
    {
        public WrongPasswordException() { }
        public WrongPasswordException(string message)
        : base(message) { }
    }
}