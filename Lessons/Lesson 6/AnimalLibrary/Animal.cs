using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
namespace AnimalLibrary
{
    public abstract class Animal
    {
        public void MakeSound()
        {
            Console.WriteLine($"{Name} the {GetType().Name} make {Sound}");
        }
        public void MakeAction(Animal class2)
        {
            Console.WriteLine($"{Name} the {GetType().Name} {Action} at the {class2.Name} the {class2.GetType().Name}");
        }
        public void Print() => Console.WriteLine($"Имя: {Name} Возраст: {Age} Цвет: {Colour} Вид: {GetType().Name}");
        public string Name { get; set; }
        public int Age { get; set; }
        public string Colour { get; set; }
        public abstract string Sound { get; }
        public abstract string Action { get; }
    }
}
