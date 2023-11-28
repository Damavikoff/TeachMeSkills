namespace AnimalLibrary
{
    public class Dog : Animal
    {
        private string sound = "Woof";
        private string action = "barks";
        public override string Sound { get { return sound; } }
        public override string Action { get { return action; } }
    }
}
