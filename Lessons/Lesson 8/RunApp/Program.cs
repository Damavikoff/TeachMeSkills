using ConsoleCalculator;
using Logger;

class Program
{
    static void Main(string[] args)
    {
        Calculator calculator = new Calculator(5, 6);

        var loggerConnect = new LoggerConnect(new Loggers(new ConsoleLogger(), new FileLogger("text.txt")), calculator);
        loggerConnect.Calculate();
    }
}