// 2. Самостоятельно реализовать систему типов для “строительства дома”.
// Реализовать систему типов как отдельную библиотеку.
// В консольном приложении продемонстрировать работу.
// Предусмотреть возможность строительства разных “типовых” домов.
using Build;

var home = new House();
home.BuildFoundament();
home.BuildFoundament();

var kitchen = new Cooking();
kitchen.BuildFoundament();
kitchen.BuildWalls();
kitchen.BuildFloor();
kitchen.BuildRoof();
kitchen.BuildRoof();