using Microsoft.AspNetCore.Mvc;
using VivesHelpdesk.Model;
using VivesHelpdesk.Services;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketService _ticketService;
        private readonly PersonService _personService;

        public TicketController(
            PersonService personService,
            TicketService ticketService)
        {
            _ticketService = ticketService;
            _personService = personService;
        }

        [HttpGet]
        public IActionResult Index(int? assignedToId)
        {
            if (assignedToId.HasValue)
            {
                var assignedToPerson = _personService.Get(assignedToId.Value);

                if (assignedToPerson is not null)
                {
                    //ViewBag.AssignedToPerson = assignedToPerson;
                    ViewData["AssignedToPerson"] = assignedToPerson;
                }
            }
            
            var tickets = _ticketService.Find(assignedToId);

            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return GetCreateEditView("Create");
        }

        [HttpPost]
        public IActionResult Create([FromForm]Ticket ticket)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return GetCreateEditView("Create", ticket);
            }

            _ticketService.Create(ticket);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit([FromRoute]int id)
        {
            var ticket = _ticketService.Get(id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            return GetCreateEditView("Edit", ticket);
        }

        [HttpPost]
        public IActionResult Edit([FromRoute]int id, [FromForm]Ticket ticket)
        {
            //Validate
            if (!ModelState.IsValid)
            {
                return GetCreateEditView(nameof(Edit), ticket);
            }

            _ticketService.Update(id, ticket);

            return RedirectToAction("Index");

        }

        private IActionResult GetCreateEditView(string viewName, Ticket? ticket = null)
        {
            var people = _personService.Find();
            ViewBag.People = people;
            return View(viewName, ticket);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ticket = _ticketService.Get(id);

            return View(ticket);
        }

        [HttpPost]
        [Route("[controller]/Delete/{id:int?}")]
        public IActionResult DeleteConfirmed(int id)
        {
            var ticket = _ticketService.Get(id);

            if(ticket is null)
            {
                return RedirectToAction("Index");
            }

            _ticketService.Delete(id);

            return RedirectToAction("Index");
        }
    }
}
