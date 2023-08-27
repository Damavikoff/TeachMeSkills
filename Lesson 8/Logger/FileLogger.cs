using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class FileLogger : ILogger
    {
        string path;
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