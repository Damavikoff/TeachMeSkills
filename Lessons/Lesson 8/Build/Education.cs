namespace Build
{
    public class Education : Building
    {
        bool isFoundament;
        bool isWalls;
        bool isRoof;
        bool isFloor;
        public override void BuildFoundament()
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
        public override void BuildWalls()
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
        public override void BuildRoof()
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
        public override void BuildFloor()
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