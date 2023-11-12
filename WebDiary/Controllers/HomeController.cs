using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using WebDiary.Models;
using Microsoft.Extensions.Configuration;

namespace WebDiary.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
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

        private EventViewModel[] GenerateEvents()
        {
            var events = new List<EventViewModel>();
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("connectionString"));
            conn.Open();
            using (SqlCommand cmd = new SqlCommand(@"select id,
                                                            title,
                                                            start,
                                                            [end],
                                                            description,
                                                            allDay,
                                                            url,
                                                            backgroundColor
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
                            description = Convert.ToString(dr["description"]), //shouldn't be null or tooltip error
                            allDay = Convert.ToBoolean(dr["allDay"]), //
                            url = Convert.ToString(dr["url"]),
                            backgroundColor = Convert.ToString(dr["backgroundColor"])
                            //extendedProps = Convert.ToString(dr["extendedProps"])
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
            SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("connectionString"));
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            String sql = "insert into [Events] values (@id, @title, @start, @end, @description, @allDay, @url, @backgroundColor)";
            //(id, title, start, [end], description, allDay, url, backgroundColor) 
            SqlCommand command = new SqlCommand(sql,conn);
            Guid id = Guid.NewGuid();
            command.Parameters.Add("@id", SqlDbType.UniqueIdentifier).Value = id; // eventViewModel.id;
            command.Parameters.Add("@title", SqlDbType.VarChar).Value = eventViewModel.title;
            command.Parameters.Add("@start", SqlDbType.DateTime).Value = eventViewModel.start;
            command.Parameters.Add("@end", SqlDbType.DateTime).Value = eventViewModel.end;
            command.Parameters.Add("@description", SqlDbType.VarChar).Value = String.IsNullOrWhiteSpace(eventViewModel.description) ? DBNull.Value : eventViewModel.description;//eventViewModel.description;
            command.Parameters.Add("@allDay", SqlDbType.Bit).Value = Convert.ToBoolean(eventViewModel.allDay);
            command.Parameters.Add("@url", SqlDbType.VarChar).Value = String.IsNullOrWhiteSpace(eventViewModel.url) ? DBNull.Value : eventViewModel.url; 
            command.Parameters.Add("@backgroundColor", SqlDbType.VarChar).Value = Convert.ToString(eventViewModel.backgroundColor);
            conn.Open();
            command.ExecuteNonQuery();
		    conn.Close();
            return RedirectToAction("Index");
        }

    }
}