namespace Logger
{
    public class FileLogger : ILogger
    {
        private readonly string path;
        public FileLogger(string pathFile)
        {
            path = pathFile ?? throw new ArgumentNullException(nameof(pathFile));
        }
        public void Log(string message)
        {
            File.AppendAllText(path, message + Environment.NewLine);
        }
    }
}