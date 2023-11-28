namespace Build
{
    public class House : Building //не интерфейс, а класс?
    {
        bool isFoundament;
        bool isWalls;
        bool isRoof;
        bool isFloor;
        public override void BuildFoundament()
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
        public override void BuildWalls()
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
        public override void BuildRoof()
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
        public override void BuildFloor()
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