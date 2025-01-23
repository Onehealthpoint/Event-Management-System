using Event_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers
{
    public class CalenderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents()
        {
            var events = EventSystemModel.GetAll();
            var jsonEvents = events.Select(evt => new
            {
                id = evt.EventId,
                title = evt.EventName,
                start = evt.EventDateTime,
                end = evt.EventDateTime,
                url = $"/Event/Details/{evt.EventId}",
                editable = false
            });
            return Json(jsonEvents);
        }
    }
}
