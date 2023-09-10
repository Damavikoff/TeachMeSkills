namespace Lesson_12
{
    internal class AuthClass
    {
        private string login;
        private string password;
        private string confirmPassword;
        public string Login 
        { 
            get => login; 
            set
            {
                if (value.Length > 20 || value.Contains(" "))
                    throw new WrongLoginException("Логин должен быть меньше 20 символов и не содержать пробелы");
                else 
                    login = value;
            }
        }
        public string Password
        {
            get => password;
            set
            {
                if (value.Length > 20 || value.Contains(" ") || (!IsNumberContains(value)))
                    throw new WrongPasswordException("Пароль должен быть меньше 20 символов, не содержать пробелы, и иметь хотя бы одну цифру");
                else
                    password = value;
            }
        }
        public string ConfirmPassword 
        { 
            get => confirmPassword; 
            set
            {
                if (value != password)
                    throw new WrongPasswordException("Пароль не идентичен");
                else
                    confirmPassword = value;
            } 
        }
        public static bool AuthMethod(string login, string password, string confirmPassword)
        {
            try
            {
                AuthClass auth = new AuthClass { Login = login, Password = password, ConfirmPassword = confirmPassword };
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        static bool IsNumberContains(string input)
        {
            foreach (char c in input)
                if (char.IsNumber(c))
                    return true;
            return false;
        }
    }
}