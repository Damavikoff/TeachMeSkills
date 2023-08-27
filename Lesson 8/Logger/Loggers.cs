using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public class Loggers : ILogger
    {
        private readonly ILogger[] _loggers;
        //params 
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