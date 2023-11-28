namespace AnimalLibrary
{
    public class Cat : Animal
    {
        private string sound = "Meow";
        private string action = "meows";//"rubs up against";
        public override string Sound { get { return sound; } }
        public override string Action { get { return action; } }
        public virtual void AddAge(int age)
        {
            Age += age;
        }
    }
}
