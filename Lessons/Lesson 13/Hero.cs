namespace Lesson_13
{
    public class Hero
    {
        public string name { get; set; }
        public int age { get; set; }
        public string secretIdentity { get; set; }
        public string[] powers { get; set; }
        public Hero() { }
        public Hero(string name, int age, string secretIdentity, string[] powers)
        {
            this.name = name;
            this.age = age;
            this.secretIdentity = secretIdentity;
            this.powers = powers;
        }
    }
}