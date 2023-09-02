namespace Logger
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.Write(message + Environment.NewLine);
        }
    }
}