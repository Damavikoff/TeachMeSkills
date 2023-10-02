using System.Globalization;
using System.Text.Json;

namespace Diary
{
    internal class Notepad
    {
        private static List<Record> _records = new List<Record>();
        private static List<Record> _responseBuffer = new List<Record>();

        public List<Record> AddRecord(string userInput)
        {
            var splittedUserInput = FormatUserInput(userInput);

            foreach (var record in splittedUserInput)
            {
                _records.Add(new Record(record.Key, record.Value[0], record.Value[1], record.Value[2], record.Value[3]));
                Record._uniqueNames2.Add(record.Key);
            }
            return _records;
        }
        public void LoadRecords() 
        {
            string json = String.Empty;
            using (StreamReader reader = new StreamReader(@"data.json")) 
            {
                json = reader.ReadToEnd(); 
            }
            if (json != String.Empty)
            {
                _records = JsonSerializer.Deserialize<List<Record>>(json);
                
            }
            else
            {
                throw new Exception("Json файл пуст");
            }
        }
        public string SaveRecords() 
        {
            const string _errorMessage = "Изменений нет";
            const string _successMessage = "Все изменения сохранены";
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string json = JsonSerializer.Serialize(_records, options);
            if (json != null)
            {
                using (StreamWriter writer = new StreamWriter(@"data.json", false))
                {
                    writer.Write(json);
                    return _successMessage;
                }
            }
            else
            {
                return _errorMessage;
            }
        }
        public List<Record> DeleteRecord(string userInput)
        {
            var splittedUserInput = FormatUserInput(userInput);
            foreach (var record in splittedUserInput)
            {
                if (Record._uniqueNames2.Contains(record.Key))
                {
                    _records.RemoveAll(x => x.Name == record.Key);
                    Record._uniqueNames2.Remove(record.Key);
                }
                else
                    throw new ArgumentException("Задачи с таким названием не существует");
            }
            return _records;
        }
        public List<Record> ChangeRecordStatus(string userInput)
        {
            var splittedUserInput = FormatUserInput(userInput);

            foreach (var record in splittedUserInput)
            {
                if (Record._uniqueNames2.Contains(record.Key))
                {
                    _records.Where(x => x.Name == record.Key).Select(x => { x.Status = record.Value[0]; return x; }).ToList();
                }
                else
                    throw new ArgumentException("Задачи с таким названием не существует");
            }
            return _records;
        }
        public List<Record> ChangeRecordPriority(string userInput)
        {
            const string succesMessage = "success";
            var splittedUserInput = FormatUserInput(userInput);

            foreach (var record in splittedUserInput)
            {
                if (Record._uniqueNames2.Contains(record.Key))
                        _records.Where(x => x.Name == record.Key).Select(x => { x.Priority = record.Value[0]; return x; }).ToList();
                else
                    throw new ArgumentException("Задачи с таким названием не существует");
            }
            return _records;
        }
        public List<Record> ChangeRecordDeadline(string userInput)
        {
            var splittedUserInput = FormatUserInput(userInput);
            foreach (var record in splittedUserInput) 
            {
                if (Record._uniqueNames2.Contains(record.Key))
                    _records.Where(x => x.Name == record.Key).Select(x => { x.Deadline = record.Value[0]; return x; }).ToList();
                else
                    throw new ArgumentException("Задачи с таким названием не существует");
            }
            return _records;
        }
        public List<Record> ShowFormattedRecords(string userInput) 
        {
            _responseBuffer.Clear();
            if (userInput != null) //try catch
            {
                var splittedUserInput = FormatUserInput(userInput);

                foreach (var s in splittedUserInput)
                {
                    var keyValues = splittedUserInput[s.Key]; 
                    var key = s.Key;
                    switch (key)
                    {
                        case "Deadline":
                        case "Status":
                        case "Priority":
                            FilterRequest(key, keyValues);
                            break;
                        case "Sort":
                            SortRequest(keyValues);
                            break;
                        default:
                            return _records;
                    }
                }
            }
            return _responseBuffer.ToList();
        }
        private IDictionary<string, List<string>> FormatUserInput(string userInput)
        {
            // title casing converts the first character of a word to uppercase and the rest of the characters to lowercase.
            // However, this method does not currently provide proper casing to convert a word that is entirely uppercase, such as an acronym
            userInput = userInput.ToLower();
            TextInfo ti = CultureInfo.CurrentCulture.TextInfo;
            userInput = ti.ToTitleCase(userInput);

            var splittedUserInput = userInput.Split(';')
                                            .Select(part => part.Split('=', ',')
                                            .Select(token => token.Trim('"')))
                                            .ToDictionary(tokens => tokens.First(),
                                            tokens => tokens.Skip(1).ToList());
            
            return splittedUserInput;   
        }
        private void FilterRequest(string key, List<string> list)
        {
            if (_responseBuffer.Count == 0)
            {
                for (int i = 0; i < list.Count; i++)
                    _responseBuffer = _records.Where(x => x.GetPropertyByKey(key) == list[i]).ToList();
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    _responseBuffer = _responseBuffer.Where(x => x.GetPropertyByKey(key) == list[i]).ToList();
            }
        }
        private void SortRequest(List<string> list)
        {
            if (_responseBuffer.Count == 0)
            {
                for (int i = 0; i < list.Count; i++)
                    _responseBuffer = _records.OrderBy(x => x.GetPropertyByKey(list[i])).ToList();
            }
            else
            {
                for (int i = 0; i < list.Count; i++)
                    _responseBuffer = _responseBuffer.OrderBy(x => x.GetPropertyByKey(list[i])).ToList();
            }
        }
    }
}