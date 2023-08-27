using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build
{
    public class Cooking : IBuilding
    {
        bool isFoundament;
        bool isWalls;
        bool isRoof;
        bool isFloor;
        public void BuildFoundament()
        {
            if (isFoundament) 
            { 
                Console.WriteLine("Фундамент из железобетона уже построен"); 
            }
            else 
            { 
                Console.WriteLine("Фундамент из железобетона построен");
                isFoundament = true;
            }      
        }
        public void BuildWalls()
        {
            if (isWalls)
            {
                Console.WriteLine("Стены из керамического блока уже построены");
            }
            else
            {
                Console.WriteLine("Стены из керамического блока построены");
                isWalls = true;
            }
        }
        public void BuildRoof()
        {
            if (isRoof)
            {
                Console.WriteLine("Крыша из бетона уже построена");
            }
            else
            {
                Console.WriteLine("Крыша из бетона построена");
                isRoof = true;
            }  
        }
        public void BuildFloor()
        {
            if (isFloor)
            {
                Console.WriteLine("Пол из кирпича уже построен");
            }
            else
            {
                Console.WriteLine("Пол из кирпича построен");
                isFloor = true;
            }
        }
    }
}