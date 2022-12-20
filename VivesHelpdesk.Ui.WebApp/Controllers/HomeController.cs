using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VivesHelpdesk.Data;
using VivesHelpdesk.Services;
using VivesHelpdesk.Ui.WebApp.Models;

namespace VivesHelpdesk.Ui.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TicketService _ticketService;

        public HomeController(
            ILogger<HomeController> logger,
            TicketService ticketService)
        {
            _logger = logger;
            _ticketService = ticketService;
        }

        public IActionResult Index()
        {
            var tickets = _ticketService.Find();
            return View(tickets);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

