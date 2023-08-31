namespace AnimalLibrary
{
    public class Kitten : Cat
    {
        private string sound = "Purr";
        private string action = "looks";       
        public override string Sound { get { return sound; } }
        public override string Action { get { return action; } }
        public override void AddAge(int age)
        {
            base.AddAge(age);
            Console.WriteLine($"{Name} the {GetType().Name} now {Age} years old");
        }
    }
}
