namespace Logger
{
    public class Loggers : ILogger
    {
        private readonly ILogger[] _loggers;
        public Loggers(params ILogger[] loggers)
        {
            _loggers = loggers;
        }
        public void Log(string message)
        {
            foreach (var logger in _loggers)
            {
                logger.Log(message);
            }
        }
    }
}