using Diary;
using Diary.Logger;

Notepad notepad = new Notepad();
try
{
    const string loadingMessage = "Записи загружены успешно";
    notepad.LoadRecords();
    Console.WriteLine(loadingMessage);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}
var fileLogger = new FileLogger("text.txt");

notepad.SaveRecords();

ShowHelp();
void PrintItems(List<Record> list)
{
    string result = "";
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("{0,10} |{1,20} |{2,25} |{3,15} |{4,10}", "Priority", "Name", "Text", "Deadline", "Status");
    Console.ForegroundColor = ConsoleColor.White;
    foreach (var item in list)
    {
        result += String.Join(" ", item.Priority, item.Name, item.Text, item.Deadline, item.Status + Environment.NewLine);
        Console.WriteLine("{0,10} |{1,20} |{2,25} |{3,15} |{4,10}", item.Priority, item.Name, item.Text, item.Deadline, item.Status);
    }
    fileLogger.Log(result);
}

Record _record;
string _string;
string userInput;
UserMenu action;
do
{
    char firstChar;
    fileLogger.Log(userInput = Console.ReadLine());
    if (userInput == "")
    {
        firstChar = '0';
    }
    else
    {
        firstChar = userInput[0];
        userInput = userInput.Trim(firstChar, ' ');
    }    
    
    action = GetAction(firstChar.ToString());
    ExecuteAction(action);
}
while (action != UserMenu.Exit);
return 0;

static UserMenu GetAction(string input) =>
           int.TryParse(input, out var actionNumber)
           ? Enum.IsDefined((UserMenu)actionNumber) ? (UserMenu)actionNumber : UserMenu.Exit
           : UserMenu.Exit;

void ExecuteAction(UserMenu action)
{
    switch (action)
    {
        case UserMenu.Exit:
            Console.WriteLine(notepad.SaveRecords());
            break;

        case UserMenu.ShowAllRecords:
            try
            {
                PrintItems(notepad.ShowFormattedRecords(userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileLogger.Log(ex.Message);
            }
            break;

        case UserMenu.ChangeDeadline:
            try
            {
                PrintItems(notepad.ChangeRecordDeadline(userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileLogger.Log(ex.Message);
            }
            break;

        case UserMenu.ChangeStatus:
            try
            {
                PrintItems(notepad.ChangeRecordStatus(userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileLogger.Log(ex.Message);
            }
            break;

        case UserMenu.ChangePriority:
            try
            {
                PrintItems(notepad.ChangeRecordPriority(userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileLogger.Log(ex.Message);
            }
            break;

        case UserMenu.CreateNewRecord:
            try
            {
                PrintItems(notepad.AddRecord(userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileLogger.Log(ex.Message);
            }
            break;

        case UserMenu.DeleteRecord:
            try
            {
                PrintItems(notepad.DeleteRecord(userInput));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                fileLogger.Log(ex.Message);
            }
            break;

        case UserMenu.ShowHelp:
            ShowHelp(); 
            break;
    };
}

static string GetMessageByAction(UserMenu action) => action switch
{
    UserMenu.Exit => $"{(int)UserMenu.Exit} - Выход с сохранением",
    UserMenu.ShowAllRecords => $"{(int)UserMenu.ShowAllRecords} - Отображение всех записей с возможностью фильтрации и сортировки" + Environment.NewLine +
                                  "Пример: 1 \"Priority\"=\"High\",\"Normal\";\"Status\"=\"Inprogress\";\"Sort\"=\"Status\",\"Deadline\"" + Environment.NewLine +
                                  "Параметры не обязательны",
    UserMenu.ChangeDeadline => $"{(int)UserMenu.ChangeDeadline} - Изменение время выполнения записи по имени" + Environment.NewLine +
                                  "Пример: 2 \"School Test\"=\"12:30 19.08.2023\"",
    UserMenu.ChangeStatus => $"{(int)UserMenu.ChangeStatus} - Изменение статуса записи по имени" + Environment.NewLine +
                                  "Пример: 3 \"Call Viktor\"=\"Done\"",
    UserMenu.ChangePriority => $"{(int)UserMenu.ChangePriority} - Изменение приоритета записи по имени" + Environment.NewLine +
                                  "Пример: 4 \"School Test\"=\"High\"",
    UserMenu.CreateNewRecord => $"{(int)UserMenu.CreateNewRecord} - Создание новой записи" + Environment.NewLine +
                                  "Пример: 5 \"Doctor\",\"Need to go to a doctor\",\"15:00 12.12.2023\",\"Normal\",\"Todo\"",
    UserMenu.DeleteRecord => $"{(int)UserMenu.DeleteRecord} - Удаление записи по имени" + Environment.NewLine +
                                  "Пример: 6 \"Doctor\"",
    UserMenu.ShowHelp => $"{(int)UserMenu.ShowHelp} - Отображение помощи",
    _ => string.Empty
};
static void ShowHelp()
{
    const string helpMessage = "Выберите действие (выход с сохранением - любой символ):";

    Console.WriteLine(helpMessage);
    var allActions = Enum.GetValues<UserMenu>();
    foreach (var action in allActions)
    {
        Console.WriteLine(GetMessageByAction(action));
    }
}