using ConsoleCalculator;

namespace Logger
{
    public class LoggerConnect
    {
        private readonly ILogger _logger;
        private readonly Calculator _calculator;
        private int _i = 0;

        public LoggerConnect(ILogger logger, Calculator calculator)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _calculator = calculator;
        }
        public void Calculate()
        {
            _logger.Log($"Сумма: {_calculator.Sum()}");
            _logger.Log($"Деление: {_calculator.Div()}");
            _logger.Log($"Вычитание: {_calculator.Sub()}");
            _logger.Log($"Умножение: {_calculator.Mul()}");
        }
    }
}
