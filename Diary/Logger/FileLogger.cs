namespace Diary.Logger
{
    internal class FileLogger : ILogger
    {
        private readonly string _path;
        public FileLogger(string path)
        {
            _path=path??throw new ArgumentNullException(nameof(path));
        }
        public void Log(string message) 
        {
            string time = "[" + DateTime.Now + "]:";
            message = message.Insert(0, time);
            File.AppendAllText(_path, message + Environment.NewLine);
        }
    }
}