namespace Build
{
    public class Cooking : Building
    {
        bool isFoundament;
        bool isWalls;
        bool isRoof;
        bool isFloor;
        public override void BuildFoundament()
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
        public override void BuildWalls()
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
        public override void BuildRoof()
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
        public override void BuildFloor()
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