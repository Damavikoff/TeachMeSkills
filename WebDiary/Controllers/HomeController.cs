using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using WebDiary.Models;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View(new EventViewModel());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            return Json(GenerateEvents());
        }

        private EventViewModel[] GenerateEvents()//(DateTime start, DateTime end, int startId)
        {
            string connectionString = "Data Source=localhost; Integrated Security=False; user id=sa;password=123qwe; Initial Catalog=WebDiary; TrustServerCertificate=True";

            var events = new List<EventViewModel>();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(@"select id,
                                                            title,
                                                            start,
                                                            [end],
                                                            description,
                                                            allDay,
                                                            url,
                                                            backgroundColor,
                                                            extendedProps
                                                     from [Events]", conn)
            {
                CommandType = CommandType.Text
            })

            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        events.Add(new EventViewModel() //id, title, start, end, allDay must be not null
                        {
                            id = (Guid)dr["id"],
                            title = Convert.ToString(dr["title"]),
                            start = Convert.ToDateTime(dr["start"]),
                            end = Convert.ToDateTime(dr["end"]),
                            description = Convert.ToString(dr["description"]),
                            allDay = Convert.ToBoolean(dr["allDay"]),
                            url = Convert.ToString(dr["url"]),
                            backgroundColor = Convert.ToString(dr["backgroundColor"]),
                            extendedProps = Convert.ToString(dr["extendedProps"])
                        });
                    }
                }

            }
            conn.Close();
            return events.ToArray();
        }
        [HttpPost]
        public IActionResult AddEvent(EventViewModel eventViewModel)
        {
            string connectionString = "Data Source=localhost; Integrated Security=False; user id=sa;password=123qwe; Initial Catalog=WebDiary; TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(connectionString);
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "insert into [Events] (id, title, start, [end], allDay) values (@id, @title, @start, @end, @allDay)";

            SqlCommand command = new SqlCommand(sql,conn);
            Guid id = Guid.NewGuid();
            command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id; // eventViewModel.id;
            command.Parameters.Add("@title", SqlDbType.VarChar).Value = eventViewModel.title;
            command.Parameters.Add("@start", SqlDbType.DateTime).Value = Convert.ToDateTime("2023-10-08");
            command.Parameters.Add("@end", SqlDbType.DateTime).Value = Convert.ToDateTime("2023-10-09");
            command.Parameters.Add("@allDay", SqlDbType.Bit).Value = 1;
            conn.Open();
            command.ExecuteNonQuery();

            //command.Dispose();
		conn.Close();
            return RedirectToAction("Index");
        }

    }
}