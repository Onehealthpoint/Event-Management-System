using System.Net.Mail;
using System.Net;
using Event_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Event_Management_System.Controllers
{
    public class EventController : Controller
    {
        string smtpServer = "smtp.gmail.com"; 
        int smtpPort = 587;
        string senderEmail = "sadik078@academiacollege.edu.np"; 
        string senderPassword = "DotnetProject2025";    
        string subject = "Event Reminder";

        void SendEmail(string recipientEmail, string body)
        {
            try
            {
                using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                    smtpClient.EnableSsl = true;
                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(recipientEmail);
                    smtpClient.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        // GET: /events
        public ActionResult Index()
        {
            var events = EventSystemModel.GetAll();
            return View(events);
        }

        // GET: /events/details/5
        public ActionResult Details(string id)
        {
            var eventModel = new EventSystemModel() { EventId = id };
            eventModel.Read(id);
            var attendees = EventAttendees.GetAllByEvent(id);
            ViewBag.Attendees = attendees.Select(a => {
                var member = new OrganizationMembers { MemberId = a.MemberId };
                member.Read(a.MemberId);
                return member;
            }).ToList();
            return View(eventModel);
        }

        // GET: /events/create
        public ActionResult Create()
        {
            ViewBag.AllMembers = OrganizationMembers.GetAll();
            return View();
        }

        // POST: /events/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventSystemModel eventModel, List<string> selectedMembers)
        {
            string body = $"This is a friendly reminder for the upcoming event.\n\tEvent Name:{eventModel.EventName}\n\tEvent Venue:{eventModel.EventVenue}\n\tEvent DateTime:{eventModel.EventDateTime}";
            if (ModelState.IsValid)
            {
                eventModel.Create();
                if (selectedMembers != null)
                {
                    foreach (var memberId in selectedMembers)
                    {
                        var eventAttendee = new EventAttendees { EventId = eventModel.EventId, MemberId = memberId };
                        eventAttendee.Create();
                        var member = new OrganizationMembers { MemberId= memberId };
                        member.Read(memberId);
                        SendEmail(member.MemberEmail, body);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllMembers = OrganizationMembers.GetAll();
            return View(eventModel);
        }

        // GET: /events/edit/5
        public ActionResult Edit(string id)
        {
            var eventModel = new EventSystemModel() { EventId = id };
            eventModel.Read(id);
            ViewBag.AllMembers = OrganizationMembers.GetAll();
            ViewBag.SelectedMembers = EventAttendees.GetAllByEvent(id).Select(a => a.MemberId).ToList();
            return View(eventModel);
        }

        // POST: /events/edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, EventSystemModel eventModel, List<string> selectedMembers)
        {
            string body = $"This is a friendly reminder for the upcoming event.\n\tEvent Name:{eventModel.EventName}\n\tEvent Venue:{eventModel.EventVenue}\n\tEvent DateTime:{eventModel.EventDateTime}";
            if (ModelState.IsValid)
            {
                eventModel.Update();
                var currentAttendees = EventAttendees.GetAllByEvent(id);
                foreach (var attendee in currentAttendees)
                {
                    if (!selectedMembers.Contains(attendee.MemberId))
                    {
                        attendee.Delete();
                    }
                }
                foreach (var memberId in selectedMembers)
                {
                    if (!currentAttendees.Any(a => a.MemberId == memberId))
                    {
                        var newAttendee = new EventAttendees { EventId = id, MemberId = memberId };
                        newAttendee.Create();
                        var member = new OrganizationMembers { MemberId = memberId };
                        member.Read(memberId);
                        SendEmail(member.MemberEmail, body);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.AllMembers = OrganizationMembers.GetAll();
            ViewBag.SelectedMembers = EventAttendees.GetAllByEvent(id).Select(a => a.MemberId).ToList();
            return View(eventModel);
        }

        // GET: /events/delete/5
        public ActionResult Delete(string id)
        {
            var eventModel = new EventSystemModel() { EventId = id };
            eventModel.Read(id);
            var attendees = EventAttendees.GetAllByEvent(id);
            ViewBag.Attendees = attendees.Select(a => {
                var member = new OrganizationMembers { MemberId = a.MemberId };
                member.Read(a.MemberId);
                return member;
            }).ToList();
            return View(eventModel);
        }

        // POST: /events/delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            var eventModel = new EventSystemModel { EventId = id };
            var members = EventAttendees.GetAllByEvent(id);
            eventModel.Delete();
            foreach (var member in members)
            {
                member.Delete();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}