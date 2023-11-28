namespace AnimalLibrary
{
    public class Bird : Animal
    {
        private string sound = "Tweet";
        private string action = "tweets";//"flies";  
        public override string Sound { get { return sound; } }
        public override string Action { get { return action; } }
    }
}
