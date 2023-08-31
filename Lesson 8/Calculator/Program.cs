// 1. Самостоятельно реализовать систему типов для лога сообщений.
// Реализовать систему типов как отдельную библиотеку.
// В консольном приложении реализовать класс Calculator, который выполняет простые математические операции, в нем показать пример использования логера.
// Предусмотреть возможность записи лога как в файл, так и на консоль.
using Logger;
using ConsoleCalculator;

var operationOne = new Calculator(2, 3, new FileLogger("text.txt"));
operationOne.Sum();
var operationTwo = new Calculator(7, 3, new ConsoleLogger());
operationTwo.Div();
var operationThree = new Calculator(11, 3, new Loggers(new FileLogger("text.txt"), new ConsoleLogger()));
operationThree.Sub();