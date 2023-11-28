namespace Lesson_13
{
    public class Squad
    {
        public string squadName { get; set; }
        public string homeTown { get; set; }
        public int formed { get; set; }
        public string secretBase { get; set; }
        public bool active { get; set; }
        public List<Hero> members { get; set; }
        public Squad() { }
        public Squad(string squadName, string homeTown, int formed, string secretBase, bool active, List<Hero> members)
        {
            this.squadName = squadName;
            this.homeTown = homeTown;
            this.formed = formed;
            this.secretBase = secretBase;
            this.active = active;
            this.members = members;
        }
    }
}