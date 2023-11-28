using Lesson_13;
using System.Xml.Serialization;
using System.Text.Json;

Squad squad = new Squad();

DeserealizeFile(Console.ReadLine(), ref squad); //@"C:\Users\kyrgyzstan\source\repos\TeachMeSkills"
SerializeFile(squad);
void DeserealizeFile(string jsonPath, ref Squad squad)
{
    string json = String.Empty;
    string[] files = new DirectoryInfo(jsonPath).GetFiles("*.json", SearchOption.TopDirectoryOnly)
   .Select(f => f.FullName).ToArray();

    if (files.Length == 0)
        throw new Exception("Folder is empty");

    foreach (string file in files)
    {
        try
        {
            using (StreamReader reader = new StreamReader(file))
            {
                json = reader.ReadToEnd();
                squad = JsonSerializer.Deserialize<Squad>(json);
                break;
            }
        }
        catch (Exception ex)
        {
            string exception = ex.ToString();
        }
    }
}
void SerializeFile(Squad squad)
{
    var fileName = squad.squadName;
    Type[] types = new Type[2] { typeof(Squad), typeof(Hero) };
    XmlSerializer[] serializers = new XmlSerializer[2];
    serializers = XmlSerializer.FromTypes(types);

    using (StreamWriter writer = new StreamWriter($"{fileName}.xml"))
    {
        serializers[0].Serialize(writer, squad);
        writer.Close();
    }
}