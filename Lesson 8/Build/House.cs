using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build
{
    public class House : IBuilding //не интерфейс, а класс?
    {
        bool isFoundament;
        bool isWalls;
        bool isRoof;
        bool isFloor;
        public void BuildFoundament()
        {
            if (isFoundament)
            {
                Console.WriteLine("Фундамент из кирпича уже построен");
            }
            else
            {
                Console.WriteLine("Фундамент из кирпича построен");
                isFoundament = true;
            }
        }
        public void BuildWalls()
        {
            if (isWalls)
            {
                Console.WriteLine("Стены из брусьев уже построены");
            }
            else
            {
                Console.WriteLine("Стены из брусьев построены");
                isWalls = true;
            }
        }
        public void BuildRoof()
        {
            if (isRoof)
            {
                Console.WriteLine("Крыша из газобетона уже построена");
            }
            else
            {
                Console.WriteLine("Крыша из газобетона построена");
                isRoof = true;
            }
        }
        public void BuildFloor()
        {
            if (isFloor)
            {
                Console.WriteLine("Пол из ламината уже построен");
            }
            else
            {
                Console.WriteLine("Пол из ламината построен");
                isFloor = true;
            }
        }
    }
}