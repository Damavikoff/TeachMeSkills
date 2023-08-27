using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class ConsoleLogger : ILogger
    {
        string path;
        public void Log(string message)
        {
            Console.Write(message + Environment.NewLine);
        }
    }
}