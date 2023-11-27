using Lesson26.Models;

namespace Lesson26.Services
{
    public class SparjaService : ISparjaService
    {
        SparjaContext _sparjaContext;
        public SparjaService(SparjaContext sparjaContext)
        {
            _sparjaContext = sparjaContext;
        }
        public void AddSparja(Sparja sparja)
        {
            _sparjaContext.Sparjas.Add(sparja);
            _sparjaContext.SaveChanges();
        }
        public dynamic GetSparjas()
        {
            var groupedResult = _sparjaContext.Sparjas.GroupBy(x => new { x.Email, x.Name })
             .Select(g => new 
              {
                 name = g.Key.Name,
                 count = g.Count(),
                 date = g.Max(x => x.PostedAt)
              })
             .OrderBy(c => c.name);

            return groupedResult;
        }
    }
}