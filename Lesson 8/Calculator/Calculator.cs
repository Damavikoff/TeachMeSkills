using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logger;

namespace ConsoleCalculator
{
    internal class Calculator
    {
        private readonly ILogger _logger;
        public int A { get; set; }
        public int B { get; set; }
        public Calculator(int a, int b, ILogger logger)
        {
            A = a;
            B = b;
            _logger = logger ?? throw new ArgumentNullException();
        }
        public void Sum() { _logger.Log($"Исходное число А: {A} Исходное число B: {B} Ответ: {A += B}"); }
        public void Div() { _logger.Log($"Исходное число А: {A} Исходное число B: {B} Ответ: {A /= B}"); }
        public void Sub() { _logger.Log($"Исходное число А: {A} Исходное число B: {B} Ответ: {A -= B}"); }
        public void Mul() { _logger.Log($"Исходное число А: {A} Исходное число B: {B} Ответ: {A *= B}"); }
    }
}