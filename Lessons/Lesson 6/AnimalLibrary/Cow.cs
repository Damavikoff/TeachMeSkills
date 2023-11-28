namespace AnimalLibrary
{
    public class Cow : Animal
    {
        private string sound = "Moo";
        private string action = "mooing";//"look";
        public override string Sound { get { return sound; } }
        public override string Action { get { return action; } }
    }
}
