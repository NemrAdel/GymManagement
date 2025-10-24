using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            if (sessions is null || !sessions.Any())
            {
                TempData["InfoMessage"] = "No sessions available.";
            }

            return View(sessions);
        }

        public ActionResult Create()
        {

            return View();
        }
    }
}
