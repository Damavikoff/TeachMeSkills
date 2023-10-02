namespace Diary
{
    internal class Record
    {
        PriorityList priorityList;
        StatusList statusList;
        private string priority;
        private string status;
        private string name;
        public string Name 
        {   
            get
            {
                return name;
            }
            set
            {
                if (_uniqueNames2.Contains(value))
                {
                    throw new ArgumentException($"Имя не уникально");
                }
                else
                {
                    name = value;
                }
            }
        }
        public string Text { get; set; }
        public string Deadline { get; set; }
        public DateTime CreationDate { get; set; }

        public static List<string> _uniqueNames2 = new List<string>();
        public string Priority
        { 
            get
            {
                return priority;
            }
            set
            {
                if (Enum.TryParse(value, out priorityList))
                    priority = value;
                else
                    throw new ArgumentException($"Возможные значения {nameof(Priority)}: High, Normal, Low");
            }
        }
        public string Status
        {
            get
            {
                return status;
            }
            set
            {
                if (Enum.TryParse(value, out statusList))
                    status = value;
                else
                    throw new ArgumentException($"Возможные значения {nameof(Status)}: Todo, Inprogress, Done");
            }
        }
        public Record(string name, string text, string deadline, string priority, string status)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException(nameof(text));

            Name = name;
            Text = text;
            Deadline = deadline;
            Priority = priority;
            CreationDate = DateTime.Now;
            Status = status;
            _uniqueNames2.Add(name);
        }
        public string GetPropertyByKey(string key)
        {
            string unexpectedKey = "Wrong key";
            switch (key)
            {
                case "Priority":
                    return Priority;
                case "Status":
                    return Status;
                case "Deadline":
                    return Deadline;
                default:
                    return unexpectedKey;
            }
        }  
    }
}