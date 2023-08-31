using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Build
{
    public class Education : IBuilding
    {
        bool isFoundament;
        bool isWalls;
        bool isRoof;
        bool isFloor;
        public void BuildFoundament()
        {
            if (isFoundament)
            {
                Console.WriteLine("Фундамент из бетона уже построен");
            }
            else
            {
                Console.WriteLine("Фундамент из бетона построен");
                isFoundament = true;
            }             
        }
        public void BuildWalls()
        {
            if (isWalls)
            {
                Console.WriteLine("Стены из бетона уже построены");
            }
            else
            {
                Console.WriteLine("Стены из бетона построены");
                isWalls = true;
            }
        }
        public void BuildRoof()
        {
            if (isRoof)
            {
                Console.WriteLine("Крыша из профлиста уже построена");
            }
            else
            {
                Console.WriteLine("Крыша из профлиста построена");
                isRoof = true;
            }    
        }
        public void BuildFloor()
        {
            if (isFloor)
            {
                Console.WriteLine("Пол из досок уже построен");
            }
            {
                Console.WriteLine("Пол из досок построен");
                isFloor = true;
            }
        }
    }
}