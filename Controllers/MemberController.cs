using Event_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers
{
    public class MemberController : Controller
    {
        // GET: /members
        public ActionResult Index()
        {
            var members = OrganizationMembers.GetAll();
            return View(members);
        }

        // GET: /members/details/5
        public ActionResult Details(string id)
        {
            var member = new OrganizationMembers() { MemberId = id};
            member.Read(id);
            var events = EventAttendees.GetAllByMember(id);
            ViewBag.Events = events.Select(a => {
                var eventModel = new EventSystemModel { EventId = a.EventId };
                eventModel.Read(a.EventId);
                return eventModel;
            }).ToList();
            return View(member);
        }

        // GET: /members/create
        public ActionResult Create()
        {
            ViewBag.AllEvents = EventSystemModel.GetAll();
            return View();
        }

        // POST: /members/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(OrganizationMembers member, List<string> selectedEvents)
        {
            if (ModelState.IsValid)
            {
                member.Create();
                if (selectedEvents != null)
                {
                    foreach (var eventId in selectedEvents)
                    {
                        var eventAttendee = new EventAttendees { EventId = eventId, MemberId = member.MemberId };
                        eventAttendee.Create();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllEvents = EventSystemModel.GetAll();
            return View(member);
        }

        // GET: /members/edit/5
        public ActionResult Edit(string id)
        {
            var member = new OrganizationMembers() { MemberId = id };
            member.Read(id);
            ViewBag.AllEvents = EventSystemModel.GetAll();
            ViewBag.SelectedEvents = EventAttendees.GetAllByMember(id).Select(a => a.EventId).ToList();
            return View(member);
        }

        // POST: /members/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, OrganizationMembers member, List<string> selectedEvents)
        {
            if (ModelState.IsValid)
            {
                member.Update();
                var currentAttendances = EventAttendees.GetAllByMember(id);
                foreach (var attendance in currentAttendances)
                {
                    if (!selectedEvents.Contains(attendance.EventId))
                    {
                        attendance.Delete();
                    }
                }
                foreach (var eventId in selectedEvents)
                {
                    if (!currentAttendances.Any(a => a.EventId == eventId))
                    {
                        var newAttendance = new EventAttendees { EventId = eventId, MemberId = id };
                        newAttendance.Create();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllEvents = EventSystemModel.GetAll();
            ViewBag.SelectedEvents = EventAttendees.GetAllByMember(id).Select(a => a.EventId).ToList();
            return View(member);
        }

        // GET: /members/delete/5
        public ActionResult Delete(string id)
        {
            var member = new OrganizationMembers() { MemberId = id };
            member.Read(id);
            var events = EventAttendees.GetAllByMember(id);
            ViewBag.Events = events.Select(a => {
                var eventModel = new EventSystemModel { EventId = a.EventId };
                eventModel.Read(a.EventId);
                return eventModel;
            }).ToList();
            return View(member);
        }

        // POST: /members/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var member = new OrganizationMembers { MemberId = id };
            var events = EventAttendees.GetAllByMember(id);
            member.Delete();
            foreach(var e in events)
            {
                e.Delete();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
