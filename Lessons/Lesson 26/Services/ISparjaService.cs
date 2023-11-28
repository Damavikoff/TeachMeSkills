using Lesson26.Models;

namespace Lesson26.Services
{
    public interface ISparjaService
    {
        void AddSparja(Sparja sparja);
        dynamic GetSparjas();
    }
}